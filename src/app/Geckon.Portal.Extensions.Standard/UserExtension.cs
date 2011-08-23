using System;
using System.Linq;
using System.Web.Mvc;
using Geckon.Portal.Core.Exception;
using Geckon.Portal.Core.Standard.Extension;
using Geckon.Portal.Data;

namespace Geckon.Portal.Extensions.Standard
{
    public class UserExtension : AExtension
    {
        #region Get

        public ContentResult Get( string sessionID )
        {
            ResultBuilder.Add( "Geckon.Portal", CallContext.User );

            return GetContentResult();
        }
        
        #endregion
        #region Create
        
        public ContentResult Create( string sessionID, string firstname, string middlename, string lastname, string email )
        {
            using( PortalDataContext db = PortalDataContext.Default() )
            {
                User user = db.User_Insert( null, firstname, middlename, lastname, email ).First();

                ResultBuilder.Add( "Geckon.Portal",
                                   user );
            }

            return GetContentResult();
        }

        #endregion
        #region Update

        public ContentResult Update( string sessionID, string firstname, string middlename, string lastname, string email )
        {
            UserInfo user = CallContext.User;

            using( PortalDataContext db = PortalDataContext.Default() )
            {
                User updatedUser = db.User_Update( user.GUID, null, firstname, middlename, lastname, email ).First();

                ResultBuilder.Add( "Geckon.Portal",
                                   updatedUser );
            }

            return GetContentResult();
        }

        #endregion
        #region Delete

        public ContentResult Delete(string sessionID, string userGUID)
        {
            UserInfo user = CallContext.User;

            if( user.GUID.ToString() != userGUID )
                throw new InsufficientPermissionsExcention( "The current user doesn't have permissions to delete the user with guid: " + userGUID );

            using( PortalDataContext db = PortalDataContext.Default() )
            {
                int result = db.User_Delete( Guid.Parse( userGUID ) );

                ResultBuilder.Add( "Geckon.Portal",
                                   new ScalarResult( result ) );
            }

            return GetContentResult();
        }

        #endregion
    }
}
