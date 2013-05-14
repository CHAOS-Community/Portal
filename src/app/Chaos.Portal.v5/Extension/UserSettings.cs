namespace Chaos.Portal.v5.Extension
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chaos.Portal.Core;
    using Chaos.Portal.Core.Data.Model;
    using Chaos.Portal.Core.Extension;

    public class UserSettings : AExtension
    {
        #region Initialization

        public UserSettings(IPortalApplication portalApplication): base(portalApplication)
        {
        }

        #endregion
        #region Business Logic

        #region Get

        public IEnumerable<Dto.UserSettings> Get(Guid clientGUID )
        {
            var userSettings = PortalRepository.UserSettingsGet(clientGUID, Request.User.Guid);
            return userSettings.Select(item => new Dto.UserSettings(item));
        }

        #endregion
        #region Set

        public ScalarResult Set(Guid clientGUID, string settings )
        {
            var result = PortalRepository.UserSettingsSet(clientGUID, Request.User.Guid, settings);

            return new ScalarResult((int) result);
        }

        #endregion
        #region Delete

        public ScalarResult Delete(Guid clientGUID )
        {
            var result = PortalRepository.UserSettingsDelete(clientGUID, Request.User.Guid);

            return new ScalarResult((int) result);
        }

        #endregion

        #endregion
    }
}
