namespace Chaos.Portal.Bindings.Standard
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Xml.Linq;

    public class XDocumentBinding : IParameterBinding
    {
        public object Bind(IDictionary<string, string> parameters, ParameterInfo parameterInfo)
        {
            if (parameters.ContainsKey(parameterInfo.Name) && !string.IsNullOrEmpty(parameters[parameterInfo.Name]))
                return XDocument.Parse(parameters[parameterInfo.Name]);

            return null;
        }

    }
}
