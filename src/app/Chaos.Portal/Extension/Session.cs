namespace Chaos.Portal.Extension
{
    using Chaos.Portal.Data.Dto;

    [PortalExtension(configurationName : "Portal")]
    public class Session : AExtension
    {
        #region Initialization

        public override IExtension WithConfiguration(string configuration) { return this; }

        #endregion
        #region Business Logic

        #region Get

        public Data.Dto.Session Get(ICallContext callContext)
        {
            return callContext.Session;
        } 

        #endregion
        #region Create

        public Data.Dto.Session Create( ICallContext callContext )
        {
            return PortalRepository.SessionCreate(callContext.AnonymousUserGuid);
        }

        #endregion
        #region Update

        public Data.Dto.Session Update( ICallContext callContext )
        {
            return PortalRepository.SessionUpdate(callContext.Session.Guid, callContext.User.Guid);
        }

        #endregion
        #region Delete

        public ScalarAResult Delete( ICallContext callContext )
        {
            var result = PortalRepository.SessionDelete(callContext.Session.Guid, callContext.User.Guid);

            return new ScalarAResult((int) result);
        }

        #endregion

        #endregion
    }
}
