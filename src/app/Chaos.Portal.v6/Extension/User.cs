namespace Chaos.Portal.v6.Extension
{
    using System.Collections.Generic;
    using System.Linq;

    using Chaos.Portal.Core;
    using Chaos.Portal.Core.Data.Model;
    using Chaos.Portal.Core.Extension;

    public class User : AExtension
    {
        #region Initialization

        public User(IPortalApplication portalApplication): base(portalApplication)
        {
        }

        #endregion
        #region Get

        public IEnumerable<UserInfo> Get()
        {
            if (Request.User.SystemPermissonsEnum.HasFlag(SystemPermissons.UserManager))
				return PortalRepository.UserInfoGet(null, null, null);

            var result = PortalRepository.UserInfoGetWithGroupPermission(Request.User.Guid);

            return result.Any() ? result : new[] { Request.User };
        }

		public UserInfo GetCurrent()
		{
            return Request.User;
		}


        #endregion
    }
}