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
            get { throw new NotImplementedException(); }
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
            get { return new Guid("89ADE138-0384-433E-9CE7-0AFE53313EF2"); }
        }

        public T GetModule<T>() where T : IModule
        {
            throw new NotImplementedException();
        }
    }
}