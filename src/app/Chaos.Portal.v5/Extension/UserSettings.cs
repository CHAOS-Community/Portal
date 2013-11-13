using CHAOS;
using CHAOS.Extensions;

namespace Chaos.Portal.v5.Extension
{
    using Core;
    using Core.Extension;

    public class UserSettings : AExtension
    {
        #region Initialization

        public UserSettings(IPortalApplication portalApplication): base(portalApplication)
        {
        }

        #endregion
        #region Business Logic

        #region Get

        public Dto.UserSettings Get(UUID clientGUID )
        {
            var result = PortalRepository.UserSettingsGet(clientGUID.ToGuid(), Request.User.Guid);

            return new Dto.UserSettings(result);
        }

        #endregion
        #region Set

        public uint Set(UUID clientGUID, string settings)
        {
            var result = PortalRepository.UserSettingsSet(clientGUID.ToGuid(), Request.User.Guid, settings);

            return result;
        }

        #endregion
        #region Delete

        public uint Delete(UUID clientGUID )
        {
            var result = PortalRepository.UserSettingsDelete(clientGUID.ToGuid(), Request.User.Guid);

            return result;
        }

        #endregion

        #endregion
    }
}
