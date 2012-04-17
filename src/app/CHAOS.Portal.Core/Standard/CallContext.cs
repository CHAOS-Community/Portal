using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using CHAOS.Portal.Core.Cache;
using CHAOS.Portal.Core.Request;
using CHAOS.Portal.Data.EF;
using CHAOS.Portal.Exception;
using Geckon;
using Geckon.Index;
using Geckon.Serialization.JSON;
using Geckon.Serialization.Standard;

namespace CHAOS.Portal.Core.Standard
{
    public class CallContext : ICallContext
    {
        #region Fields

        private DTO.Standard.Session _session;

        #endregion
        #region Properties

        public PortalApplication PortalApplication { get; set; }
        public IPortalRequest    PortalRequest { get; set; }
        public IPortalResponse   PortalResponse { get; set; }
        public ICache            Cache { get; set; }
        public IIndexManager     IndexManager { get; set; }

        public DTO.Standard.UserInfo User 
        { 
            get
            {
                var userInfo = Cache.Get<DTO.Standard.UserInfo>( string.Format( "[UserInfo:sid={0}]", Session.GUID ) );

                if( userInfo == null )
                {
                    using( var db = new PortalEntities() )
                    {
                        userInfo = db.UserInfo_Get( null, Session.GUID.ToByteArray(), null ).ToDTO().FirstOrDefault();
                        
                        if( userInfo == null )
                            throw new SessionDoesNotExist( "Session has expired" );

                        Cache.Put( string.Format( "[UserInfo:sid={0}]", Session.GUID ), userInfo, new TimeSpan(0, 1, 0) );
                    }
                }

                return userInfo;
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
                        _session = db.Session_Get( new UUID( PortalRequest.Parameters[ "sessionGUID" ] ).ToByteArray(), null ).ToDTO().First();
                    }
                }

                return _session;
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
                return PortalRequest.Parameters.ContainsKey( "format" ) ? (ReturnFormat) Enum.Parse( typeof( ReturnFormat ), PortalRequest.Parameters["format"].ToUpper() ) : ReturnFormat.GXML;
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
                case ReturnFormat.GXML:
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
