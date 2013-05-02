namespace Chaos.Portal.Core.Request
{
    using System.Collections.Generic;
    using System.Diagnostics;

    public interface IPortalRequest
    {
        string                     Extension { get; }
        string                     Action { get; }
        IDictionary<string,string> Parameters { get; }
		IEnumerable<FileStream>    Files { get; }
        ReturnFormat               ReturnFormat { get; }
        Stopwatch                  Stopwatch { get; }
    }
}