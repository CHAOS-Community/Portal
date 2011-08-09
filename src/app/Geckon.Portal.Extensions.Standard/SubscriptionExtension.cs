using System;
using System.Linq;
using System.Web.Mvc;
using Geckon.Portal.Core;
using Geckon.Portal.Core.Exception;
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
            UserInfo     user   = GetUserInfo( sessionID );
            Subscription result = null;

            using( PortalDataContext db = GetNewPortalDataContext() )
            {
                result = db.Subscription_Get(null, Guid.Parse(guid), null, user.ID).FirstOrDefault();
            }

            if( result == null )
                throw new InsufficientPermissionsExcention( "User does not have sufficient permissions to access the subscription" );

            Data.Dto.Subscription subscription = Data.Dto.Subscription.Create( result );

            ResultBuilder.Add( "Geckon.Portal",
                                   subscription );

            CallModules( new Parameter( "sessionID", sessionID ),
                         new Parameter( "guid", guid           ) );

            return GetContentResult();
        }

        #endregion
    }
}
