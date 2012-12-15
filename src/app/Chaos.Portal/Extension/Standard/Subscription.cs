using System;
using System.Collections.Generic;
using CHAOS.Extensions;
using Chaos.Portal.Data.Dto;
using Chaos.Portal.Data.Dto.Standard;

namespace Chaos.Portal.Extension.Standard
{
    [Extension(configurationName : "Portal")]
    public class Subscription : AExtension
    {
        #region Initialization

        public override IExtension WithConfiguration(string configuration) { return this; }

        #endregion
        #region Get

        public IEnumerable<ISubscriptionInfo> Get( ICallContext callContext, Guid guid )
        {
            return PortalRepository.SubscriptionGet(guid, callContext.User.GUID.ToGuid());
        }

        #endregion
        #region Create

        public ISubscriptionInfo Create( ICallContext callContext, string name )
        {
            return PortalRepository.SubscriptionCreate(new Guid(), name, callContext.User.GUID.ToGuid());
        }

        #endregion
        #region Delete

        public ScalarResult Delete(ICallContext callContext, Guid guid)
        {
            var result = (int) PortalRepository.SubscriptionDelete(guid, callContext.User.GUID.ToGuid());

            return new ScalarResult(result);
        }

        #endregion
        #region Update

        public ScalarResult Update(ICallContext callContext, Guid guid, string newName)
        {
            var result = PortalRepository.SubscriptionUpdate(guid, newName, callContext.User.GUID.ToGuid());

            return new ScalarResult( (int) result );
        }

        #endregion
    }
}
