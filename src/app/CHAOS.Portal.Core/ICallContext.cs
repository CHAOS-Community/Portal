using System.IO;
using CHAOS.Portal.Core.Cache;
using CHAOS.Portal.Core.Request;
using CHAOS.Portal.Core.Standard;
using Geckon;
using Geckon.Index;

namespace CHAOS.Portal.Core
{
    public interface ICallContext
    {
        PortalApplication PortalApplication { get; }
        IPortalRequest    PortalRequest{ get; }
        IPortalResponse   PortalResponse { get; }
        ReturnFormat      ReturnFormat{ get; }
        Session           Session { get; }
        UUID              AnonymousUserGUID { get; }
        bool              IsAnonymousUser { get; }
        ICache            Cache{ get; }
        IIndexManager     IndexManager { get; }

        Stream GetResponseStream();
    }
}
