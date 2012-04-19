using System.Collections.Generic;
using System.Configuration;
using CHAOS.Portal.Core.Cache;
using CHAOS.Portal.Core.Extension;
using CHAOS.Portal.Core.Extension.Standard;
using CHAOS.Portal.Core.Module;
using CHAOS.Portal.Core.Request;
using CHAOS.Portal.Exception;
using Geckon.Index;

namespace CHAOS.Portal.Core.Standard
{
    public class PortalApplication
    {
        #region Properties

        public ParameterBindings                         Bindings { get; set; }
        public IDictionary<string, IExtension>           LoadedExtensions { get; set; }
        public IDictionary<string, ICollection<IModule>> LoadedModules { get; set; }
        public ICache                                    Cache { get; set; }
        public IIndexManager                             IndexManager { get; set; }
        public string                                    ServiceDirectory { get; set; }

        #endregion
        #region Constructors

        public PortalApplication( ICache cache, IIndexManager indexManager )
        {
            Bindings         = new ParameterBindings();
            LoadedExtensions = new Dictionary<string, IExtension>();
            LoadedModules    = new Dictionary<string, ICollection<IModule>>();
            Cache            = cache; 
            IndexManager     = indexManager;
            ServiceDirectory = ConfigurationManager.AppSettings["ServiceDirectory"];
        }

        #endregion
        #region Business Logic

        public void ProcessRequest( ICallContext callContext )
        {
            IExtension extension;

            if( LoadedExtensions.ContainsKey( callContext.PortalRequest.Extension ) )
                extension = GetExtension( callContext.PortalRequest.Extension );
            else
                extension = new DefaultExtension();

            extension.CallAction( callContext );
        }

        protected virtual IExtension GetExtension( string extension )
        {
            if( !LoadedExtensions.ContainsKey( extension ) )
                throw new ExtensionMissingException( "Extension is not one of the available extensions" );

            return LoadedExtensions[ extension ];
        }

        #endregion
    }
}
