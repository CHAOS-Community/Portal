using System.Collections.Generic;
using Geckon.Portal.Core.Extension;
using Geckon.Portal.Core.Module;
using System;
using Geckon.Portal.Data.Result;

namespace Geckon.Portal.Core
{
    public interface IPortalContext
    {
        System.Diagnostics.Stopwatch TimeStamp { get; }
        IDictionary<string, IExtensionLoader> LoadedExtensions { get; }
        IDictionary<string, IModule>          LoadedModules { get;}

        void RegisterExtension( IExtensionLoader extensionLoader );
        void RegisterModule( IModule module );
        T CallModule<T>( IExtension extension, IMethodQuery methodQuery ) where T : IResult;
        T GetModule<T>() where T : IModule;
        IEnumerable<IModule> GetModules( string extension, string action );
    }
}
