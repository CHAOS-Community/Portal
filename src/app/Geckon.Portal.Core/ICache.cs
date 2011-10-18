using System;
using Geckon.Portal.Data.Result;
using System.Collections.Generic;

namespace Geckon.Portal.Core
{
    public interface ICache
    {
        bool Put( string key, IResult value, TimeSpan timeSpan );
        bool Put( string key, IResult value, DateTime dateTime );
        bool Remove( string key );
        object Get( string key );
        T Get<T>( string key ) where T: IResult, new();
        IEnumerable<T> Get<T>(IEnumerable<string> keys) where T : IResult, new();
    }
}
