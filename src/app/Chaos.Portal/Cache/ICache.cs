namespace Chaos.Portal.Cache
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The Cache interface.
    /// </summary>
    public interface ICache
    {
        bool Store(string key, object value, TimeSpan timeSpan);

        bool Store(string key, object value, DateTime dateTime);

        bool Store(string key, object value);

        T Get<T>(string key) where T : class;

        IEnumerable<T> Get<T>(IEnumerable<string> keys) where T : class;
    }
}
