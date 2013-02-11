namespace Chaos.Portal.Bindings.Standard
{
    using System.Reflection;
    using System.Xml.Linq;

    public class XDocumentBinding : IParameterBinding
    {
        public object Bind( ICallContext callContext, ParameterInfo parameterInfo )
        {
            if (callContext.Request.Parameters.ContainsKey(parameterInfo.Name) && !string.IsNullOrEmpty(callContext.Request.Parameters[parameterInfo.Name]))
                return XDocument.Parse(callContext.Request.Parameters[parameterInfo.Name]);

            return null;
        }

    }
}
