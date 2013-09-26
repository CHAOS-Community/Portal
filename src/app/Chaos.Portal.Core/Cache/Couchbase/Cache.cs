namespace Chaos.Portal.Core.Cache.Couchbase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Enyim.Caching.Memcached;

    using Newtonsoft.Json;

    using global::Couchbase;
    using global::Couchbase.Extensions;

    /// <summary>
    /// Couchbase caching.
    /// </summary>
    public class Cache : ICache
    {
        #region Constructors

        /// <summary>
        /// Initializes static members of the <see cref="Cache"/> class.
        /// </summary>
        public Cache(ICouchbaseClient client)
        {
            Client = client;
        }

        #endregion
        #region Properties

        /// <summary>
        /// Gets or sets the Client.
        /// </summary>
        private static ICouchbaseClient Client { get; set; }

        #endregion
        #region Business Logic

        /// <summary>
        /// The put.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="timeSpan">
        /// The time span.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Store(string key, object value, TimeSpan timeSpan)
        {
            return Client.Store(StoreMode.Set, key, JsonConvert.SerializeObject(value), timeSpan);
        }

        /// <summary>
        /// The put.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="dateTime">
        /// The date time.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool Store(string key, object value, DateTime dateTime)
        {
            return Client.Store(StoreMode.Set, key, JsonConvert.SerializeObject(value), dateTime);
        }

        /// <summary>
        /// Stores an object in the cahce
        /// </summary>
        /// <param name="key">the key to retrieve by</param>
        /// <param name="value">the value object</param>
        /// <returns></returns>
        public bool Store(string key, object value)
        {
            return Client.StoreJson(StoreMode.Set, key, value);
        }

        /// <summary>
        /// Used to get a object from the cache
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize to</typeparam>
        /// <param name="key">The key to get from the cache</param>
        /// <returns>The object retrieved from cache</returns>
        public T Get<T>(string key) where T : class
        {
            return Client.GetJson<T>(key);
        }

        /// <summary>
        /// Removes a document from the cache
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            Client.Remove(key);
        }

        /// <summary>
        /// Retrieve multiple objects from the caches
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize to</typeparam>
        /// <param name="keys">A list of keys to retrieve from the cache</param>
        /// <returns>An IEnumerable of the returned objects</returns>
        public IEnumerable<T> Get<T>(IEnumerable<string> keys) where T : class 
        {
            return Enumerable.Select<KeyValuePair<string, object>, T>(Client.Get(keys), item => JsonConvert.DeserializeObject<T>(item.Value.ToString()));
        }

        #endregion
    }
}
