using System;
using System.Linq;
using Geckon.Portal.Core.Exception;
using Geckon.Portal.Core.Standard.Extension;
using Geckon.Portal.Data;

namespace Geckon.Portal.Extensions.Standard
{
    public class SubscriptionExtension : AExtension
    {
        #region Get

        public void Get( string sessionID, string guid )
        {
            UserInfo     user   = CallContext.User;
            SubscriptionInfo result = null;

            using( PortalDataContext db = PortalDataContext.Default() )
            {
                result = db.SubscriptionInfo_Get(null, Guid.Parse(guid), null, user.ID).FirstOrDefault();
            }

            if( result == null )
                throw new InsufficientPermissionsExcention( "User does not have sufficient permissions to access the subscription" );

            ResultBuilder.Add( "Geckon.Portal",
                               result );
        }

        #endregion
        #region Create

        public void Create( string sessionID, string name )
        {
            UserInfo user   = CallContext.User;

            using( PortalDataContext db = PortalDataContext.Default() )
            {
                int result = db.Subscription_Insert( Guid.NewGuid(), name, user.ID );

                if( result == -100 )
                    throw new InsufficientPermissionsExcention( "User does not have sufficient permissions to access the subscription" );

                SubscriptionInfo subscriptionInfo = db.SubscriptionInfo_Get( result, null, null, user.ID ).First();

                ResultBuilder.Add( "Geckon.Portal",
                                   subscriptionInfo );
            }
        }

        #endregion
        #region Delete

        public void Delete( string sessionID, string guid )
        {
            UserInfo user   = CallContext.User;
            int      result = 0;

            using( PortalDataContext db = PortalDataContext.Default() )
            {
                result = db.Subscription_Delete( null, Guid.Parse( guid ), user.ID );
            }

            if( result == -100 )
                throw new InsufficientPermissionsExcention( "User does not have sufficient permissions to delete the subscription" );

            ResultBuilder.Add( "Geckon.Portal",
                               new ScalarResult( result ) );
        }

        #endregion
        #region Update

        public void Update( string sessionID, string guid, string newName )
        {
            UserInfo user      = CallContext.User;
            int      result    = 0;

            using( PortalDataContext db = PortalDataContext.Default() )
            {
                result = db.Subscription_Update( null, Guid.Parse( guid ), newName, user.ID );
            }

            if( result == -100 )
                throw new InsufficientPermissionsExcention( "User does not have sufficient permissions to access the subscription" );

            ResultBuilder.Add( "Geckon.Portal",
                               new ScalarResult( result ) );
        }

        #endregion
    }
}

namespace Geckon.Portal.Data.Dto
{
    public class UserInfo
    {
    }
}
