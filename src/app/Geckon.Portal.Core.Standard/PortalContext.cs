using System.Collections.Generic;
using Geckon.Portal.Core.Extension;
using Geckon.Portal.Core.Module;
using System;
using Geckon.Portal.Data.Result;
using System.Diagnostics;

namespace Geckon.Portal.Core.Standard
{
    public class PortalContext : IPortalContext
    {
        #region Fields

        #endregion
        #region Properties

        public IDictionary<string, IExtensionLoader> LoadedExtensions { get; protected set; }
        public IDictionary<string, IModule>          LoadedModules    { get; protected set; }

        #endregion
        #region Constructors

        public PortalContext()
        {
            LoadedExtensions = new Dictionary<string, IExtensionLoader>();
            LoadedModules    = new Dictionary<string, IModule>();
        }

        #endregion
        #region Business Logic

        public void RegisterExtension( IExtensionLoader extension )
        {
            LoadedExtensions.Add( extension.Extension.Map,extension );
        }

        public IExtensionLoader GetExtension(string extensionName)
        {
            return LoadedExtensions[extensionName];
        }

        public bool IsExtensionLoaded(string extensionName)
        {
            return LoadedExtensions.ContainsKey(extensionName);
        }

        public void RegisterModule( IModule module )
        {
            LoadedModules.Add( module.Name, module );
        }

        public T CallModule<T>( IExtension extension, IMethodQuery methodQuery ) where T : IResult
        {
            methodQuery.Parameters.Add( "extension", new Parameter( "extension", extension ) );

            return (T) LoadedModules[ typeof(T).FullName ].InvokeMethod( methodQuery );
        }

        public T GetModule<T>( ) where T : IModule
        { 
            return (T) LoadedModules[ typeof(T).FullName ];
        }

        public IEnumerable<IModule> GetModules( string extension, string action )
        {
            foreach( IModule module in LoadedModules.Values )
            {
                if( !module.ContainsServiceHook( extension, action ) )
                    continue;

                yield return module;
            }
        }

        #endregion
    }
}
