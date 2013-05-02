namespace Chaos.Portal.Extension
{
    using System;
    using System.Collections.Generic;

    using Chaos.Portal.Core.Data.Model;
    using Chaos.Portal.Core.Exceptions;

    public class Subscription : AExtension
    {
        #region Get

        public IEnumerable<SubscriptionInfo> Get( Guid guid )
        {
            return PortalRepository.SubscriptionGet(guid, User.Guid);
        }

        #endregion
        #region Create

        public SubscriptionInfo Create( string name )
        {
            if(!User.HasPermission(SystemPermissons.CreateSubscription)) throw new InsufficientPermissionsException();

            return PortalRepository.SubscriptionCreate(new Guid(), name, User.Guid);
        }

        #endregion
        #region Delete

        public ScalarResult Delete(Guid guid)
        {
            var result = (int) PortalRepository.SubscriptionDelete(guid, User.Guid);

            return new ScalarResult(result);
        }

        #endregion
        #region Update

        public ScalarResult Update(Guid guid, string newName)
        {
            var result = PortalRepository.SubscriptionUpdate(guid, newName, User.Guid);

            return new ScalarResult( (int) result );
        }

        #endregion
    }
}
