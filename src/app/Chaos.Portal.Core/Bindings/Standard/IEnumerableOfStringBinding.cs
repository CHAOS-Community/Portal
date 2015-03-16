namespace Chaos.Portal.Core.Bindings.Standard
{
    using System.Collections.Generic;
    using System.Reflection;

    public class EnumerableOfStringParameterBinding : IParameterBinding
    {
        public object Bind(IDictionary<string, string> parameters, ParameterInfo parameterInfo)
        {
            if (!parameters.ContainsKey(parameterInfo.Name) || string.IsNullOrEmpty(parameters[parameterInfo.Name])) return new string[0];

          return parameters[parameterInfo.Name].Split(',');
        }
    }
}