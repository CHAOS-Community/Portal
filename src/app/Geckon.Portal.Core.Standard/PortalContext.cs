using System.Collections.Generic;
using System.Configuration;
using Geckon.Portal.Core.Extension;
using Geckon.Portal.Core.Module;
using Geckon.Serialization.Xml;
using System;

namespace Geckon.Portal.Core.Standard
{
    public class PortalContext : IPortalContext
    {
        #region Fields

        #endregion
        #region Properties

        public IDictionary<string, IModule> LoadedModules { get; protected set; }
        public ICache                       Cache         { get; private set; }
        public ISolr                        Solr          { get; private set; }

        public Guid AnonymousUserGUID
        {
            get
            {
                string guid = ( string ) Cache.Get( "AnonymousUserGUID" );
                
                if( string.IsNullOrEmpty( guid ) )
                    guid = ConfigurationManager.AppSettings["AnonymousUserGUID"];

                return new Guid( guid );
            }
        }

        #endregion
        #region Constructors

        public PortalContext()
        {
            LoadedModules = new Dictionary<string, IModule>();
            Cache         = new Membase();
        }

        #endregion
        #region Business Logic

        public void RegisterModule( IModule module )
        {
            LoadedModules.Add( module.Name, module );
        }

        public IEnumerable<XmlSerialize> CallModules( IExtension extension, IMethodQuery methodQuery )
        {
            methodQuery.Parameters.Add( "extension", new Parameter( "extension", extension ) );

            foreach( IModule module in LoadedModules.Values )
            {
                if( !module.ContainsMethodSignature( methodQuery ) )
                    continue;

                yield return module.InvokeMethod( methodQuery );
            }
        }

        public T CallModule<T>( IExtension extension, IMethodQuery methodQuery ) where T : XmlSerialize
        {
            methodQuery.Parameters.Add( "extension", new Parameter( "extension", extension ) );

            return (T) LoadedModules[ typeof(T).FullName ].InvokeMethod( methodQuery );
        }

        public T GetModule<T>( ) where T : IModule
        {
            return (T) LoadedModules[ typeof(T).FullName ];
        }

        #endregion
    }
}
