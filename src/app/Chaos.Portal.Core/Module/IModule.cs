namespace Chaos.Portal.Core.Module
{
    using System.Collections.Generic;

    using Extension;

    public interface IModule : IBaseModule
    {
        IEnumerable<string> GetExtensionNames(Protocol version);
        IExtension GetExtension(Protocol version, string name);
        IExtension GetExtension<TExtension>(Protocol version) where TExtension : IExtension;
    }
}