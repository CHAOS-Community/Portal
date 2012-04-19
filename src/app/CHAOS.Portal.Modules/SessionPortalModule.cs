using System;
using System.Linq;
using CHAOS.Portal.Core;
using CHAOS.Portal.Core.Module.Standard;
using CHAOS.Portal.DTO.Standard;
using CHAOS.Portal.Data.EF;
using CHAOS.Portal.Exception;
using Geckon;
using Session = CHAOS.Portal.DTO.Standard.Session;

namespace CHAOS.Portal.Modules
{
    [Module("Portal")]
    public class SessionPortalModule : AModule
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
            ConnectionString = configuration;
        }

        #endregion
        #region Business Logic

        #region Get

        public Session Get(ICallContext callContext)
        {
            IModuleResult module = callContext.PortalResponse.PortalResult.GetModule("Geckon.Portal");

            Session session = callContext.Cache.Get<Session>( string.Format( "[Session:sid={0}]", callContext.Session.GUID ) );

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

        public Session Create( ICallContext callContext, uint protocolVersion )
        {
            if( protocolVersion != 4 )
                throw new WrongProtocolVersionException( "Current version is 4" );

            // TODO: Add Module filtering

            using( var db = new PortalEntities() )
            {
            	var sessionGUID = new UUID();

				db.Session_Create( sessionGUID.ToByteArray(), callContext.AnonymousUserGUID.ToByteArray() );

                return db.Session_Get( sessionGUID.ToByteArray(), null ).ToDTO().First();
            }
        }

        #endregion
        #region Update

        public Session Update(ICallContext callContext)
        {
            using( var db = new PortalEntities() )
            {
                db.Session_Update( null, callContext.Session.GUID.ToByteArray(), callContext.User.GUID.ToByteArray() ).First();

                return db.Session_Get( callContext.Session.GUID.ToByteArray(), callContext.User.GUID.ToByteArray() ).ToDTO().First();
            }
        }

        #endregion
        #region Delete

        public ScalarResult Delete(ICallContext callContext)
        {
            using( PortalEntities db = new PortalEntities() )
            {
                int result = db.Session_Delete( callContext.Session.GUID.ToByteArray(), null );

                return new ScalarResult( result );
            }
        }

        #endregion

        #endregion
    }
}
