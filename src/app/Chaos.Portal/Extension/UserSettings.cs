namespace Chaos.Portal.Extension
{
    using System;
    using System.Collections.Generic;

    using Chaos.Portal.Data.Dto;

    [PortalExtension(configurationName : "Portal")]
    public class UserSettings : AExtension
    {
        #region Initialization

        public override IExtension WithConfiguration(string configuration) { return this; }

        #endregion
        #region Business Logic

        #region Get

        public IEnumerable<Data.Dto.UserSettings> Get( ICallContext callContext, Guid clientGUID )
        {
            return PortalRepository.UserSettingsGet(clientGUID, callContext.User.Guid);
        }

        #endregion
        #region Set

        public ScalarResult Set( ICallContext callContext, Guid clientGUID, string settings )
        {
            var result = PortalRepository.UserSettingsSet(clientGUID, callContext.User.Guid, settings);

            return new ScalarResult((int) result);
        }

        #endregion
        #region Delete

        public ScalarResult Delete( ICallContext callContext, Guid clientGUID )
        {
            var result = PortalRepository.UserSettingsDelete(clientGUID, callContext.User.Guid);

            return new ScalarResult((int) result);
        }

        #endregion

        #endregion
    }
}
