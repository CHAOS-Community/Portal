using System;
using System.Linq;
using Geckon.Portal.Core.Exception;
using Geckon.Portal.Core.Standard.Extension;
using Geckon.Portal.Data;

namespace Geckon.Portal.Extensions.Standard
{
    public class UserExtension : AExtension
    {
        #region Get

        public void Get( CallContext callContext )
        {
            PortalResult.GetModule( "Geckon.Portal" ).AddResult( callContext.User );
        }
        
        #endregion
        #region Create
        
        public void Create( CallContext callContext, string firstName, string middleName, string lastName, string email )
        {
            using( PortalDataContext db = PortalDataContext.Default() )
            {
                User user = db.User_Insert( null, firstName, middleName, lastName, email ).First();

                PortalResult.GetModule("Geckon.Portal").AddResult(user);
            }
        }

        #endregion
        #region Update

        // TODO: It should not be possible to change email, without email confirmation
        public void Update( CallContext callContext, string firstName, string middleName, string lastName, string email )
        {
            UserInfo user = callContext.User;

            using( PortalDataContext db = PortalDataContext.Default() )
            {
                User updatedUser = db.User_Update( user.GUID, null, firstName, middleName, lastName, email ).First();

                PortalResult.GetModule("Geckon.Portal").AddResult(updatedUser);
            }
        }

        #endregion
        #region Delete

        public void Delete( CallContext callContext, string guid )
        {
            UserInfo user = callContext.User;

            if( user.GUID.ToString() != guid )
                throw new InsufficientPermissionsExcention( "The current user doesn't have permissions to delete the user with guid: " + guid );

            using( PortalDataContext db = PortalDataContext.Default() )
            {
                int result = db.User_Delete( Guid.Parse( guid ) );

                PortalResult.GetModule("Geckon.Portal").AddResult( new ScalarResult( result ) );
            }
        }

        #endregion
    }
}
