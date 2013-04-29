namespace Chaos.Portal.Module
{
    using System.Collections.Generic;

    using Chaos.Portal.Extension;

    public interface IModule
    {
        IEnumerable<string> GetExtensionNames();
        IExtension GetExtension(string name);
        IExtension GetExtension<TExtension>() where TExtension : IExtension;

        void Load(IPortalApplication portalApplication);
    }
}