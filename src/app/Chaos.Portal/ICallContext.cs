namespace Chaos.Portal
{
    using System;
    using System.Collections.Generic;

    using Chaos.Portal.Core.Data.Model;
    using Chaos.Portal.Indexing.View;
    using Chaos.Portal.Cache;
    using Chaos.Portal.Request;
    using Chaos.Portal.Response;

    public interface ICallContext
    {
        IPortalApplication             Application { get; }
        IPortalRequest                 Request{ get; }
        IPortalResponse                Response { get; }
        Session                       Session { get; }
        IEnumerable<SubscriptionInfo> Subscriptions { get; }
        IEnumerable<Group>            Groups { get; }
        UserInfo                      User { get; }
        Guid                           AnonymousUserGuid { get; }
        bool                           IsAnonymousUser { get; }
        ICache                         Cache{ get; }
        IViewManager                   ViewManager { get; }
	    
		Session GetSessionFromDatabase();
    }
}
