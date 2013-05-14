namespace Chaos.Portal.v6.Extension
{
    using System;
    using System.Collections.Generic;

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

        public IEnumerable<SubscriptionInfo> Get( Guid guid )
        {
            return PortalRepository.SubscriptionGet(guid, Request.User.Guid);
        }

        #endregion
        #region Create

        public SubscriptionInfo Create( string name )
        {
            if(!Request.User.HasPermission(SystemPermissons.CreateSubscription)) throw new InsufficientPermissionsException();

            return PortalRepository.SubscriptionCreate(new Guid(), name, Request.User.Guid);
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
