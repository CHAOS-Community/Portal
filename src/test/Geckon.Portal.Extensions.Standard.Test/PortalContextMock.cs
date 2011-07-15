using System;
using System.Collections.Generic;
using Geckon.Portal.Core;
using Geckon.Portal.Core.Extension;
using Geckon.Portal.Core.Module;
using Geckon.Serialization.Xml;

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
            get { throw new NotImplementedException(); }
        }

        public void RegisterModule( IModule module )
        {
            throw new NotImplementedException();
        }

        public IEnumerable<XmlSerialize> CallModules(IExtension extension, IMethodQuery methodQuery)
        {
            yield break;
        }

        public T CallModule<T>(IExtension extension, IMethodQuery methodQuery) where T : XmlSerialize
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
    }

    public class MockCache : ICache
    {
        public bool Put(string key, object value, TimeSpan timeSpan)
        {
            return true;
        }

        public bool Put(string key, object value, DateTime dateTime)
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

        public T Get<T>(string key) where T : XmlSerialize, new()
        {
            return (T) Get(key);
        }
    }
}