namespace Chaos.Portal.Core.Bindings.Standard
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using CHAOS.Extensions;

    using Chaos.Portal.Core.Exceptions;

    public class GuidParameterBinding : IParameterBinding
    {
        public object Bind(IDictionary<string, string> parameters, ParameterInfo parameterInfo)
        {
            if (parameters.ContainsKey(parameterInfo.Name) && !string.IsNullOrEmpty(parameters[parameterInfo.Name]) && parameters[parameterInfo.Name] != "00000000-0000-0000-0000-000000000000")
                return new Guid(parameters[parameterInfo.Name]);

            if(!parameterInfo.ParameterType.IsNullable()) throw new ParameterBindingMissingException( parameterInfo.Name );
                
            return null;
        }
    }
}
