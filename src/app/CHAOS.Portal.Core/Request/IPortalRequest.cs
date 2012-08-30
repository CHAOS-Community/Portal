using System.Collections.Generic;
using CHAOS.Portal.Core.Standard;

namespace CHAOS.Portal.Core.Request
{
    public interface IPortalRequest
    {
        string                     Extension { get; }
        string                     Action { get; }
        IDictionary<string,string> Parameters { get; }
		IEnumerable<FileStream> Files { get; }
    }
}