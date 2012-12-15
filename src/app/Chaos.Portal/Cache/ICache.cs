using System;
using System.Collections.Generic;
using CHAOS.Portal.DTO;

namespace Chaos.Portal.Cache
{
    public interface ICache
    {
        bool Put(string key, IResult value, TimeSpan timeSpan);
        bool Put(string key, IResult value, DateTime dateTime);
        T Get<T>(string key) where T : IResult, new();
        IEnumerable<T> Get<T>(IEnumerable<string> keys) where T : IResult, new();
    }
}
