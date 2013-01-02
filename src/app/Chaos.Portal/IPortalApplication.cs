using System;
using System.Collections.Generic;
using CHAOS.Index;
using Chaos.Portal.Bindings;
using Chaos.Portal.Cache;
using Chaos.Portal.Data;
using Chaos.Portal.Extension;
using Chaos.Portal.Logging;
using Chaos.Portal.Request;
using Chaos.Portal.Response;

namespace Chaos.Portal
{
    public interface IPortalApplication
    {
        IDictionary<string, IExtension>      LoadedExtensions { get; set; }
        IDictionary<Type, IParameterBinding> Bindings { get; }
        ICache                               Cache { get; }
        IIndexManager                        IndexManager { get; }
        IPortalRepository                    PortalRepository { get; }
        ILog                                 Log { get; }

        /// <summary>
        /// Process a request to portal. Any underlying extensions or modules will be called based on the callContext parameter
        /// </summary>
        /// <param name="request">contains information about what extension and action to call</param>
        /// <param name="response">the object that contains the response</param>
        /// <returns>The response object</returns>
        IPortalResponse ProcessRequest( IPortalRequest request, IPortalResponse response );

        /// <summary>
        /// Return the loaded instance of the requested extension
        /// </summary>
        /// <typeparam name="TExtension">The type of extension to get</typeparam>
        /// <returns>The loaded the instance of the extension</returns>
        TExtension GetExtension<TExtension>() where TExtension : IExtension;
    }
}