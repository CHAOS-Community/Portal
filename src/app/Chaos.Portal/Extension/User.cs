using System.Collections.Generic;

namespace Chaos.Portal.Extension
{
    using Core.Data.Model;

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