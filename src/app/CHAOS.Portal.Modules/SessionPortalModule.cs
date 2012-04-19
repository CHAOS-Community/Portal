using System;
using System.Linq;
using CHAOS.Portal.Core;
using CHAOS.Portal.Core.Module.Standard;
using CHAOS.Portal.DTO.Standard;
using CHAOS.Portal.Data.EF;
using CHAOS.Portal.Exception;
using Geckon;

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

        public void Get( ICallContext callContext )
        {
            IModuleResult module = callContext.PortalResponse.PortalResult.GetModule("Geckon.Portal");

            DTO.Standard.Session session = callContext.Cache.Get<DTO.Standard.Session>( string.Format( "[Session:sid={0}]", callContext.Session.GUID ) );

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

            
            module.AddResult( session );
        } 

        #endregion
        #region Create

        public void Create( ICallContext callContext, uint protocolVersion )
        {
            if( protocolVersion != 4 )
                throw new WrongProtocolVersionException( "Current version is 4" );

            // TODO: Add Module filtering

            using( PortalEntities db = new PortalEntities() )
            {
            	UUID sessionGUID = new UUID();

				db.Session_Create( sessionGUID.ToByteArray(), callContext.AnonymousUserGUID.ToByteArray() );

                DTO.Standard.Session session = db.Session_Get( sessionGUID.ToByteArray(), null ).ToDTO().First();

                callContext.PortalResponse.PortalResult.GetModule( "Geckon.Portal" ).AddResult( session );
            }
        }

        #endregion
        #region Update

        public void Update( ICallContext callContext )
        {
            using( PortalEntities db = new PortalEntities() )
            {
                db.Session_Update( null, callContext.Session.GUID.ToByteArray(), callContext.User.GUID.ToByteArray() ).First();

                DTO.Standard.Session session = db.Session_Get( callContext.Session.GUID.ToByteArray(), callContext.User.GUID.ToByteArray() ).ToDTO().First();

                callContext.PortalResponse.PortalResult.GetModule( "Geckon.Portal" ).AddResult( session );
            }
        }

        #endregion
        #region Delete

        public void Delete( ICallContext callContext )
        {
            using( PortalEntities db = new PortalEntities() )
            {
                int result = db.Session_Delete( callContext.Session.GUID.ToByteArray(), null );

                callContext.PortalResponse.PortalResult.GetModule( "Geckon.Portal" ).AddResult( new ScalarResult( result ) );
            }
        }

        #endregion

        #endregion
    }
}
