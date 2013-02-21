using System;
using System.Collections.Generic;

using Chaos.Portal.Cache;
using Chaos.Portal.Data.Dto;
using Chaos.Portal.Request;
using Chaos.Portal.Response;

namespace Chaos.Portal
{
    using Chaos.Portal.Indexing.View;

    public interface ICallContext
    {
        IPortalApplication             Application { get; }
        IPortalRequest                 Request{ get; }
        IPortalResponse                Response { get; }
        ISession                       Session { get; }
        IEnumerable<ISubscriptionInfo> Subscriptions { get; }
        IEnumerable<IGroup>            Groups { get; }
        IUserInfo                      User { get; }
        Guid                           AnonymousUserGuid { get; }
        bool                           IsAnonymousUser { get; }
        ICache                         Cache{ get; }
        IViewManager                   ViewManager { get; }
	    
		ISession GetSessionFromDatabase();
    }
}
