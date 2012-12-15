using CHAOS.Extensions;
using Chaos.Portal.Data.Dto;
using Chaos.Portal.Data.Dto.Standard;

namespace Chaos.Portal.Extension.Standard
{
    [Extension(configurationName : "Portal")]
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
            return PortalRepository.SessionCreate(callContext.User.GUID.ToGuid());
        }

        #endregion
        #region Update

        public ISession Update( ICallContext callContext )
        {
            return PortalRepository.SessionUpdate(callContext.Session.GUID.ToGuid(), callContext.User.GUID.ToGuid());
        }

        #endregion
        #region Delete

        public ScalarResult Delete( ICallContext callContext )
        {
            var result = PortalRepository.SessionDelete(callContext.Session.GUID.ToGuid(), callContext.User.GUID.ToGuid());

            return new ScalarResult((int) result);
        }

        #endregion

        #endregion
    }
}
