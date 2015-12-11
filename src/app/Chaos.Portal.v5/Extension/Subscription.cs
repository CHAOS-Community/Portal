namespace Chaos.Portal.v5.Extension
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chaos.Portal.Core;
    using Chaos.Portal.Core.Data.Model;
    using Chaos.Portal.Core.Exceptions;
    using Chaos.Portal.Core.Extension;

    public class Subscription : AExtension
    {
        #region Initialization

        public Subscription(IPortalApplication portalApplication): base(portalApplication)
        {
        }

        #endregion
        #region Get

        public IEnumerable<Dto.SubscriptionInfo> Get( Guid guid )
        {
            var subscriptionInfos = PortalRepository.SubscriptionGet(guid, Request.User.Guid);

            return subscriptionInfos.Select(subscriptionInfo => new Dto.SubscriptionInfo(subscriptionInfo));
        }

        #endregion
        #region Create

        public Dto.SubscriptionInfo Create( string name )
        {
            if(!Request.User.HasPermission(SystemPermissons.CreateSubscription)) throw new InsufficientPermissionsException();

            var subscriptionInfo = PortalRepository.SubscriptionCreate(new Guid(), name, Request.User.Guid);

            return new Dto.SubscriptionInfo(subscriptionInfo);
        }

        #endregion
        #region Delete

        public ScalarResult Delete(Guid guid)
        {
            var result = (int) PortalRepository.SubscriptionDelete(guid, Request.User.Guid);

            return new ScalarResult(result);
        }

        #endregion
        #region Update

        public ScalarResult Update(Guid guid, string newName)
        {
            var result = PortalRepository.SubscriptionUpdate(guid, newName, Request.User.Guid);

            return new ScalarResult( (int) result );
        }

        #endregion
    }
}
