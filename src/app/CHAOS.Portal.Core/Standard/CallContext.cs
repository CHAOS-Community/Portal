using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using CHAOS.Index;
using CHAOS.Portal.Core.Cache;
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

        private DTO.Standard.Session               _session;
        private DTO.Standard.UserInfo              _user;
        private IEnumerable<DTO.Standard.Group>    _group;

        #endregion
        #region Properties

        public PortalApplication PortalApplication { get; set; }
        public IPortalRequest    PortalRequest { get; set; }
        public IPortalResponse   PortalResponse { get; set; }
        public ICache            Cache { get; set; }
        public IIndexManager     IndexManager { get; set; }

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

                        Cache.Put( string.Format( "[UserInfo:sid={0}]", Session.GUID ), _user, new TimeSpan(0, 1, 0) );
                    }
                }

                return _user;
            }
        }

        public DTO.Standard.Session Session
        {
            get
            {
                if( _session == null )
                {
                    if( !PortalRequest.Parameters.ContainsKey( "sessionGUID" ) )
                        throw new NullReferenceException( "SessionGUID can't be null" );

                    using( var db = new PortalEntities() )
                    {
                        _session = db.Session_Get( new UUID( PortalRequest.Parameters[ "sessionGUID" ] ).ToByteArray(), null ).ToDTO().FirstOrDefault();

                        if( _session == null )
                            throw new SessionDoesNotExistException( "Session has expired" );
                    }
                }

                return _session;
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

        public bool IsAnonymousUser
        {
            get { return Session == null || AnonymousUserGUID.ToString() == Session.UserGUID.ToString(); }
        }

        public UUID AnonymousUserGUID
        {
            get { return new UUID( ConfigurationManager.AppSettings["AnonymousUserGUID"] ); }
        }

        public ReturnFormat ReturnFormat
        {
            get
            {
                return PortalRequest.Parameters.ContainsKey( "format" ) ? (ReturnFormat) Enum.Parse( typeof( ReturnFormat ), PortalRequest.Parameters["format"].ToUpper() ) : ReturnFormat.XML;
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
        }

        #endregion
        #region Business Logic

        public Stream GetResponseStream()
        {
            switch( ReturnFormat )
            {
                case ReturnFormat.XML:
                    XDocument xdoc = SerializerFactory.Get<XDocument>().Serialize(PortalResponse.PortalResult, false);
                    xdoc.Declaration = new XDeclaration( "1.0", "UTF-16", "yes" );

                    return new MemoryStream( Encoding.Unicode.GetBytes( xdoc.Declaration + xdoc.ToString(SaveOptions.DisableFormatting) ) );
                case ReturnFormat.JSON:
                    return new MemoryStream( Encoding.Unicode.GetBytes( SerializerFactory.Get<JSON>().Serialize(PortalResponse.PortalResult, false).Value ) );
                case ReturnFormat.JSONP:
                    return new MemoryStream( Encoding.Unicode.GetBytes( SerializerFactory.Get<JSON>().Serialize(PortalResponse.PortalResult, false).GetAsJSONP(PortalRequest.Parameters["callback"] ) ) );
                default:
                    throw new NotImplementedException("Format is unknown");
            }
        }

        #endregion
    }
}
