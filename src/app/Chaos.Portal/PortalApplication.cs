namespace Chaos.Portal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    using Chaos.Portal.Bindings;
    using Chaos.Portal.Bindings.Standard;
    using Chaos.Portal.Cache;
    using Chaos.Portal.Core.Data;
    using Chaos.Portal.Core.Exceptions;
    using Chaos.Portal.Extension;
    using Chaos.Portal.Indexing.Solr;
    using Chaos.Portal.Indexing.View;
    using Chaos.Portal.Logging;
    using Chaos.Portal.Module;
    using Chaos.Portal.Request;
    using Chaos.Portal.Response;
    using Chaos.Portal.Response.Dto;
    using Chaos.Portal.Response.Specification;

    /// <summary>
    /// The portal application.
    /// </summary>
    public class PortalApplication : IPortalApplication
    {
        #region Fields

        private readonly ILogFactory _loggingFactory;

        private static readonly IDictionary<ReturnFormat, IResponseSpecification> ResponseSpecifications = new Dictionary<ReturnFormat, IResponseSpecification>();

        #endregion
        #region Properties

        public IDictionary<Type, IParameterBinding> Bindings { get; set; }
        public IDictionary<string, IModule>         LoadedModules { get; set; }
        public ICache                               Cache { get; protected set; }
        public IViewManager                         ViewManager { get; protected set; }
        public ILog                                 Log { get; protected set; }
        public IPortalRepository                    PortalRepository { get; set; }

        

        #endregion
        #region Constructors

        static PortalApplication()
        {
            ResponseSpecifications.Add(ReturnFormat.XML, new XmlResponse());
            ResponseSpecifications.Add(ReturnFormat.JSON, new JsonResponse());
            ResponseSpecifications.Add(ReturnFormat.JSONP, new JsonpResponse());
            ResponseSpecifications.Add(ReturnFormat.ATTACHMENT, new StreamResponse());
        }

        public PortalApplication( ICache cache, IViewManager viewManager, IPortalRepository portalRepository, ILogFactory loggingFactory )
        {
            LoadedModules     = new Dictionary<string, IModule>();
            Bindings         = new Dictionary<Type, IParameterBinding>();
            Log              = new DirectLogger(loggingFactory).WithName("Portal Application");
            Cache            = cache;
            ViewManager      = viewManager;
            PortalRepository = portalRepository;
            _loggingFactory  = loggingFactory;
            
            // Load bindings
            Bindings.Add( typeof(string), new StringParameterBinding() );
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
            Bindings.Add( typeof(Guid), new GuidParameterBinding() );
            Bindings.Add( typeof(Guid?), new GuidParameterBinding() );
            Bindings.Add( typeof(IQuery), new QueryParameterBinding() );
            Bindings.Add( typeof(IEnumerable<Guid>), new EnumerableOfGuidParameterBinding());
            Bindings.Add( typeof(XDocument), new XDocumentBinding() );
        }

        #endregion
        #region Business Logic

        /// <summary>
        /// Process a request to portal. Any underlying extensions or modules will be called based on the callContext parameter
        /// </summary>
        /// <param name="request">contains information about what extension and action to call</param>
        /// <returns>The response object</returns>
        public IPortalResponse ProcessRequest( IPortalRequest request )
        {
            var response = new PortalResponse(new PortalHeader(request.Stopwatch, System.Text.Encoding.UTF8), new PortalResult(), new PortalError() );
            response.WithResponseSpecification(ResponseSpecifications[request.ReturnFormat]);
            response.Header.ReturnFormat = request.ReturnFormat;
            response.Header.Callback     = request.Parameters.ContainsKey("callback") ? request.Parameters["callback"] : null;
            
            var extension   = GetExtension(request.Extension);
            extension.WithPortalRequest(request);
            extension.WithPortalResponse(response);

            return extension.CallAction(request);
        }

        /// <summary>
        /// Return the loaded instance of the requested extension
        /// </summary>
        /// <typeparam name="TExtension">The type of extension to get</typeparam>
        /// <returns>The loaded the instance of the extension</returns>
        public TExtension GetExtension<TExtension>() where TExtension : IExtension, new()
        {
            // todo: optimize cross extension calls, this is relatively slow
            var modules = LoadedModules.Values.Distinct().FirstOrDefault(item => item.GetExtension<TExtension>() != null);

            if (modules == null) throw new ExtensionMissingException(string.Format("Extension not found"));

            return (TExtension)modules.GetExtension<TExtension>();
        }

        public TResult GetModule<TResult>() where TResult : IModule
        {
            return (TResult)LoadedModules[typeof(TResult).FullName];
        }

        public void AddModule(IModule module)
        {
            module.Load(this);

            foreach (var extensionName in module.GetExtensionNames())
            {
                LoadedModules.Add(extensionName, module);
            }
        }

        /// <summary>
        /// The get extension.
        /// </summary>
        /// <param name="extension">The key associated with the extension.</param>
        /// <returns>The instance of<see cref="IExtension"/>.</returns>
        /// <exception cref="ExtensionMissingException">Is thrown if the extension is not loaded</exception>
        public IExtension GetExtension(string extension)
        {
            if(extension == null || !LoadedModules.ContainsKey( extension ))
                throw new ExtensionMissingException( string.Format( "Extension named '{0}' not found", extension ) );

            var module = LoadedModules[extension];

            return module.GetExtension(extension);
        }

        #endregion
    }
}
