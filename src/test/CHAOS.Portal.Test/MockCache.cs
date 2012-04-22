using System;
using System.Collections.Generic;
using CHAOS.Portal.Core.Cache;
using CHAOS.Portal.DTO;

namespace CHAOS.Portal.Test
{
    public class MockCache : ICache
    {
        public bool Put(string key, IResult value, TimeSpan timeSpan)
        {
            return true;
        }

        public bool Put(string key, IResult value, DateTime dateTime)
        {
            return true;
        }

        public bool Remove(string key)
        {
            return true;
        }

        public object Get(string key)
        {
            return null;
        }

        public T Get<T>(string key) where T : IResult, new()
        {
            return (T)Get(key);
        }


        public IEnumerable<T> Get<T>(IEnumerable<string> keys) where T : IResult, new()
        {
            throw new NotImplementedException();
        }
    }
}