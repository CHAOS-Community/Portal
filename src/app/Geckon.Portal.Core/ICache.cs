using System;
using Geckon.Serialization.Xml;

namespace Geckon.Portal.Core
{
    public interface ICache
    {
        bool Put( string key, object value, TimeSpan timeSpan );
        bool Put( string key, object value, DateTime dateTime );
        bool Remove( string key );
        object Get( string key );
        T Get<T>( string key ) where T: XmlSerialize, new();
    }
}
