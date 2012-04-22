using System.Collections.Generic;

namespace CHAOS.Portal.Core.Request
{
    public interface IPortalRequest
    {
        string                     Extension { get; }
        string                     Action { get; }
        IDictionary<string,string> Parameters { get; }
    }
}