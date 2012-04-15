using System.Reflection;
using CHAOS.Portal.Core.Extension;

namespace CHAOS.Portal.Core
{
    public interface IExtensionLoader
    {
        IExtension CreateInstance();
    }
}
