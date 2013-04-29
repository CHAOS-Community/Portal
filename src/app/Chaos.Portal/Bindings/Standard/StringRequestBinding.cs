using System.Reflection;

namespace Chaos.Portal.Bindings.Standard
{
    using System.Collections.Generic;

    public class StringParameterBinding : IParameterBinding
    {
        public object Bind(IDictionary<string, string> parameters, ParameterInfo parameterInfo)
        {
            return parameters.ContainsKey(parameterInfo.Name) ? parameters[parameterInfo.Name] : null;
        }
    }
}
