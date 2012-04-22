using System;
using System.Collections.Generic;
using CHAOS.Portal.DTO;

namespace CHAOS.Portal.Core.Cache
{
    public interface ICache
    {
        bool Put(string key, IResult value, TimeSpan timeSpan);
        bool Put(string key, IResult value, DateTime dateTime);
        bool Remove(string key);
        object Get(string key);
        T Get<T>(string key) where T : IResult, new();
        IEnumerable<T> Get<T>(IEnumerable<string> keys) where T : IResult, new();
    }
}
