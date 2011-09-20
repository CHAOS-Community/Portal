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
        
        // TODO: Find way of making Cached object unique, so they aren't overwritten by other Extensions
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

            return serializer.Deserialize<T>(XDocument.Parse("<Result Fullname=\"Geckon.Portal.Data.UserInfo\"><SessionID>7210f991-ea0c-4d4f-9245-5cb9868fde3e</SessionID><ID>2</ID><GUID>a0b231e9-7d98-4f52-885e-af4837faa352</GUID><Firstname>Administrator</Firstname><Email>admin@Geckon.com</Email><SystemPermission>-1</SystemPermission></Result>"), false); ;
        }

        #endregion
    }
}
