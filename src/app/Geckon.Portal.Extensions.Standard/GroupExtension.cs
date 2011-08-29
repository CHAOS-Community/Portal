using System;
using System.Linq;
using Geckon.Portal.Core.Exception;
using Geckon.Portal.Core.Standard.Extension;
using Geckon.Portal.Data;

namespace Geckon.Portal.Extensions.Standard
{
    public class GroupExtension : AExtension
    {
        #region Get

        public void Get( string sessionID, string guid )
        {
            using( PortalDataContext db = PortalDataContext.Default() )
            {
                Guid?    groupGuid = string.IsNullOrEmpty( guid ) ? (Guid?) null : Guid.Parse( guid );
                UserInfo user      = CallContext.User;
                Group    group     = db.Group_Get( null, groupGuid, null, user.ID ).First();

                ResultBuilder.Add( "Geckon.Portal",
                                   group );
            }
        }

        #endregion
        #region Create

        public void Create( string sessionID, string name, int systemPermission )
        {
            UserInfo user = CallContext.User;

            if( user.GUID == PortalContext.AnonymousUserGUID )
                throw new InsufficientPermissionsExcention( "Anonymous users cannot create groups" );

            using (PortalDataContext db = PortalDataContext.Default())
            {
                int result = db.Group_Insert( null, name, systemPermission, user.ID );

                ResultBuilder.Add( "Geckon.Portal",
                                   new ScalarResult( result ) );
            }
        }

        #endregion
        #region Delete

        public void Delete( string sessionID, string groupGUID )
        {
            UserInfo user = CallContext.User;

            if( user.GUID == PortalContext.AnonymousUserGUID )
                throw new InsufficientPermissionsExcention( "Anonymous users cannot delete groups" );

            using( PortalDataContext db = PortalDataContext.Default() )
            {
                int result = db.Group_Delete( null, Guid.Parse( groupGUID ), user.ID, null );

                if( result == -100 )
                    throw new InsufficientPermissionsExcention("User has insufficient permissions to delete groups");

                ResultBuilder.Add( "Geckon.Portal",
                                   new ScalarResult( result ) );
            }
        }

        #endregion
        #region Update

        public void Update( string sessionID, string groupGUID, string newName, int newSystemPermission )
        {
            UserInfo user = CallContext.User;

            using( PortalDataContext db = PortalDataContext.Default() )
            {
                int result = db.Group_Update( newName, newSystemPermission, null, Guid.Parse( groupGUID ), user.ID, null );

                if( result == -100 )
                    throw new InsufficientPermissionsExcention( "User does not have permission to update group" );

                ResultBuilder.Add( "Geckon.Portal",
                                   new ScalarResult( result ) );
            }
        }

        #endregion
    }
}