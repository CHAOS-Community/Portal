using System.Collections.Generic;
using System.Xml.Linq;
using Geckon.Portal.Data.Result;

namespace Geckon.Portal.Core.Module
{
    public interface IModule
    {
        string Name { get; }
        void Init( IPortalContext portalContext, XDocument config );
        void Init( IPortalContext portalContext, XElement config );
        IEnumerable<IResult> InvokeMethod(IMethodQuery methodQuery);
        bool ContainsServiceHook(string extension, string action);
    }
}
