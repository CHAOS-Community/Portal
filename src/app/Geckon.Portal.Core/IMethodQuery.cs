using System.Collections.Generic;

namespace Geckon.Portal.Core
{
    public interface IMethodQuery
    {
        IEventType EventType { get; }
        IDictionary<string, IParameter> Parameters { get; }
    }
}
