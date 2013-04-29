namespace Chaos.Portal.Extension
{
    using System;
    using System.Collections.Generic;

    using Chaos.Portal.Core.Data.Model;

    [PortalExtension(configurationName : "Portal")]
    public class UserSettings : AExtension
    {
        #region Initialization

        public override IExtension WithConfiguration(string configuration) { return this; }

        #endregion
        #region Business Logic

        #region Get

        public IEnumerable<Core.Data.Model.UserSettings> Get(Guid clientGUID )
        {
            return PortalRepository.UserSettingsGet(clientGUID, User.Guid);
        }

        #endregion
        #region Set

        public ScalarResult Set(Guid clientGUID, string settings )
        {
            var result = PortalRepository.UserSettingsSet(clientGUID, User.Guid, settings);

            return new ScalarResult((int) result);
        }

        #endregion
        #region Delete

        public ScalarResult Delete(Guid clientGUID )
        {
            var result = PortalRepository.UserSettingsDelete(clientGUID, User.Guid);

            return new ScalarResult((int) result);
        }

        #endregion

        #endregion
    }
}
