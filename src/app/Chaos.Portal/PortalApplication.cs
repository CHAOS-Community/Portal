using System.Configuration;
using Chaos.Portal.Core.EmailService;
using Chaos.Portal.EmailService;

namespace Chaos.Portal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    using CHAOS;
    using CHAOS.Net;
    using Core;
    using Core.Bindings;
    using Core.Bindings.Standard;
    using Core.Cache;
    using Core.Data;
    using Core.Exceptions;
    using Core.Extension;
    using Core.Indexing;
    using Core.Indexing.Solr;
    using Core.Indexing.View;
    using Core.Logging;
    using Core.Module;
    using Core.Request;
    using Core.Response;

    /// <summary>
    /// The portal application.
    /// </summary>
    public class PortalApplication : IPortalApplication
    {
        #region Events

        private ApplicationDelegates.ModuleHandler _onModuleLoaded;

        public event ApplicationDelegates.ModuleHandler OnModuleLoaded
        {
            add
            {
                _onModuleLoaded = (ApplicationDelegates.ModuleHandler)Delegate.Combine(_onModuleLoaded, value);

                InvokeForAllLoadedModules(value);
            }
            remove
            {
                _onModuleLoaded = (ApplicationDelegates.ModuleHandler)Delegate.Remove(_onModuleLoaded, value);
            }
        }

        private void InvokeForAllLoadedModules(ApplicationDelegates.ModuleHandler value)
        {
            foreach (var module in ExtensionInvoker.LoadedModules.Values)
            {
                value.Invoke(this, new ApplicationDelegates.ModuleArgs(module));
            }
        }

        #endregion
        #region Fields

        private readonly ILogFactory _loggingFactory;
	    private IEmailService _emailService;

        #endregion
        #region Properties

	    public IDictionary<Type, IParameterBinding>  Bindings { get; set; }
        public ICache                                Cache { get; protected set; }
        public IViewManager                          ViewManager { get; protected set; }
        public ILog                                  Log { get; protected set; }
        public IPortalRepository                     PortalRepository { get; set; }

        public IEnumerable<string> RegisteredEndpoints
        {
            get { return ExtensionInvoker.Endpoints.Keys; }
        }

        internal Invoker ExtensionInvoker { get; set; }

        #region Email

        public IEmailService EmailService
		{
			get
			{
				if (_emailService != null) return _emailService;

				if (ConfigurationManager.AppSettings.GetValues("AWSKey") == null || ConfigurationManager.AppSettings.GetValues("AWSSecret") == null)
					throw new Exception("AWSKey and AWSSecret not set in app config");

				_emailService = new EmailService.EmailService(new AWSEmailSender(ConfigurationManager.AppSettings["AWSKey"],
																				ConfigurationManager.AppSettings["AWSSecret"]));

				return _emailService;
			}
		}

        #endregion
        #endregion
        #region Constructors

        public PortalApplication( ICache cache, IViewManager viewManager, IPortalRepository portalRepository, ILogFactory loggingFactory )
        {
            
            ExtensionInvoker = new Invoker();
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
            Bindings.Add( typeof(UUID), new UUIDParameterBinding() );
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
            return ExtensionInvoker.Invoke(request);
        }

        public void AddModule(IModule module)
        {
            module.Load(this);

            foreach (var extensionName in module.GetExtensionNames(Protocol.V5))
            {
                if (!ExtensionInvoker.LoadedModules.ContainsKey(extensionName))
                    MapRoute(string.Format("/v5/{0}", extensionName), () => module.GetExtension(Protocol.V5, extensionName));
            }

            foreach (var extensionName in module.GetExtensionNames(Protocol.V6))
            {
                if (!ExtensionInvoker.LoadedModules.ContainsKey(extensionName))
                    MapRoute(string.Format("/v6/{0}", extensionName), () => module.GetExtension(Protocol.V6, extensionName));
            }

            OnOnModuleLoaded(new ApplicationDelegates.ModuleArgs(module));
        }

        public void AddModule(IModuleConfig module)
        {
            module.Load(this);

            OnOnModuleLoaded(new ApplicationDelegates.ModuleArgs(module));
        }

        public void AddView(IView view, string coreName = null)
        {
            view.WithPortalApplication(this);
            view.WithCache(Cache);
            view.WithIndex(new SolrCore(new HttpConnection(ConfigurationManager.AppSettings["SOLR_URL"]), string.IsNullOrEmpty(coreName) ? view.Name : coreName));
            ViewManager.AddView(view);
        }

        protected virtual void OnOnModuleLoaded(ApplicationDelegates.ModuleArgs args)
        {
            ExtensionInvoker.LoadedModules.Add(args.Module.GetType().FullName, args.Module);

            var handler = _onModuleLoaded;
            if (handler != null) handler(this, args);
        }

        public void MapRoute(string path, Func<IExtension> func)
        {
            ExtensionInvoker.Endpoints.Add(path.ToLower(), func);
        }

        #endregion

        internal class Invoker
        {
            public IDictionary<string, Func<IExtension>> Endpoints { get; set; }
            public IDictionary<string, IBaseModule> LoadedModules { get; set; }

            public Invoker()
            {
                Endpoints = new Dictionary<string, Func<IExtension>>();
                LoadedModules = new Dictionary<string, IBaseModule>();
            }

            public IPortalResponse Invoke(IPortalRequest request)
            {
                var response = new PortalResponse(request);
                var extension = GetExtension(request);
                extension.WithPortalRequest(request);
                extension.WithPortalResponse(response);

                return extension.CallAction(request);
            }

            private IExtension GetExtension(IPortalRequest request)
            {
                var version = request.Version == Protocol.V6 || request.Version == Protocol.Latest ? "v6" : "v5";
                var fullpath = string.Format("/{0}/{1}/{2}", version, request.Extension, request.Action).ToLower();
                var path = string.Format("/{0}/{1}", version, request.Extension).ToLower();

                if (Endpoints.ContainsKey(fullpath))
                    return Endpoints[fullpath].Invoke();

                if (Endpoints.ContainsKey(path))
                    return Endpoints[path].Invoke();

                return GetExtension(request.Version, request.Extension);
            }

            private IExtension GetExtension(Protocol version, string extension)
            {
                if (extension == null || !LoadedModules.ContainsKey(extension))
                    throw new ExtensionMissingException(string.Format("Extension named '{0}' not found", extension));

                var module = LoadedModules[extension] as IModule;

                if(module == null) throw new ExtensionMissingException("Module is in an unknown format");

                return module.GetExtension(version, extension);
            }
        }
    }   
}