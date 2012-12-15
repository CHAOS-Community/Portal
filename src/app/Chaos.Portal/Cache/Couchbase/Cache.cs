using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using CHAOS.Serialization.Standard;
using Chaos.Portal.Data.Dto;
using Couchbase;
using Enyim.Caching.Memcached;

namespace Chaos.Portal.Cache.Couchbase
{
    public class Cache : ICache
    {
        #region Properties

        private static ICouchbaseClient Client { get; set; }

        #endregion
        #region Constructors

        static Cache()
        {
            Client = new CouchbaseClient();
        }

        #endregion
        #region Business Logic

        public bool Put( string key, IResult value, TimeSpan timeSpan )
        {
            var xml = SerializerFactory.XMLSerializer.Serialize(value, false);
            
            return Client.Store(StoreMode.Set, key, xml.ToString(SaveOptions.None), timeSpan);
        }

        public bool Put(string key, IResult value, DateTime dateTime)
        {
            var xml = SerializerFactory.XMLSerializer.Serialize(value, false);

            return Client.Store(StoreMode.Set, key, xml.ToString(SaveOptions.None), dateTime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key) where T : IResult, new()
        {
            var get = (string) Client.Get(key);

            if (get == null)
                return default(T);

            return SerializerFactory.XMLSerializer.Deserialize<T>(XDocument.Parse(get), false);
        }

        /// <summary>
        /// Retrieve multiple objects from the caches
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public IEnumerable<T> Get<T>(IEnumerable<string> keys) where T : IResult, new()
        {
            return Client.Get(keys).Select(item => SerializerFactory.XMLSerializer.Deserialize<T>(XDocument.Parse((string) item.Value), false));
        }

        #endregion
    }
}
