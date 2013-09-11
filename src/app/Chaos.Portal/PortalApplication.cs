using System.Configuration;
using Chaos.Portal.Core.EmailService;
using Chaos.Portal.EmailService;

namespace Chaos.Portal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    using Chaos.Portal.Core;
    using Chaos.Portal.Core.Bindings;
    using Chaos.Portal.Core.Bindings.Standard;
    using Chaos.Portal.Core.Cache;
    using Chaos.Portal.Core.Data;
    using Chaos.Portal.Core.Exceptions;
    using Chaos.Portal.Core.Extension;
    using Chaos.Portal.Core.Indexing;
    using Chaos.Portal.Core.Indexing.View;
    using Chaos.Portal.Core.Logging;
    using Chaos.Portal.Core.Module;
    using Chaos.Portal.Core.Request;
    using Chaos.Portal.Core.Response;

    /// <summary>
    /// The portal application.
    /// </summary>
    public class PortalApplication : IPortalApplication
    {
        #region Fields

        private readonly ILogFactory _loggingFactory;
	    private IEmailService _emailService;

        #endregion
        #region Properties

	    public IDictionary<Type, IParameterBinding> Bindings { get; set; }
        public IDictionary<string, IModule>         LoadedModules { get; set; }
        public ICache                               Cache { get; protected set; }
        public IViewManager                         ViewManager { get; protected set; }
        public ILog                                 Log { get; protected set; }
        public IPortalRepository                    PortalRepository { get; set; }

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
            LoadedModules    = new Dictionary<string, IModule>();
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
            var response  = new PortalResponse(request);
            var extension = GetExtension(request.Version, request.Extension);
            extension.WithPortalRequest(request);
            extension.WithPortalResponse(response);

            return extension.CallAction(request);
        }

//        private static IPortalResponse CreatePortalResponse(IPortalRequest request)
//        {
//            switch(request.Version)
//            {
//                case Protocol.V5:
//                    return new PortalResponse(request);
//                case Protocol.V6:
//                default:
//                    return new v6.Response.PortalResponse(request);
//            }
//        }

        /// <summary>
        /// Return the loaded instance of the requested extension
        /// </summary>
        /// <typeparam name="TExtension">The type of extension to get</typeparam>
        /// <returns>The loaded the instance of the extension</returns>
        public TExtension GetExtension<TExtension>(Protocol version) where TExtension : IExtension
        {
            // todo: optimize cross extension calls, this is relatively slow
            var modules = LoadedModules.Values.Distinct().FirstOrDefault(item => item.GetExtension<TExtension>(version) != null);

            if (modules == null) throw new ExtensionMissingException(string.Format("Extension not found"));

            return (TExtension)modules.GetExtension<TExtension>(version);
        }

        /// <summary>
        /// The get extension.
        /// </summary>
        /// <param name="version"> </param>
        /// <param name="extension">The key associated with the extension.</param>
        /// <returns>The instance of<see cref="IExtension"/>.</returns>
        /// <exception cref="ExtensionMissingException">Is thrown if the extension is not loaded</exception>
        public IExtension GetExtension(Protocol version, string extension)
        {
            if (extension == null || !LoadedModules.ContainsKey(extension))
                throw new ExtensionMissingException(string.Format("Extension named '{0}' not found", extension));

            var module = LoadedModules[extension];

            return module.GetExtension(version, extension);
        }

        public TModule GetModule<TModule>() where TModule : IModule
        {
            var moduleType = typeof(TModule);
            var moduleName = moduleType.FullName;

            foreach(var loadedModule in LoadedModules.Values)
            {
                if(loadedModule.GetType().FullName == moduleName)
                    return (TModule) loadedModule;
            }

            throw new ModuleNotLoadedException(string.Format("Module [{0}] is not loaded in Portal", moduleName));
        }

        // todo: rethink the module loading process, it's not logical
        public void AddModule(IModule module)
        {
            module.Load(this);

            foreach (var extensionName in module.GetExtensionNames(Protocol.V5))
            {
                if(!LoadedModules.ContainsKey(extensionName))
                    LoadedModules.Add(extensionName, module);
            }

            foreach (var extensionName in module.GetExtensionNames(Protocol.V6))
            {
                if (!LoadedModules.ContainsKey(extensionName))
                    LoadedModules.Add(extensionName, module);
            }
        }

        #endregion
    }
}