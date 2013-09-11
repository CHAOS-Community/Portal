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

        /// <summary>
        /// Return the loaded instance of the requested extension
        /// </summary>
        /// <typeparam name="TExtension">The type of extension to get</typeparam>
        /// <returns>The loaded the instance of the extension</returns>
        TExtension GetExtension<TExtension>(Protocol version) where TExtension : IExtension;

        TResult GetModule<TResult>() where TResult : IModule;

        void AddModule(IModule module);
    }
}