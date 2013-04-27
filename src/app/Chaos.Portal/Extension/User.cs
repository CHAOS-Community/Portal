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

        public UserInfo Get( ICallContext callContext )
        {
            return callContext.User;
        }

        #endregion
		#region Get

		public UserInfo GetCurrent(ICallContext callContext)
		{
			return callContext.User;
		}

		#endregion
    }
}