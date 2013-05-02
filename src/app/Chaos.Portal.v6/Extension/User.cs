namespace Chaos.Portal.v6.Extension
{
    using System.Collections.Generic;
    using System.Linq;

    using Chaos.Portal.Core.Data.Model;
    using Chaos.Portal.Core.Extension;

    public class User : AExtension
    {
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