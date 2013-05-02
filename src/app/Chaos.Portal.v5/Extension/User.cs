namespace Chaos.Portal.v5.Extension
{
    using Chaos.Portal.Core.Data.Model;
    using Chaos.Portal.Core.Extension;

    public class User : AExtension
    {
        #region Get

        public UserInfo Get()
        {
            return User;
        }

        #endregion
    }
}