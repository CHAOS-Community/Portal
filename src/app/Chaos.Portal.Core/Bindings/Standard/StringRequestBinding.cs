namespace Chaos.Portal.Core.Bindings.Standard
{
    using System.Collections.Generic;
    using System.Reflection;

    public class StringParameterBinding : IParameterBinding
    {
        public object Bind(IDictionary<string, string> parameters, ParameterInfo parameterInfo)
        {
            return parameters.ContainsKey(parameterInfo.Name) ? parameters[parameterInfo.Name] : null;
        }
    }
}
