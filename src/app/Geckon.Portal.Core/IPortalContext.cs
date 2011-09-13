using System.Collections.Generic;
using Geckon.Portal.Core.Extension;
using Geckon.Portal.Core.Module;
using System;
using Geckon.Portal.Data.Result;

namespace Geckon.Portal.Core
{
    public interface IPortalContext
    {
        void RegisterModule( IModule module );
        T CallModule<T>( IExtension extension, IMethodQuery methodQuery ) where T : IResult;
        Guid AnonymousUserGUID { get; }
        ICache Cache { get; }
        ISolr Solr { get; }
        T GetModule<T>() where T : IModule;
        IEnumerable<IModule> GetModules( string extension, string action );
    }
}
