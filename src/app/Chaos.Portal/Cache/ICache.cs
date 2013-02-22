namespace Chaos.Portal.Cache
{
    using System;
    using System.Collections.Generic;

    using Chaos.Portal.Cache.Couchbase;

    /// <summary>
    /// The Cache interface.
    /// </summary>
    public interface ICache
    {
        bool Put(string key, ICacheable value, TimeSpan timeSpan);

        bool Put(string key, ICacheable value, DateTime dateTime);

        bool Store(ICacheable value);

        T Get<T>(string key) where T : class, ICacheable;

        IEnumerable<T> Get<T>(IEnumerable<string> keys) where T : ICacheable;
    }
}
