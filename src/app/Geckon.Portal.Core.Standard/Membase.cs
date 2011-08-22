using System;
using System.Xml;
using Enyim.Caching.Memcached;
using Geckon.Serialization.Xml;
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
        public bool Put( string key, object value, TimeSpan timeSpan )
        {
            return Store( StoreMode.Set, key, value, timeSpan );
        }

        public bool Put( string key, object value, DateTime dateTime )
        {
            return Store( StoreMode.Set, key, value, dateTime );
        }

        public T Get<T>( string key ) where T : XmlSerialize, new()
        {
            object obj = Get( key );
            
            if( obj == null )
                return null;

            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml( obj.ToString() );

            T result = new T();
            XmlSerialize.FromXML( xDoc.DocumentElement, result );
            return result;
        }

        #endregion
    }
}
