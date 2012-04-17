using System;
using System.Linq;
using System.Data.Objects;
using CHAOS.Portal.Core;
using CHAOS.Portal.Core.Extension;
using CHAOS.Portal.DTO.Standard;
using CHAOS.Portal.Data.EF;
using CHAOS.Portal.Exception;
using Geckon;

namespace CHAOS.Portal.Extensions.Group
{
    public class GroupExtension : IExtension
    {
        #region Get

        public void Get( ICallContext callContext, UUID guid )
        {
            var module = callContext.PortalResponse.PortalResult.GetModule("Geckon.Portal");

            using( PortalEntities db = new PortalEntities() )
            {
                var user  = callContext.User;
                var group = db.Group_Get( guid != null ? guid.ToByteArray() : null, null, user.GUID.ToByteArray() ).ToDTO().First();

                module.AddResult( group );
            }
        }

        #endregion
        #region Create

        public void Create( ICallContext callContext, string name, int systemPermission )
        {
            var module = callContext.PortalResponse.PortalResult.GetModule("Geckon.Portal");
            var user   = callContext.User;
            
            if( user.GUID.ToString() == callContext.AnonymousUserGUID.ToString() )
                throw new InsufficientPermissionsException( "Anonymous users cannot create groups" );

            using( PortalEntities db = new PortalEntities() )
            {
				var guid      = new UUID();
				var errorCode = new ObjectParameter( "ErrorCode", 0 );

            	db.Group_Create( guid.ToByteArray(), name, user.GUID.ToByteArray(), systemPermission, errorCode );

				if( ( (int) errorCode.Value ) == -100 )
                    throw new InsufficientPermissionsException("User has insufficient permissions to delete groups");

				if( ( (int) errorCode.Value ) == -200 )
                    throw new UnhandledException("Group_Create was rolled back");

                module.AddResult( db.Group_Get( guid.ToByteArray(), null, user.GUID.ToByteArray() ).ToDTO().First() );
            }
        }

        #endregion
        #region Delete

        public void Delete( ICallContext callContext, UUID guid )
        {
            var module = callContext.PortalResponse.PortalResult.GetModule("Geckon.Portal");
            var user   = callContext.User;

            if( user.GUID.ToString() == callContext.AnonymousUserGUID.ToString() )
                throw new InsufficientPermissionsException( "Anonymous users cannot delete groups" );

            using( PortalEntities db = new PortalEntities() )
            {
				var errorCode = new ObjectParameter( "ErrorCode", 0 );

                db.Group_Delete( guid.ToByteArray(), user.GUID.ToByteArray(), errorCode );

                if( ( (int) errorCode.Value ) == -100 )
                    throw new InsufficientPermissionsException("User has insufficient permissions to delete groups");

				if( ( (int) errorCode.Value ) == -200 )
					throw new UnhandledException("Group_Delete was rolled back");

                module.AddResult( new ScalarResult( 1 ) );
            }
        }

        #endregion
        #region Update

        public void Update( ICallContext callContext, UUID guid, string newName, int newSystemPermission )
        {
            var module = callContext.PortalResponse.PortalResult.GetModule("Geckon.Portal");
            var user   = callContext.User;

            using( PortalEntities db = new PortalEntities() )
            {
				ObjectParameter errorCode = new ObjectParameter( "ErrorCode", 0 );
                
				db.Group_Update( newName, BitConverter.GetBytes( newSystemPermission ), guid.ToByteArray(), user.GUID.ToByteArray(), errorCode );

                if( ( (int) errorCode.Value ) == -100 )
                    throw new InsufficientPermissionsException( "User does not have permission to update group" );

                module.AddResult( new ScalarResult( 1 ) );
            }
        }

        #endregion
    }
}
