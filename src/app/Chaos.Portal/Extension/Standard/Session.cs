namespace Chaos.Portal.Extension.Standard
{
    using Chaos.Portal.Data.Dto;
    using Chaos.Portal.Data.Dto.Standard;
    
    [PortalExtension(configurationName : "Portal")]
    public class Session : AExtension
    {
        #region Initialization

        public override IExtension WithConfiguration(string configuration) { return this; }

        #endregion
        #region Business Logic

        #region Get

        public ISession Get(ICallContext callContext)
        {
            return callContext.Session;
        } 

        #endregion
        #region Create

        public ISession Create( ICallContext callContext )
        {
            return PortalRepository.SessionCreate(callContext.AnonymousUserGuid);
        }

        #endregion
        #region Update

        public ISession Update( ICallContext callContext )
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
