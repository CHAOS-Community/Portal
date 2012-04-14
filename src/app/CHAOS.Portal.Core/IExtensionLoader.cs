using System.Reflection;

namespace CHAOS.Portal.Core
{
    public interface IExtensionLoader
    {
        Assembly Assembly { get; }
        string FullName { get; }
    }
}
