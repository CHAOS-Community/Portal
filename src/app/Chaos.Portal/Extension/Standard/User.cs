using Chaos.Portal.Data.Dto;

namespace Chaos.Portal.Extension.Standard
{
    [PortalExtension(configurationName : "Portal")]
    public class User : AExtension
    {
        #region Initialization

        public override IExtension WithConfiguration(string configuration) { return this; }

        #endregion
        #region Get

        public IUserInfo Get( ICallContext callContext )
        {
            return callContext.User;
        }

        #endregion

        
    }
}
