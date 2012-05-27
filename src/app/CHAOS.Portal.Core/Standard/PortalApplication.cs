using System;
using System.Collections.Generic;
using System.Configuration;
using CHAOS.Index;
using CHAOS.Portal.Core.Bindings;
using CHAOS.Portal.Core.Bindings.Standard;
using CHAOS.Portal.Core.Cache;
using CHAOS.Portal.Core.Extension;
using CHAOS.Portal.Core.Module;
using CHAOS.Portal.Exception;

namespace CHAOS.Portal.Core.Standard
{
    public class PortalApplication
    {
        #region Properties

        public IDictionary<Type, IParameterBinding>      Bindings { get; set; }
        public IDictionary<string, IExtension>           LoadedExtensions { get; set; }
        public IDictionary<string, ICollection<IModule>> LoadedModules { get; set; }
        public ICache                                    Cache { get; set; }
        public IIndexManager                             IndexManager { get; set; }
        public string                                    ServiceDirectory { get; set; }

        #endregion
        #region Constructors

        public PortalApplication( ICache cache, IIndexManager indexManager )
        {
            Bindings         = new Dictionary<Type, IParameterBinding>();
            LoadedExtensions = new Dictionary<string, IExtension>();
            LoadedModules    = new Dictionary<string, ICollection<IModule>>();
            Cache            = cache; 
            IndexManager     = indexManager;
            ServiceDirectory = ConfigurationManager.AppSettings["ServiceDirectory"];

            Bindings.Add( typeof(string), new StringParameterBinding() );
            Bindings.Add( typeof(ICallContext), new CallContextParameterBinding() );
            Bindings.Add( typeof(CallContext), new CallContextParameterBinding() );
            Bindings.Add( typeof(long), new ConvertableParameterBinding<long>() );
            Bindings.Add( typeof(int), new ConvertableParameterBinding<int>() );
            Bindings.Add( typeof(short), new ConvertableParameterBinding<short>() );
            Bindings.Add( typeof(ulong), new ConvertableParameterBinding<ulong>() );
            Bindings.Add( typeof(uint), new ConvertableParameterBinding<uint>() );
            Bindings.Add( typeof(ushort), new ConvertableParameterBinding<ushort>() );
            Bindings.Add( typeof(double), new ConvertableParameterBinding<double>() );
            Bindings.Add( typeof(float), new ConvertableParameterBinding<float>() );
            Bindings.Add( typeof(bool), new ConvertableParameterBinding<bool>() );
            Bindings.Add( typeof(DateTime), new DateTimeParameterBinding());
            Bindings.Add( typeof(long?), new ConvertableParameterBinding<long>() );
            Bindings.Add( typeof(int?), new ConvertableParameterBinding<int>() );
            Bindings.Add( typeof(short?), new ConvertableParameterBinding<short>() );
            Bindings.Add( typeof(ulong?), new ConvertableParameterBinding<ulong>() );
            Bindings.Add( typeof(uint?), new ConvertableParameterBinding<uint>() );
            Bindings.Add( typeof(ushort?), new ConvertableParameterBinding<ushort>() );
            Bindings.Add( typeof(double?), new ConvertableParameterBinding<double>() );
            Bindings.Add( typeof(float?), new ConvertableParameterBinding<float>() );
            Bindings.Add( typeof(bool?), new ConvertableParameterBinding<bool>() );
            Bindings.Add( typeof(DateTime?), new DateTimeParameterBinding());
            Bindings.Add( typeof(UUID), new UUIDParameterBinding() );
            Bindings.Add( typeof(IQuery), new QueryParameterBinding() );            
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
