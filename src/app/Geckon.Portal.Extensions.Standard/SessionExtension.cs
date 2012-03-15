using System;
using System.Linq;
using CHAOS.Portal.Data.DTO;
using CHAOS.Portal.Data.EF;
using Geckon.Portal.Core.Standard.Extension;
using Geckon.Portal.Data.Result;
using Session = CHAOS.Portal.Data.DTO.Session;

namespace Geckon.Portal.Extensions.Standard
{
    public class SessionExtension : AExtension
    {
        #region Get

        public void Get( CallContext callContext )
        {
            IModuleResult module = PortalResult.GetModule("Geckon.Portal");

			CHAOS.Portal.Data.DTO.Session session = callContext.Cache.Get<CHAOS.Portal.Data.DTO.Session>( string.Format( "[Session:sid={0}]", callContext.SessionGUID ) );

            if( session == null )
            {
                using( PortalEntities db = new PortalEntities() )
                {

                    session = db.Session_Get( callContext.SessionGUID.ToByteArray(), null ).ToDTO().First();

                    callContext.Cache.Put( string.Format( "[Session:sid={0}]", callContext.SessionGUID ),
                                           session,
                                           new TimeSpan( 0, 1, 0 ) );
                }
            }

            
            module.AddResult( session );
        } 

        #endregion
        #region Create

        public void Create( CallContext callContext, int protocolVersion )
        {
            // TODO: Check protocol version
            // TODO: Add Module filtering

            using( PortalEntities db = new PortalEntities() )
            {
            	UUID sessionGUID = new UUID();

				db.Session_Create( sessionGUID.ToByteArray(), callContext.AnonymousUserGUID.ToByteArray() );

                PortalResult.GetModule( "Geckon.Portal" ).AddResult( db.Session_Get( sessionGUID.ToByteArray(), null ).ToDTO().First() );
            }
        }

        #endregion
        #region Update

        public void Update( CallContext callContext )
        {
            using( PortalEntities db = new PortalEntities() )
            {
				db.Session_Update( null, callContext.SessionGUID.ToByteArray(), callContext.User.GUID.ToByteArray() );

				Session session = db.Session_Get( callContext.SessionGUID.ToByteArray(), callContext.User.GUID.ToByteArray() ).ToDTO().First();

				PortalResult.GetModule( "Geckon.Portal" ).AddResult( session );
            }
        }

        #endregion
        #region Delete

        public void Delete( CallContext callContext )
        {
            using( PortalEntities db = new PortalEntities() )
            {
				int result = db.Session_Delete( callContext.SessionGUID.ToByteArray(), null );

                PortalResult.GetModule( "Geckon.Portal" ).AddResult( new ScalarResult( result ) );
            }
        }

        #endregion
    }
}
