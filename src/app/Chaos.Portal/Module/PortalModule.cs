namespace Chaos.Portal.Module
{
    using Chaos.Portal.Extension;
    using Chaos.Portal.Extension.Standard;

    public class PortalModule : IModule
    {
        #region Initialization

        public PortalModule()
        {
            Extensions = new IExtension[6];
            Extensions[0] = new ClientSettings();
            Extensions[1] = new Group();
            Extensions[2] = new Session();
            Extensions[3] = new Subscription();
            Extensions[4] = new User();
            Extensions[5] = new UserSettings();
        }

        public IModule WithPortalApplication(IPortalApplication portalApplication)
        {
            foreach(var extension in Extensions)
            {
                extension.WithPortalApplication(portalApplication);
            }

            return this;
        }

        #endregion

        #region Properties

        public IExtension[] Extensions { get; private set; }

        #endregion
    }

    public interface IModule
    {
        IModule WithPortalApplication(IPortalApplication portalApplication);

        IExtension[] Extensions { get; }
    }
}