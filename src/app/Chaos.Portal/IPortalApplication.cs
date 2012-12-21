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

        IPortalResponse ProcessRequest( IPortalRequest request, IPortalResponse response );
    }
}