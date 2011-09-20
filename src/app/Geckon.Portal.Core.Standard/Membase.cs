using System;
using System.Xml.Linq;
using Enyim.Caching.Memcached;
using Geckon.Portal.Data.Result;
using Geckon.Portal.Data.Result.Standard;
using Geckon.Serialization;
using Geckon.Serialization.Standard;
using Membase;

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
            ISerializer<XDocument> serializer = SerializerFactory.Get<XDocument>();

            return Store( StoreMode.Set, key, serializer.Serialize( value, false ).ToString( SaveOptions.DisableFormatting ), timeSpan );
        }

        public bool Put( string key, IResult value, DateTime dateTime )
        {
            ISerializer<XDocument> serializer = SerializerFactory.Get<XDocument>();

            return Store( StoreMode.Set, key, serializer.Serialize( value, false ).ToString( SaveOptions.DisableFormatting ), dateTime );
        }

        public T Get<T>( string key ) where T : IResult, new()
        {
            object obj = Get(key);

            if( obj == null )
                return default(T);

            ISerializer<XDocument> serializer = SerializerFactory.Get<XDocument>();

            return serializer.Deserialize<T>( XDocument.Parse( (string) obj ), false); ;
        }

        #endregion
    }
}
