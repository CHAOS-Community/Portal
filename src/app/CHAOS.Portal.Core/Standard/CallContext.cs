using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using CHAOS.Index;
using CHAOS.Portal.Core.Cache;
using CHAOS.Portal.Core.Logging;
using CHAOS.Portal.Core.Logging.Database;
using CHAOS.Portal.Core.Request;
using CHAOS.Portal.Data.EF;
using CHAOS.Portal.Exception;
using CHAOS.Serialization.JSON;
using CHAOS.Serialization.Standard;

namespace CHAOS.Portal.Core.Standard
{
    public class CallContext : ICallContext
    {
       #region Fields

        private DTO.Standard.Session                       _session;
        private DTO.Standard.UserInfo                      _user;
        private IEnumerable<DTO.Standard.SubscriptionInfo> _subscriptions;
        private IEnumerable<DTO.Standard.Group>            _group;
		private LogLevel?								   _logLevel;

	    private const string SESSIONGUID_PARAMETER_NAME = "sessionGUID";

        #endregion
       #region Properties

        public PortalApplication PortalApplication { get; set; }
        public IPortalRequest    PortalRequest { get; set; }
        public IPortalResponse   PortalResponse { get; set; }
        public ICache            Cache { get; set; }
        public IIndexManager     IndexManager { get; set; }
		public ILog			     Log { get; protected set; }

        /// <summary>
        /// Returns the current user, the user is cached and will not be updated during the callContexts life.
        /// </summary>
        public DTO.Standard.UserInfo User 
        { 
            get
            {
                if( _user == null )
                {
                    using( var db = new PortalEntities() )
                    {
                        _user = db.UserInfo_Get( null, Session.GUID.ToByteArray(), null ).ToDTO().FirstOrDefault();
                        
                        if( _user == null )
                            throw new SessionDoesNotExistException( "Session has expired" );

                        Cache.Put( string.Format( "[UserInfo:sid={0}]", Session.GUID ), _user, new TimeSpan(0, 1, 0, 0) );
                    }
                }

                return _user;
            }
        }

		/// <summary>
		/// Get the current session
		/// </summary>
        public DTO.Standard.Session Session
        {
            get
            {
				if( GetSessionFromDatabase() == null)
					throw new SessionDoesNotExistException( "SessionGUID is invalid or has expired" );

				return _session;
            }
        }

		/// <summary>
		/// Get subscriptions associated with the current user
		/// </summary>
        public IEnumerable<DTO.Standard.SubscriptionInfo> Subscriptions
        {
            get
            {
                if( _subscriptions == null )
                {
                    using( var db = new PortalEntities() )
                    {
                        _subscriptions = db.SubscriptionInfo_Get( null, User.GUID.ToByteArray() ).ToDTO().ToList();
                    }
                }

                return _subscriptions;
            }
        }

        /// <summary>
        /// returns the groups the current user is part of, the Groups are cached, changes in the database will not be pickup up during the callContext life.
        /// </summary>
        public IEnumerable<DTO.Standard.Group> Groups
        {
            get
            {
                if( _group == null )
                {
                    using( var db = new PortalEntities() )
                    {
                        _group = db.Group_Get( null, null, User.GUID.ToByteArray() ).ToDTO().ToList();
                    }
                }

                return _group;
            }
        }

		/// <summary>
		/// True if current user is anonymous
		/// </summary>
        public bool IsAnonymousUser
        {
            get { return GetSessionFromDatabase() == null || AnonymousUserGUID.ToString() == Session.UserGUID.ToString(); }
        }

		/// <summary>
		/// Get the UUID of the anonymous user
		/// </summary>
        public UUID AnonymousUserGUID
        {
            get { return new UUID( ConfigurationManager.AppSettings["AnonymousUserGUID"] ); }
        }

		/// <summary>
		/// Get the current Return format
		/// </summary>
        public ReturnFormat ReturnFormat
        {
            get
            {
                return PortalRequest.Parameters.ContainsKey( "format" ) ? (ReturnFormat) Enum.Parse( typeof( ReturnFormat ), PortalRequest.Parameters["format"].ToUpper() ) : ReturnFormat.XML;
            }
        }

	    public LogLevel LogLevel
	    {
		    get
		    {
				if( !_logLevel.HasValue )
				{
					var logLevel = ConfigurationManager.AppSettings["LOG_LEVEL"];

					_logLevel = (LogLevel) Enum.Parse( typeof(LogLevel), logLevel ?? "Info" );
				}

			    return _logLevel.Value;
		    }
	    }

        #endregion
       #region Constructors

        public CallContext( PortalApplication portalApplication, IPortalRequest portalRequest, IPortalResponse portalResponse )
        {
            PortalApplication = portalApplication;
            PortalRequest     = portalRequest;
            PortalResponse    = portalResponse;

            Cache        = portalApplication.Cache;
            IndexManager = portalApplication.IndexManager;
			Log          = new DatabaseLogger( string.Format("{0}/{1}", PortalRequest.Extension, PortalRequest.Action ), GetSessionFromDatabase() != null ? GetSessionFromDatabase().GUID : null, LogLevel ); // TODO: LogLevel should be set in config
        }

        #endregion
       #region Business Logic

		/// <summary>
		/// Write any results in the portal response to a stream. The format is determined by the ReturnFormat property
		/// </summary>
		/// <returns></returns>
        public Stream GetResponseStream()
        {
            switch( ReturnFormat )
            {
                case ReturnFormat.XML:
                    Log.Debug("Serializing");
                    var xdoc = SerializerFactory.Get<XDocument>().Serialize(PortalResponse.PortalResult, false);
                    Log.Debug("Serialized");
                    xdoc.Declaration = new XDeclaration( "1.0", "UTF-8", "yes" );

                    var stream = new MemoryStream();

                    xdoc.Save(stream);

                    return stream;
                case ReturnFormat.JSON:
                    return new MemoryStream( Encoding.UTF8.GetBytes( SerializerFactory.Get<JSON>().Serialize(PortalResponse.PortalResult, false).Value ) );
                case ReturnFormat.JSONP:
                    return new MemoryStream( Encoding.UTF8.GetBytes( SerializerFactory.Get<JSON>().Serialize(PortalResponse.PortalResult, false).GetAsJSONP(PortalRequest.Parameters["callback"] ) ) );
                case ReturnFormat.ATTACHMENT:
                    return PortalResponse.Attachment.Stream;
                default:
                    throw new NotImplementedException("Format is unknown");
            }
        }

		/// <summary>
		/// Gets the current session from the database
		/// </summary>
		/// <returns>The current session from the database, or null if sessionGUID is not specified</returns>
		public DTO.Standard.Session GetSessionFromDatabase()
		{
			if( _session == null )
            {
                if( !PortalRequest.Parameters.ContainsKey( SESSIONGUID_PARAMETER_NAME ) )
                    return null;

                using( var db = new PortalEntities() )
                {
                    _session = db.Session_Get( new UUID( PortalRequest.Parameters[ SESSIONGUID_PARAMETER_NAME ] ).ToByteArray(), null ).ToDTO().FirstOrDefault();

					if( _session == null )
						return null;
                }
            }

			return _session;
		}

        #endregion
    }
}
