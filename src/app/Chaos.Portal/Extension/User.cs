using System.Collections.Generic;
using System.Linq;

namespace Chaos.Portal.Extension
{
    using Core.Data.Model;

    [PortalExtension(configurationName : "Portal")]
    public class User : AExtension
    {
        #region Initialization

        public override IExtension WithConfiguration(string configuration) { return this; }

        #endregion
        #region Get

        public IEnumerable<UserInfo> Get()
        {
			if (User.SystemPermissonsEnum.HasFlag(SystemPermissons.UserManager))
				return PortalRepository.UserInfoGet(null, null, null);

	        var result = PortalRepository.UserInfoGetWithGroupPermission(User.Guid);

	        return result.Any() ? result : new[] {User};
        }

		public UserInfo GetCurrent()
		{
			return User;
		}


        #endregion
    }
}