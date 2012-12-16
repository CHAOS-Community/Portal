using System;
using System.Collections.Generic;
using CHAOS.Extensions;
using Chaos.Portal.Data.Dto;
using Chaos.Portal.Data.Dto.Standard;

namespace Chaos.Portal.Extension.Standard
{
    [PortalExtension(configurationName : "Portal")]
    public class UserSettings : AExtension
    {
        #region Initialization

        public override IExtension WithConfiguration(string configuration) { return this; }

        #endregion
        #region Business Logic

        #region Get

        public IEnumerable<IUserSettings> Get( ICallContext callContext, Guid clientGUID )
        {
            return PortalRepository.UserSettingsGet(clientGUID, callContext.User.GUID.ToGuid());
        }

        #endregion
        #region Set

        public ScalarResult Set( ICallContext callContext, Guid clientGUID, string settings )
        {
            var result = PortalRepository.UserSettingsSet(clientGUID, callContext.User.GUID.ToGuid(), settings);

            return new ScalarResult((int) result);
        }

        #endregion
        #region Delete

        public ScalarResult Delete( ICallContext callContext, Guid clientGUID )
        {
            var result = PortalRepository.UserSettingsDelete(clientGUID, callContext.User.GUID.ToGuid());

            return new ScalarResult((int) result);
        }

        #endregion

        #endregion
    }
}
