using System;
using System.Data.Objects;
using System.Linq;
using CHAOS.Portal.Data.DTO;
using CHAOS.Portal.Data.EF;
using Geckon.Portal.Core.Exception;
using Geckon.Portal.Core.Standard.Extension;
using Group = CHAOS.Portal.Data.DTO.Group;
using UserInfo = CHAOS.Portal.Data.DTO.UserInfo;

namespace Geckon.Portal.Extensions.Standard
{
    public class GroupExtension : AExtension
    {
        #region Get

        public void Get( CallContext callContext, Guid? guid )
        {
            using( PortalEntities db = new PortalEntities() )
            {
                UserInfo user      = callContext.User;
                Group    group     = db.Group_Get( guid.HasValue ? guid.Value.ToByteArray() : null, null, user.GUID.ToByteArray() ).ToDTO().First();

                PortalResult.GetModule("Geckon.Portal").AddResult(group);
            }
        }

        #endregion
        #region Create

        public void Create( CallContext callContext, string name, int systemPermission )
        {
            UserInfo user = callContext.User;
            
            if( user.GUID.ToString() == callContext.AnonymousUserGUID.ToString() )
                throw new InsufficientPermissionsExcention( "Anonymous users cannot create groups" );

            using( PortalEntities db = new PortalEntities() )
            {
				UUID            guid      = new UUID();
				ObjectParameter errorCode = new ObjectParameter( "ErrorCode", 0 );

            	db.Group_Create( guid.ToByteArray(), name, user.GUID.ToByteArray(), systemPermission, errorCode );
	
				if( ( (int) errorCode.Value ) == -100 )
                    throw new InsufficientPermissionsExcention("User has insufficient permissions to delete groups");

				if( ( (int) errorCode.Value ) == -200 )
                    throw new UnhandledException("Group_Create was rolled back");

                PortalResult.GetModule("Geckon.Portal").AddResult( db.Group_Get( guid.ToByteArray(), null, user.GUID.ToByteArray() ).ToDTO().First() );
            }
        }

        #endregion
        #region Delete

        public void Delete( CallContext callContext, string guid )
        {
            UserInfo user = callContext.User;

            if( user.GUID.ToString() == callContext.AnonymousUserGUID.ToString() )
                throw new InsufficientPermissionsExcention( "Anonymous users cannot delete groups" );

            using( PortalEntities db = new PortalEntities() )
            {
				ObjectParameter errorCode = new ObjectParameter( "ErrorCode", 0 );

                db.Group_Delete( new UUID( guid ).ToByteArray(), user.GUID.ToByteArray(), errorCode );

                if( ( (int) errorCode.Value ) == -100 )
                    throw new InsufficientPermissionsExcention("User has insufficient permissions to delete groups");

				if( ( (int) errorCode.Value ) == -200 )
					throw new UnhandledException("Group_Delete was rolled back");

                PortalResult.GetModule("Geckon.Portal").AddResult( new ScalarResult( 1 ) );
            }
        }

        #endregion
        #region Update

        public void Update( CallContext callContext, string guid, string newName, int newSystemPermission )
        {
            UserInfo user = callContext.User;

            using( PortalEntities db = new PortalEntities() )
            {
				ObjectParameter errorCode = new ObjectParameter( "ErrorCode", 0 );
                
				db.Group_Update( newName, BitConverter.GetBytes( newSystemPermission ), new UUID( guid ).ToByteArray(), user.GUID.ToByteArray(), errorCode );

                if( ( (int) errorCode.Value ) == -100 )
                    throw new InsufficientPermissionsExcention( "User does not have permission to update group" );

                PortalResult.GetModule("Geckon.Portal").AddResult( new ScalarResult( 1 ) );
            }
        }

        #endregion
    }
}