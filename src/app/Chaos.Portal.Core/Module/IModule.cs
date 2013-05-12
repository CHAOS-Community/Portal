namespace Chaos.Portal.Core.Module
{
    using System.Collections.Generic;

    using Chaos.Portal.Core.Extension;

    public interface IModule
    {
        IEnumerable<string> GetExtensionNames(Protocol version);
        IExtension GetExtension(Protocol version, string name);
        IExtension GetExtension<TExtension>(Protocol version) where TExtension : IExtension;

        void Load(IPortalApplication portalApplication);
    }
}