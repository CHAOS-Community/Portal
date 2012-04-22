using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using CHAOS.Portal.DTO;
using CHAOS.Serialization;
using CHAOS.Serialization.Standard;
using Enyim.Caching.Memcached;
using Membase;

namespace CHAOS.Portal.Core.Cache.Membase
{
    public class Membase : MembaseClient, ICache
    {
        #region Fields

        #endregion
        #region Constructors

        #endregion
        #region Business Logic

        public bool Put( string key, IResult value, TimeSpan timeSpan )
        {
            return Store( StoreMode.Set, key, value.ToXML().ToString( SaveOptions.DisableFormatting ), timeSpan );
        }

        public bool Put(string key, IResult value, DateTime dateTime)
        {
            return Store( StoreMode.Set, key, value.ToXML().ToString( SaveOptions.DisableFormatting ), dateTime );
        }

        public new T Get<T>(string key) where T : IResult, new()
        {
            object obj = Get(key);
            
            if( obj == null )
                return default(T);

            ISerializer<XDocument> serializer = SerializerFactory.Get<XDocument>();

            return serializer.Deserialize<T>( XDocument.Parse( (string) obj ), false);
        }

        /// <summary>
        /// Retrieve multiple objects from the caches
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public IEnumerable<T> Get<T>(IEnumerable<string> keys) where T : IResult, new()
        {
            IDictionary<string, object> results = Get( keys );
            ISerializer<XDocument> serializer = SerializerFactory.Get<XDocument>();

            return results.Select( obj => serializer.Deserialize<T>( XDocument.Parse( ( string ) obj.Value ), false) );
        }

        #endregion
    }
}
