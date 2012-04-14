using CHAOS.Portal.Core.Request;

namespace CHAOS.Portal.Core
{
    public interface ICallContext
    {
        PortalApplication PortalApplication { get; }
        IPortalRequest    PortalRequest{ get; }
    }
}
