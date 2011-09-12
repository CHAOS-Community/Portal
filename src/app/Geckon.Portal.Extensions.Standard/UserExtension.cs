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

        public void Get( string sessionID )
        {
            ResultBuilder.Add( "Geckon.Portal", CallContext.User );
        }
        
        #endregion
        #region Create
        
        public void Create( string sessionID, string firstName, string middleName, string lastName, string email )
        {
            using( PortalDataContext db = PortalDataContext.Default() )
            {
                User user = db.User_Insert( null, firstName, middleName, lastName, email ).First();

                ResultBuilder.Add( "Geckon.Portal",
                                   user );
            }
        }

        #endregion
        #region Update

        public void Update( string sessionID, string firstName, string middleName, string lastName, string email )
        {
            UserInfo user = CallContext.User;

            using( PortalDataContext db = PortalDataContext.Default() )
            {
                User updatedUser = db.User_Update( user.GUID, null, firstName, middleName, lastName, email ).First();

                ResultBuilder.Add( "Geckon.Portal",
                                   updatedUser );
            }
        }

        #endregion
        #region Delete

        public void Delete( string sessionID, string guid )
        {
            UserInfo user = CallContext.User;

            if( user.GUID.ToString() != guid )
                throw new InsufficientPermissionsExcention( "The current user doesn't have permissions to delete the user with guid: " + guid );

            using( PortalDataContext db = PortalDataContext.Default() )
            {
                int result = db.User_Delete( Guid.Parse( guid ) );

                ResultBuilder.Add( "Geckon.Portal",
                                   new ScalarResult( result ) );
            }
        }

        #endregion
    }
}
