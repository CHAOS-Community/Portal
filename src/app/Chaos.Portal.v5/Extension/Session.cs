namespace Chaos.Portal.v5.Extension
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

        public Dto.Session Get()
        {
            var session = Request.Session;

            return new Dto.Session(session);
        }

        #endregion
        #region Create

        public Dto.Session Create()
        {
            var session = PortalRepository.SessionCreate(Request.AnonymousUserGuid);
            
            return new Dto.Session(session);
        }

        #endregion
        #region Update

        public Dto.Session Update()
        {
            var session = PortalRepository.SessionUpdate(Request.Session.Guid, Request.User.Guid);

            return new Dto.Session(session);
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
