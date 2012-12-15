using System.Collections.Generic;
using CHAOS;
using CHAOS.Index;
using Chaos.Portal.Cache;
using Chaos.Portal.Data.Dto;
using Chaos.Portal.Request;
using Chaos.Portal.Response;

namespace Chaos.Portal
{
    public interface ICallContext
    {
        IPortalApplication             Application { get; }
        IPortalRequest                 Request{ get; }
        IPortalResponse                Response { get; }
        ISession                       Session { get; }
        IEnumerable<ISubscriptionInfo> Subscriptions { get; }
        IEnumerable<IGroup>            Groups { get; }
        IUserInfo                      User { get; }
        UUID                           AnonymousUserGUID { get; }
        bool                           IsAnonymousUser { get; }
        ICache                         Cache{ get; }
        IIndexManager                  IndexManager { get; }
	    
		ISession GetSessionFromDatabase();
    }
}
