namespace Chaos.Portal.Extension
{
    using Chaos.Portal.Core.Data.Model;

    [PortalExtension(configurationName : "Portal")]
    public class Session : AExtension
    {
        #region Initialization

        public override IExtension WithConfiguration(string configuration) { return this; }

        #endregion
        #region Business Logic

        #region Get

        public Core.Data.Model.Session Get(ICallContext callContext)
        {
            return callContext.Session;
        } 

        #endregion
        #region Create

        public Core.Data.Model.Session Create( ICallContext callContext )
        {
            return PortalRepository.SessionCreate(callContext.AnonymousUserGuid);
        }

        #endregion
        #region Update

        public Core.Data.Model.Session Update( ICallContext callContext )
        {
            return PortalRepository.SessionUpdate(callContext.Session.Guid, callContext.User.Guid);
        }

        #endregion
        #region Delete

        public ScalarResult Delete( ICallContext callContext )
        {
            var result = PortalRepository.SessionDelete(callContext.Session.Guid, callContext.User.Guid);

            return new ScalarResult((int) result);
        }

        #endregion

        #endregion
    }
}
