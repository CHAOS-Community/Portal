namespace Chaos.Portal.v5.Extension
{
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

        public UserInfo Get()
        {
            return Request.User;
        }

        #endregion
    }
}