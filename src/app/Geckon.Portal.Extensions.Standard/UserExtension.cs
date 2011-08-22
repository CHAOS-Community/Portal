using System;
using System.Linq;
using System.Web.Mvc;
using Geckon.Portal.Core;
using Geckon.Portal.Core.Exception;
using Geckon.Portal.Core.Standard.Extension;
using Geckon.Portal.Data;
using Geckon.Portal.Data.Dto;

namespace Geckon.Portal.Extensions.Standard
{
    public class UserExtension : AExtension
    {
        #region Get

        public ContentResult Get( string sessionID )
        {
            ResultBuilder.Add( "Geckon.Portal", CallContext.User );

    //        CallModules( new Parameter( "sessionID", sessionID ) );

            return GetContentResult();
        }
        
        #endregion
        #region Create
        
        public ContentResult Create( string sessionID, string firstname, string middlename, string lastname, string email )
        {
            using( PortalDataContext db = PortalDataContext.Default() )
            {
                Data.Dto.User user = Data.Dto.User.Create( db.User_Insert( null, firstname, middlename, lastname, email ).First() );

                ResultBuilder.Add( "Geckon.Portal",
                                   user );
            }

            //CallModules( new Parameter( "sessionID", sessionID ),
            //             new Parameter( "firstname", firstname ),
            //             new Parameter( "middlename", middlename ),
            //             new Parameter( "lastname", lastname ),
            //             new Parameter( "email", email ) );

            return GetContentResult();
        }

        #endregion
        #region Update

        public ContentResult Update( string sessionID, string firstname, string middlename, string lastname, string email )
        {
            Data.Dto.UserInfo user = CallContext.User;

            using( PortalDataContext db = PortalDataContext.Default() )
            {
                Data.Dto.User updatedUser = Data.Dto.User.Create( db.User_Update( user.GUID, null, firstname, middlename, lastname, email ).First() );

                ResultBuilder.Add( "Geckon.Portal",
                                   updatedUser );
            }

            //CallModules( new Parameter( "sessionID", sessionID ),
            //             new Parameter( "firstname", firstname ),
            //             new Parameter( "middlename", middlename ),
            //             new Parameter( "lastname", lastname ),
            //             new Parameter( "email", email ) );

            return GetContentResult();
        }

        #endregion
        #region Delete

        public ContentResult Delete(string sessionID, string userGUID)
        {
            Data.Dto.UserInfo user = CallContext.User;

            if( user.GUID.ToString() != userGUID )
                throw new InsufficientPermissionsExcention( "The current user doesn't have permissions to delete the user with guid: " + userGUID );

            using( PortalDataContext db = PortalDataContext.Default() )
            {
                int result = db.User_Delete( Guid.Parse( userGUID ) );

                ResultBuilder.Add( "Geckon.Portal",
                                   new ScalarResult( result ) );
            }

            //CallModules( new Parameter( "sessionID", sessionID ),
            //             new Parameter( "userGUID", userGUID ) );

            return GetContentResult();
        }

        #endregion
    }
}
