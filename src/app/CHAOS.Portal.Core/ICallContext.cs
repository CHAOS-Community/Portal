using System.IO;
using CHAOS.Portal.Core.Request;
using CHAOS.Portal.Core.Standard;

namespace CHAOS.Portal.Core
{
    public interface ICallContext
    {
        PortalApplication PortalApplication { get; }
        IPortalRequest    PortalRequest{ get; }
        IPortalResponse   PortalResponse { get; }
        ReturnFormat      ReturnFormat{ get; }
        
        Stream GetResponseStream();
    }
}
