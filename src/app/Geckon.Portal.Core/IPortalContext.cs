using System.Collections.Generic;
using Geckon.Portal.Core.Extension;
using Geckon.Portal.Core.Module;
using Geckon.Serialization.Xml;
using System;

namespace Geckon.Portal.Core
{
    public interface IPortalContext
    {
        ICache Cache{ get; }
        ISolr  Solr { get; }
        void RegisterModule( IModule module );
        IEnumerable<XmlSerialize> CallModules( IExtension extension, IMethodQuery methodQuery );
        T CallModule<T>( IExtension extension, IMethodQuery methodQuery ) where T : XmlSerialize;
        Guid AnonymousUserGUID { get; }

        T GetModule<T>() where T : IModule;
    }
}
