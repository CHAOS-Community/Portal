namespace Chaos.Portal.v5.Extension
{
    using Chaos.Portal.Core.Data.Model;
    using Chaos.Portal.Core.Extension;

    public class Session : AExtension
    {
        #region Business Logic

        #region Get

        public Core.Data.Model.Session Get()
        {
            return Session;
        } 

        #endregion
        #region Create

        public Core.Data.Model.Session Create()
        {
            return PortalRepository.SessionCreate(AnonymousUserGuid);
        }

        #endregion
        #region Update

        public Core.Data.Model.Session Update()
        {
            return PortalRepository.SessionUpdate(Session.Guid, User.Guid);
        }

        #endregion
        #region Delete

        public ScalarResult Delete()
        {
            var result = PortalRepository.SessionDelete(Session.Guid, User.Guid);

            return new ScalarResult((int) result);
        }

        #endregion

        #endregion
    }
}
