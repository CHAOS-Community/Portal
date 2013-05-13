namespace Chaos.Portal.Core
{
    using System;
    using System.Collections.Generic;

    using Chaos.Portal.Core.Bindings;
    using Chaos.Portal.Core.Cache;
    using Chaos.Portal.Core.Data;
    using Chaos.Portal.Core.Extension;
    using Chaos.Portal.Core.Indexing.View;
    using Chaos.Portal.Core.Logging;
    using Chaos.Portal.Core.Module;
    using Chaos.Portal.Core.Request;
    using Chaos.Portal.Core.Response;

    public interface IPortalApplication
    {
        ICache            Cache { get; }
        IPortalRepository PortalRepository { get; }
        ILog              Log { get; }

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

        TResult GetModule<TResult>(Protocol version) where TResult : IModule;

        void AddModule(IModule module);
    }
}