using System.Xml.Linq;
using Geckon.Serialization.Xml;

namespace Geckon.Portal.Core.Module
{
    public interface IModule
    {
        string Name { get; }
        void Init( IPortalContext portalContext, XDocument config );
        void Init( IPortalContext portalContext, XElement config );
        XmlSerialize InvokeMethod( IMethodQuery methodQuery );
        bool ContainsMethodSignature( IMethodQuery methodQuery );
        bool ContainsServiceHook(string extension, string action);
    }
}
