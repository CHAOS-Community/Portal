using System;
using System.Linq;
using CHAOS.Portal.Core;
using CHAOS.Portal.Core.Extension;
using CHAOS.Portal.Data.EF;
using CHAOS.Portal.Exception;
using Geckon;

namespace CHAOS.Portal.Extensions.Session
{
    public class SessionExtension : IExtension
    {
        #region Get

        public void Get( ICallContext callContext )
        {
            IModuleResult module = callContext.PortalResponse.PortalResult.GetModule("Geckon.Portal");

            DTO.Standard.Session session = callContext.Cache.Get<DTO.Standard.Session>(string.Format("[Session:sid={0}]", callContext.SessionGUID));

            if( session == null )
            {
                using( PortalEntities db = new PortalEntities() )
                {

                    session = db.Session_Get( callContext.SessionGUID.Value.ToByteArray(), null ).ToDTO().First();

                    callContext.Cache.Put( string.Format( "[Session:sid={0}]", callContext.SessionGUID ),
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

                CHAOS.Portal.DTO.Standard.Session session = db.Session_Get( sessionGUID.ToByteArray(), null ).ToDTO().First();

                callContext.PortalResponse.PortalResult.GetModule( "Geckon.Portal" ).AddResult( session );
            }
        }

        #endregion
        //#region Update

        //public void Update( ICallContext callContext )
        //{
        //    using( PortalEntities db = new PortalEntities() )
        //    {
        //        db.Session_Update( null, callContext.SessionGUID.Value.ToByteArray(), callContext.User.GUID.ToByteArray() ).First();

        //        Session session = db.Session_Get( callContext.SessionGUID.Value.ToByteArray(), callContext.User.GUID.ToByteArray() ).ToDTO().First();

        //        PortalResult.GetModule( "Geckon.Portal" ).AddResult( session );
        //    }
        //}

        //#endregion
        //#region Delete

        //public void Delete( ICallContext callContext )
        //{
        //    using( PortalEntities db = new PortalEntities() )
        //    {
        //        int result = db.Session_Delete( callContext.SessionGUID.Value.ToByteArray(), null );

        //        PortalResult.GetModule( "Geckon.Portal" ).AddResult( new ScalarResult( result ) );
        //    }
        //}

        //#endregion
    }
}
