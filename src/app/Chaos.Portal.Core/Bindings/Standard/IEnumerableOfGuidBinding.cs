namespace Chaos.Portal.Core.Bindings.Standard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class EnumerableOfGuidParameterBinding : IParameterBinding
    {
        public object Bind(IDictionary<string, string> parameters, ParameterInfo parameterInfo)
        {
            if (!parameters.ContainsKey(parameterInfo.Name) || string.IsNullOrEmpty(parameters[parameterInfo.Name])) return new Guid[0];

            var sGuids = parameters[parameterInfo.Name].Split(',');

            return sGuids.Select(guid => new Guid(guid));
        }
    }
}