namespace Chaos.Portal.Extension.Standard
{
    using System;
    using System.Collections.Generic;

    using Chaos.Portal.Data.Dto;
    using Chaos.Portal.Data.Dto.Standard;

    [PortalExtension(configurationName: "Portal")]
    public class Subscription : AExtension
    {
        #region Initialization

        public override IExtension WithConfiguration(string configuration) { return this; }

        #endregion
        #region Get

        public IEnumerable<ISubscriptionInfo> Get( ICallContext callContext, Guid guid )
        {
            return PortalRepository.SubscriptionGet(guid, callContext.User.Guid);
        }

        #endregion
        #region Create

        public ISubscriptionInfo Create( ICallContext callContext, string name )
        {
            return PortalRepository.SubscriptionCreate(new Guid(), name, callContext.User.Guid);
        }

        #endregion
        #region Delete

        public ScalarResult Delete(ICallContext callContext, Guid guid)
        {
            var result = (int) PortalRepository.SubscriptionDelete(guid, callContext.User.Guid);

            return new ScalarResult(result);
        }

        #endregion
        #region Update

        public ScalarResult Update(ICallContext callContext, Guid guid, string newName)
        {
            var result = PortalRepository.SubscriptionUpdate(guid, newName, callContext.User.Guid);

            return new ScalarResult( (int) result );
        }

        #endregion
    }
}
