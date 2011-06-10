using System.Collections.Generic;
using Geckon.Portal.Core.Entrypoint;
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
        IEnumerable<XmlSerialize> CallModules( IEntrypoint entrypoint, IMethodQuery methodQuery );
        Guid AnonymousUserGUID { get; }
    }
}
