using System.Reflection;

namespace Geckon.Portal.Core
{
    public interface IExtensionLoader
    {
        Assembly       Assembly { get; }
        CHAOS.Portal.Data.DTO.Extension Extension { get; }
    }
}