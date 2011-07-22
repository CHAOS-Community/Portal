using System.Linq;
using System.Web.Mvc;
using Geckon.Portal.Core;
using Geckon.Portal.Core.Exception;
using Geckon.Portal.Core.Standard.Extension;
using Geckon.Portal.Data;

namespace Geckon.Portal.Extensions.Standard
{
    public class GroupExtension : AExtension
    {
        #region Get

        public ContentResult Get( string sessionID )
        {
            using( PortalDataContext db = GetNewPortalDataContext() )
            {
                Data.Dto.UserInfo user  = GetUserInfo( sessionID );
                Data.Dto.Group    group = Data.Dto.Group.Create( db.Group_Get( null, user.GUID ).First() );

                ResultBuilder.Add( "Geckon.Portal",
                                   group );
            }

            CallModules( new Parameter( "sessionID", sessionID ) );

            return GetContentResult();
        }

        #endregion
        #region Create

        public ContentResult Create( string sessionID, string name )
        {
            Data.Dto.UserInfo user = GetUserInfo( sessionID );

            if( user.GUID == PortalContext.AnonymousUserGUID )
                throw new InsufficientPermissionsExcention( "Anonymous users cannot create groups" );

            using( PortalDataContext db = GetNewPortalDataContext() )
            {
                Data.Dto.Group group = Data.Dto.Group.Create( db.Group_Insert( null, name ).First() );

                ResultBuilder.Add( "Geckon.Portal",
                                   group );
            }


        }

        #endregion
    }
}
