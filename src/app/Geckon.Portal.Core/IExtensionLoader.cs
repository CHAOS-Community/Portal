using System.Reflection;

namespace Geckon.Portal.Core
{
    public interface IExtensionLoader
    {
        Assembly       Assembly { get; }
        Data.Extension Extension { get; }
    }
}