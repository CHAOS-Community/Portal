namespace Chaos.Portal.Cache.Couchbase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    using Chaos.Portal.Data.Dto;

    using CHAOS.Serialization.Standard;

    using global::Couchbase;

    using Enyim.Caching.Memcached;

    /// <summary>
    /// Couchbase caching.
    /// </summary>
    public class Cache : ICache
    {
        #region Constructors

        /// <summary>
        /// Initializes static members of the <see cref="Cache"/> class.
        /// </summary>
        static Cache()
        {
            Client = new CouchbaseClient();
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
        public bool Put(string key, IResult value, TimeSpan timeSpan)
        {
            var xml = SerializerFactory.XMLSerializer.Serialize(value, false);
            
            return Client.Store(StoreMode.Set, key, xml.ToString(SaveOptions.None), timeSpan);
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
        public bool Put(string key, IResult value, DateTime dateTime)
        {
            var xml = SerializerFactory.XMLSerializer.Serialize(value, false);

            return Client.Store(StoreMode.Set, key, xml.ToString(SaveOptions.None), dateTime);
        }

        /// <summary>
        /// Used to get a object from the cache
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize to</typeparam>
        /// <param name="key">The key to get from the cache</param>
        /// <returns>The object retrieved from cache</returns>
        public T Get<T>(string key) where T : IResult, new()
        {
            var get = (string)Client.Get(key);

            return get == null ? default(T) : SerializerFactory.XMLSerializer.Deserialize<T>(XDocument.Parse(get), false);
        }

        /// <summary>
        /// Retrieve multiple objects from the caches
        /// </summary>
        /// <typeparam name="T">The type of object to deserialize to</typeparam>
        /// <param name="keys">A list of keys to retrieve from the cache</param>
        /// <returns>An IEnumerable of the returned objects</returns>
        public IEnumerable<T> Get<T>(IEnumerable<string> keys) where T : IResult, new()
        {
            return Client.Get(keys).Select(item => SerializerFactory.XMLSerializer.Deserialize<T>(XDocument.Parse((string) item.Value), false));
        }

        #endregion
    }
}
