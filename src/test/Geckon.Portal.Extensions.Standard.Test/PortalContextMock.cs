using System;
using System.Collections.Generic;
using Geckon.Portal.Core;
using Geckon.Portal.Core.Extension;
using Geckon.Portal.Core.Module;
using Geckon.Portal.Data.Result;

namespace Geckon.Portal.Extensions.Standard.Test
{
    public class PortalContextMock : IPortalContext
    {
        public ICache Cache
        {
            get { return new MockCache(); }
        }

        public ISolr Solr
        {
            get { return new MockSolr(); }
        }

        public IDictionary<string, IExtensionLoader> LoadedExtensions
        {
            get { throw new NotImplementedException(); }
        }

        public IDictionary<string, IModule> LoadedModules
        {
            get { throw new NotImplementedException(); }
        }

        public void RegisterExtension(IExtensionLoader extensionLoader)
        {
            throw new NotImplementedException();
        }

        public void RegisterModule( IModule module )
        {
            throw new NotImplementedException();
        }

        public IEnumerable< IResult > CallModules(IExtension extension, IMethodQuery methodQuery)
        {
            yield break;
        }

        public T CallModule<T>(IExtension extension, IMethodQuery methodQuery) where T : IResult
        {
            throw new NotImplementedException();
        }

        public Guid AnonymousUserGUID
        {
            get { return new Guid("C0B231E9-7D98-4F52-885E-AF4837FAA352"); }
        }

        public T GetModule<T>() where T : IModule
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IModule> GetModules(string extension, string action)
        {
            throw new NotImplementedException();
        }

        public IExtensionLoader GetExtension(string extensionName)
        {
            throw new NotImplementedException();
        }

        public bool IsExtensionLoaded(string extensionName)
        {
            throw new NotImplementedException();
        }
    }

    public class MockSolr : ISolr
    {
    }

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
            return (T) Get(key);
        }
    }
}