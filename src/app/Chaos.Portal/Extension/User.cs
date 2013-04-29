using System.Collections.Generic;

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

        public IEnumerable<UserInfo> Get( ICallContext callContext )
        {
	        return PortalRepository.UserInfoGet(null, null, null);
        }

		public UserInfo GetCurrent(ICallContext callContext)
		{
			return callContext.User;
		}


        #endregion
    }
}