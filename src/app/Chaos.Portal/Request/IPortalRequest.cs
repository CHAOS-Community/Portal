using System;
using System.Collections.Generic;
using Chaos.Portal.Standard;

namespace Chaos.Portal.Request
{
    public interface IPortalRequest
    {
        string                     Extension { get; }
        string                     Action { get; }
        IDictionary<string,string> Parameters { get; }
		IEnumerable<FileStream>    Files { get; }
        ReturnFormat               ReturnFormat { get; }
        DateTime                   Time { get; }
    }
}