namespace Chaos.Portal.Module
{
    using Chaos.Portal.Extension;

    public class PortalModule : IModule
    {
        #region Field

        private const string CONFIGURATION_NAME = "Portal";

        #endregion
        #region Initialization

        public void Load(IPortalApplication portalApplication)
        {
            Extensions = new IExtension[7];
            Extensions[0] = new ClientSettings();
            Extensions[1] = new Group();
            Extensions[2] = new Session();
            Extensions[3] = new Subscription();
            Extensions[4] = new User();
            Extensions[5] = new UserSettings();
            Extensions[6] = new View();

            var configuration = portalApplication.PortalRepository.ModuleGet(CONFIGURATION_NAME);
            
            foreach(var extension in Extensions)
            {
                extension.WithPortalApplication(portalApplication);

                if (configuration != null) extension.WithConfiguration(configuration.Configuration);
            }

            portalApplication.AddExtension("ClientSettings", Extensions[0]);
            portalApplication.AddExtension("Group", Extensions[1]);
            portalApplication.AddExtension("Session", Extensions[2]);
            portalApplication.AddExtension("Subscription", Extensions[3]);
            portalApplication.AddExtension("User", Extensions[4]);
            portalApplication.AddExtension("UserSettings", Extensions[5]);
            portalApplication.AddExtension("View", Extensions[6]);
        }

        #endregion

        #region Properties

        public IExtension[] Extensions { get; private set; }

        #endregion
    }
}