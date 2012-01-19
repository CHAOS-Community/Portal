using System;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using Enyim.Caching.Memcached;
using Geckon.Portal.Data.Result;
using Geckon.Serialization;
using Geckon.Serialization.Standard;
using Membase;
using Geckon.Portal.Core.Cache;

namespace Geckon.Portal.Core.Standard
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
            return base.Store( StoreMode.Set, key, value.ToXML().ToString( SaveOptions.DisableFormatting ), timeSpan );
        }

        public bool Put(string key, IResult value, DateTime dateTime)
        {
            return base.Store( StoreMode.Set, key, value.ToXML().ToString( SaveOptions.DisableFormatting ), dateTime );
        }

        public T Get<T>(string key) where T : IResult, new()
        {
            object obj = base.Get(key);
            
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
            IDictionary<string, object> results = base.Get( keys );
            ISerializer<XDocument> serializer = SerializerFactory.Get<XDocument>();

            return results.Select( obj => serializer.Deserialize<T>( XDocument.Parse( ( string ) obj.Value ), false) );
        }

        #endregion
    }
}
