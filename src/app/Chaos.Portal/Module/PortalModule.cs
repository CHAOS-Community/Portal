namespace Chaos.Portal.Module
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;

    using Chaos.Portal.Core;
    using Chaos.Portal.Core.Exceptions;
    using Chaos.Portal.Core.Extension;
    using Chaos.Portal.Core.Module;

    public class PortalModule : IModule
    {
        #region Field

        private IPortalApplication _portalApplication;

        #endregion
        #region Initialization

        public void Load(IPortalApplication portalApplication)
        {
            _portalApplication = portalApplication;
        }

        #endregion
        #region Properties


        #endregion
        #region Business Logic

        public IEnumerable<string> GetExtensionNames(Protocol version)
        {
            yield return "ClientSettings";
            yield return "Group";
            yield return "Session";
            yield return "Subscription";
            yield return "User";
            yield return "UserSettings";
            yield return "View";

        }

        public IExtension GetExtension<TExtension>(Protocol version) where TExtension : IExtension
        {
            return GetExtension(version, typeof(TExtension).Name);
        }

        public IExtension GetExtension(Protocol version, string name)
        {
            if (_portalApplication == null) throw new ConfigurationErrorsException("Load not call on module");

            if (version == Protocol.V5)
            {
                switch (name)
                {
                    case "ClientSettings":
                        return new v5.Extension.ClientSettings(_portalApplication);
                    case "Group":
                        return new v5.Extension.Group(_portalApplication);
                    case "Session":
                        return new v5.Extension.Session(_portalApplication);
                    case "Subscription":
                        return new v5.Extension.Subscription(_portalApplication);
                    case "User":
                        return new v5.Extension.User(_portalApplication);
                    case "UserSettings":
                        return new v5.Extension.UserSettings(_portalApplication);
                    case "View":
                        return new v5.Extension.View(_portalApplication);
                    default:
                        throw new ExtensionMissingException(string.Format("No extension by the name {0}, found on the Portal Module", name));
                }
            }
            
            if (version == Protocol.V6)
            {
                switch (name)
                {
                    case "ClientSettings":
                        return new v6.Extension.ClientSettings(_portalApplication);
                    case "Group":
                        return new v6.Extension.Group(_portalApplication);
                    case "Session":
                        return new v6.Extension.Session(_portalApplication);
                    case "Subscription":
                        return new v6.Extension.Subscription(_portalApplication);
                    case "User":
                        return new v6.Extension.User(_portalApplication);
                    case "UserSettings":
                        return new v6.Extension.UserSettings(_portalApplication);
                    case "View":
                        return new v5.Extension.View(_portalApplication);
                    default:
                        throw new ExtensionMissingException(string.Format("No extension by the name {0}, found on the Portal Module", name));
                }
            }

            throw new VersionNotFoundException("Version not supported by module");
        }

        #endregion
    }
}