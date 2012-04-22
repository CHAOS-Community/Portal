using System;
using System.Linq;
using System.Xml.Linq;
using CHAOS.Portal.Core;
using CHAOS.Portal.Core.Module;
using CHAOS.Portal.DTO.Standard;
using CHAOS.Portal.Data.EF;
using CHAOS.Portal.Exception;
using Session = CHAOS.Portal.DTO.Standard.Session;

namespace CHAOS.Portal.Modules
{
    [Module("Portal")]
    public class SessionModule : AModule
    {
        #region Properties

        private string ConnectionString { get; set; }

        private PortalEntities NewPortalEntities
        {
            get
            {
                return new PortalEntities( ConnectionString );
            }
        }

        #endregion
        #region Constructors

        public override void Initialize( string configuration )
        {
            ConnectionString = XDocument.Parse(configuration).Root.Attribute( "ConnectionString" ).Value;
        }

        #endregion
        #region Business Logic

        #region Get

        [Datatype("Session","Get")]
        public Session Get(ICallContext callContext)
        {
            var session = callContext.Cache.Get<Session>( string.Format( "[Session:sid={0}]", callContext.Session.GUID ) );

            if( session == null )
            {
                using( var db = NewPortalEntities )
                {
                    session = db.Session_Get( callContext.Session.GUID.ToByteArray(), null ).ToDTO().First();

                    callContext.Cache.Put( string.Format( "[Session:sid={0}]", callContext.Session.GUID ),
                                           session,
                                           new TimeSpan( 0, 1, 0 ) );
                }
            }

            
            return session;
        } 

        #endregion
        #region Create

        [Datatype("Session", "Create")]
        public Session Create( ICallContext callContext, uint protocolVersion )
        {
            if( protocolVersion != 4 )
                throw new WrongProtocolVersionException( "Current version is 4" );

            // TODO: Add Module filtering

            using( var db = NewPortalEntities )
            {
            	var sessionGUID = new UUID();

				db.Session_Create( sessionGUID.ToByteArray(), callContext.AnonymousUserGUID.ToByteArray() );

                return db.Session_Get( sessionGUID.ToByteArray(), null ).ToDTO().First();
            }
        }

        #endregion
        #region Update

        [Datatype("Session", "Update")]
        public Session Update( ICallContext callContext )
        {
            using( var db = NewPortalEntities )
            {
                var result = db.Session_Update( null, callContext.Session.GUID.ToByteArray(), callContext.User.GUID.ToByteArray() ).First();

                return db.Session_Get( callContext.Session.GUID.ToByteArray(), callContext.User.GUID.ToByteArray() ).ToDTO().First();
            }
        }

        #endregion
        #region Delete

        [Datatype("Session", "Delete")]
        public ScalarResult Delete( ICallContext callContext )
        {
            using( var db = NewPortalEntities )
            {
                var result = db.Session_Delete( callContext.Session.GUID.ToByteArray(), null ).First();

                return new ScalarResult( result.Value );
            }
        }

        #endregion

        #endregion
    }
}
