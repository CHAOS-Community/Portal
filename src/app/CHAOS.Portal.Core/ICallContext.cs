using System.Collections.Generic;
using System.IO;
using CHAOS.Index;
using CHAOS.Portal.Core.Cache;
using CHAOS.Portal.Core.Logging;
using CHAOS.Portal.Core.Request;
using CHAOS.Portal.Core.Standard;
using CHAOS.Portal.DTO.Standard;

namespace CHAOS.Portal.Core
{
    public interface ICallContext
    {
        PortalApplication             PortalApplication { get; }
        IPortalRequest                PortalRequest{ get; }
        IPortalResponse               PortalResponse { get; }
        ReturnFormat                  ReturnFormat{ get; }
        Session                       Session { get; }
        IEnumerable<SubscriptionInfo> Subscriptions { get; }
        IEnumerable<Group>            Groups { get; }
        UserInfo                      User { get; }
        UUID                          AnonymousUserGUID { get; }
        bool                          IsAnonymousUser { get; }
        ICache                        Cache{ get; }
        IIndexManager                 IndexManager { get; }
		ILog						  Log{get;}

        Stream GetResponseStream();
    }
}
