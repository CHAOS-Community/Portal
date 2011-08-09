using System;
using System.Web.Mvc;
using Geckon.Portal.Core;
using Geckon.Portal.Core.Standard.Extension;
using Geckon.Portal.Data;
using UserInfo = Geckon.Portal.Data.Dto.UserInfo;

namespace Geckon.Portal.Extensions.Standard
{
    public class SubscriptionExtension : AExtension
    {
        #region Construction

        public SubscriptionExtension( IPortalContext portalContext ) : base( portalContext ) { }
        public SubscriptionExtension() : base() { }

        #endregion
        #region Get

        public ContentResult Get( string sessionID, string guid )
        {
            UserInfo user = GetUserInfo( sessionID );

            using( PortalDataContext db = GetNewPortalDataContext() )
            {
                db.Subscription_Get( null, Guid.Parse( guid ), null, user.ID );
            }

            CallModules( new Parameter( "sessionID", sessionID ),
                         new Parameter( "guid", guid           ) );

            return GetContentResult();
        }

        #endregion
    }
}
