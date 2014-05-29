namespace Chaos.Portal.Core
{
    using System;
    using System.Collections.Generic;

    using Bindings;
    using Cache;
    using Data;
    using Extension;
    using Indexing.View;
    using Logging;
    using Module;
    using Request;
    using Response;
	using EmailService;

    public interface IPortalApplication
    {
        ICache				Cache { get; }
        IPortalRepository	PortalRepository { get; }
        ILog				Log { get; }
		IEmailService		EmailService { get; }

        IDictionary<Type, IParameterBinding> Bindings { get; set; }

        IViewManager ViewManager { get; }
        /// <summary>
        /// Process a request to portal. Any underlying extensions or modules will be called based on the callContext parameter
        /// </summary>
        /// <param name="request">contains information about what extension and action to call</param>
        /// <returns>The response object</returns>
        IPortalResponse ProcessRequest( IPortalRequest request );

        //TResult GetModule<TResult>() where TResult : IModule;

        void AddModule(IModule module);
        void AddModule(IModuleConfig module);
        event ApplicationDelegates.ModuleHandler OnModuleLoaded;
        void MapRoute(string path, Func<IExtension> func);
        void AddView(IView view, string coreName = null);
        void AddBinding(Type type, IParameterBinding binding);
    }
}