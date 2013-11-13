namespace Chaos.Portal.v5.Extension
{
    using Core;
    using Core.Extension;

    public class User : AExtension
    {
        #region Initialization

        public User(IPortalApplication portalApplication): base(portalApplication)
        {
        }

        #endregion
        #region Get

        public Dto.UserInfo Get()
        {
            return new Dto.UserInfo(Request.User);
        }

        #endregion
    }
}