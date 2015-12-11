namespace Chaos.Portal.Core.Request
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using Chaos.Portal.Core.Data.Model;

    public interface IPortalRequest
    {
        string                     Extension { get; }
        string                     Action { get; }
        IDictionary<string,string> Parameters { get; }
		IEnumerable<FileStream>    Files { get; }
        ReturnFormat               ReturnFormat { get; }
        Stopwatch                  Stopwatch { get; }

        Protocol Version { get; set; }

        /// <summary>
        /// Get the current session
        /// </summary>
        Core.Data.Model.Session Session { get; }

        /// <summary>
        /// Get subscriptions associated with the current user
        /// </summary>
        IEnumerable<Core.Data.Model.SubscriptionInfo> Subscriptions { get; }

        UserInfo User { get; }

        /// <summary>
        /// returns the groups the current user is part of, the Groups are cached, changes in the database will not be pickup up during the callContext life.
        /// </summary>
        IEnumerable<Core.Data.Model.Group> Groups { get; }

        /// <summary>
        /// True if current user is anonymous
        /// </summary>
        bool IsAnonymousUser { get; }

        /// <summary>
        /// Get the UUID of the anonymous user
        /// </summary>
        Guid AnonymousUserGuid { get; }

        void ClearCache();
    }
}