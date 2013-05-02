namespace Chaos.Portal.v5.Extension
{
    using System.Collections.Generic;

    using Chaos.Portal.Core.Data.Model;
    using Chaos.Portal.Extension;

    public class User : AExtension
    {
        #region Get

        public IEnumerable<UserInfo> Get()
        {
	        return PortalRepository.UserInfoGet(null, null, null);
        }

		public UserInfo GetCurrent()
		{
			return User;
		}


        #endregion
    }
}