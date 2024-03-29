namespace Chaos.Portal.v6.Extension
{
    using Chaos.Portal.Core;
    using Chaos.Portal.Core.Data.Model;
    using Chaos.Portal.Core.Extension;

    public class Session : AExtension
    {
        #region Initialization

        public Session(IPortalApplication portalApplication): base(portalApplication)
        {
        }

        #endregion
        #region Business Logic

        #region Get

        public Core.Data.Model.Session Get()
        {
            return Request.Session;
        } 

        #endregion
        #region Create

        public Core.Data.Model.Session Create()
        {
            return PortalRepository.SessionCreate(Request.AnonymousUserGuid);
        }

        #endregion
        #region Update

        public Core.Data.Model.Session Update()
        {
            return PortalRepository.SessionUpdate(Request.Session.Guid, Request.User.Guid);
        }

        #endregion
        #region Delete

        public ScalarResult Delete()
        {
            var result = PortalRepository.SessionDelete(Request.Session.Guid, Request.User.Guid);

            return new ScalarResult((int) result);
        }

        #endregion

        #endregion
    }
}
