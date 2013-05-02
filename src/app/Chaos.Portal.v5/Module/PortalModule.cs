namespace Chaos.Portal.v5.Module
{
    using System.Collections.Generic;
    using System.Configuration;

    using Chaos.Portal.Core;
    using Chaos.Portal.Core.Exceptions;
    using Chaos.Portal.Core.Extension;
    using Chaos.Portal.Core.Module;
    using Chaos.Portal.v5.Extension;

    using User = Chaos.Portal.v5.Extension.User;

    public class PortalModule : IModule
    {
        public IPortalApplication PortalApplication { get; private set; }

        #region Implementation of IModule

        public IEnumerable<string> GetExtensionNames()
        {
            throw new System.NotImplementedException();
        }

        public IExtension GetExtension(string name)
        {
            if (PortalApplication == null) throw new ConfigurationErrorsException("Load not call on module");

            switch (name)
            {
                case "ClientSettings":
                    return new ClientSettings().WithPortalApplication(PortalApplication);
                case "Group":
                    return new Group().WithPortalApplication(PortalApplication);
                case "Session":
                    return new Session().WithPortalApplication(PortalApplication);
                case "Subscription":
                    return new Subscription().WithPortalApplication(PortalApplication);
                case "User":
                    return new User().WithPortalApplication(PortalApplication);
                case "UserSettings":
                    return new UserSettings().WithPortalApplication(PortalApplication);
                case "View":
                    return new View().WithPortalApplication(PortalApplication);
                default:
                    throw new ExtensionMissingException(string.Format("No extension by the name {0}, found on the Portal Module", name));
            }
        }

        public IExtension GetExtension<TExtension>() where TExtension : IExtension
        {
            throw new System.NotImplementedException();
        }

        public void Load(IPortalApplication portalApplication)
        {
            PortalApplication = portalApplication;
        }

        #endregion
    }
}