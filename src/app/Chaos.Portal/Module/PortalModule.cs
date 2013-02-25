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
            Extensions[0] = new ClientSettings().WithPortalApplication(portalApplication);
            Extensions[1] = new Group().WithPortalApplication(portalApplication);
            Extensions[2] = new Session().WithPortalApplication(portalApplication);
            Extensions[3] = new Subscription().WithPortalApplication(portalApplication);
            Extensions[4] = new User().WithPortalApplication(portalApplication);
            Extensions[5] = new UserSettings().WithPortalApplication(portalApplication);
            Extensions[6] = new View().WithPortalApplication(portalApplication);

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