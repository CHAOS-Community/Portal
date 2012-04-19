﻿using System;
using System.Linq;
using CHAOS.Portal.Core;
using CHAOS.Portal.Core.Extension.Standard;
using CHAOS.Portal.DTO.Standard;
using CHAOS.Portal.Data.EF;
using CHAOS.Portal.Exception;
using Geckon;

namespace CHAOS.Portal.Extensions.Session
{
    [Extension("Session")]
    public class SessionExtension : AExtension
    {
        #region Get

        public void Get( ICallContext callContext )
        {
            IModuleResult module = callContext.PortalResponse.PortalResult.GetModule("Geckon.Portal");

            DTO.Standard.Session session = callContext.Cache.Get<DTO.Standard.Session>( string.Format( "[Session:sid={0}]", callContext.Session.GUID ) );

            if( session == null )
            {
                using( PortalEntities db = new PortalEntities() )
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
    }
}
