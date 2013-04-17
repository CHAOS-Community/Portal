namespace Chaos.Portal.Extension
{
    using Chaos.Portal.Core.Data.Model;

    [PortalExtension(configurationName : "Portal")]
    public class User : AExtension
    {
        #region Initialization

        public override IExtension WithConfiguration(string configuration) { return this; }

        #endregion
        #region Get

        public UserInfo Get( ICallContext callContext )
        {
            return callContext.User;
        }

        #endregion

        
    }
}
