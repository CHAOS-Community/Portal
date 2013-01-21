namespace Chaos.Portal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chaos.Portal.Bindings;
    using Chaos.Portal.Bindings.Standard;
    using Chaos.Portal.Cache;
    using Chaos.Portal.Data;
    using Chaos.Portal.Exceptions;
    using Chaos.Portal.Extension;
    using Chaos.Portal.Index;
    using Chaos.Portal.Index.Standard;
    using Chaos.Portal.Logging;
    using Chaos.Portal.Request;
    using Chaos.Portal.Response;

    using CHAOS;
    using CHAOS.Index;

    /// <summary>
    /// The portal application.
    /// </summary>
    public class PortalApplication : IPortalApplication
    {
        #region Fields

        /// <summary>
        /// The _logging factory.
        /// </summary>
        private readonly ILogFactory _loggingFactory;

        #endregion
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PortalApplication"/> class.
        /// </summary>
        /// <param name="cache">
        /// The cache.
        /// </param>
        /// <param name="indexManager">
        /// The index manager.
        /// </param>
        /// <param name="viewManager">
        /// The view manager.
        /// </param>
        /// <param name="portalRepository">
        /// The portal repository.
        /// </param>
        /// <param name="loggingFactory">
        /// The logging factory.
        /// </param>
        public PortalApplication(ICache cache, IIndexManager indexManager, IViewManager viewManager, IPortalRepository portalRepository, ILogFactory loggingFactory)
        {
            Bindings         = new Dictionary<Type, IParameterBinding>();
            LoadedExtensions = new Dictionary<string, IExtension>();
            Log              = new DirectLogger(loggingFactory).WithName("Portal Application");
            Cache            = cache;
            IndexManager     = indexManager;
            ViewManager      = viewManager;
            PortalRepository = portalRepository;
            _loggingFactory  = loggingFactory;
            
            // Load bindings
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
            Bindings.Add( typeof(Guid), new UUIDParameterBinding() );
            Bindings.Add( typeof(Guid?), new UUIDParameterBinding() );
            Bindings.Add( typeof(IQuery), new QueryParameterBinding() );

            // load portal extensions
            LoadedExtensions.Add("ClientSettings", new Extension.Standard.ClientSettings().WithPortalApplication(this));
            LoadedExtensions.Add("Group",          new Extension.Standard.Group().WithPortalApplication(this));
            LoadedExtensions.Add("Session",        new Extension.Standard.Session().WithPortalApplication(this));
            LoadedExtensions.Add("Subscription",   new Extension.Standard.Subscription().WithPortalApplication(this));
            LoadedExtensions.Add("User",           new Extension.Standard.User().WithPortalApplication(this));
            LoadedExtensions.Add("UserSettings",   new Extension.Standard.UserSettings().WithPortalApplication(this));
            LoadedExtensions.Add("View",           new Extension.Standard.View().WithPortalApplication(this));

            // load portal views
            ViewManager.AddView("GroupView", new GroupView());
            ViewManager.AddView("SessionView", new SessionView());
        }

        #endregion
        #region Properties

        public IDictionary<Type, IParameterBinding> Bindings { get; set; }
        public IDictionary<string, IExtension> LoadedExtensions { get; set; }
        public ICache Cache { get; protected set; }
        public IIndexManager IndexManager { get; protected set; }
        public IViewManager ViewManager { get; protected set; }
        public ILog Log { get; protected set; }
        public IPortalRepository PortalRepository { get; set; }

        #endregion
        #region Business Logic

        /// <summary>
        /// Process a request to portal. Any underlying extensions or modules will be called based on the callContext parameter
        /// </summary>
        /// <param name="request">contains information about what extension and action to call</param>
        /// <param name="response">the object that contains the response</param>
        /// <returns>The response object</returns>
        public IPortalResponse ProcessRequest( IPortalRequest request, IPortalResponse response )
        {
            var callContext = new CallContext(this, request, response, _loggingFactory.Create());

            return GetExtension(request.Extension).CallAction(callContext);
        }

        private IExtension GetExtension(string extension)
        {
            if (!LoadedExtensions.ContainsKey(extension))
                throw new ExtensionMissingException(string.Format("Extension named '{0}' not found", extension));

            return LoadedExtensions[extension];
        }

        /// <summary>
        /// Return the loaded instance of the requested extension
        /// </summary>
        /// <typeparam name="TExtension">The type of extension to get</typeparam>
        /// <returns>The loaded the instance of the extension</returns>
        public TExtension GetExtension<TExtension>() where TExtension : IExtension
        {
            var extension = LoadedExtensions.FirstOrDefault(ext => ext.Value is TExtension).Value;

            if(extension == null)
                throw new ExtensionMissingException(string.Format("Extension of type '{0}' not found", typeof(TExtension).FullName));

            return (TExtension) extension;
        }

        #endregion
    }
}
