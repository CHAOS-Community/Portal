using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHAOS.Portal.DTO;
using Couchbase;
using Couchbase.Extensions;
using Enyim.Caching.Memcached;
using Newtonsoft.Json;

namespace CHAOS.Portal.Core.Cache.Couchbase
{
    public class Couchbase : ICache
    {
        #region Constructors

        /// <summary>
        /// Initializes static members of the <see cref="Cache"/> class.
        /// </summary>
        public Couchbase(ICouchbaseClient client)
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

        public bool Put(string key, IResult value, TimeSpan timeSpan)
        {
            return Client.Store(StoreMode.Set, key, JsonConvert.SerializeObject(value), timeSpan);
        }

        public bool Put(string key, IResult value, DateTime dateTime)
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

        public bool Remove(string key)
        {
            return Client.Remove(key);
        }

        public object Get(string key)
        {
            return Client.Get(key);
        }

        public T Get<T>(string key) where T : IResult, new()
        {
            return Get<T>(new[] {key}).First();
        }

        public IEnumerable<T> Get<T>(IEnumerable<string> keys) where T : IResult, new()
        {
            return Client.Get(keys).Select(item => JsonConvert.DeserializeObject<T>(item.Value.ToString()));
        }
    }
}
