namespace Chaos.Portal.Core.Bindings.Standard
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using CHAOS.Extensions;

    using Chaos.Portal.Core.Exceptions;

    public class ConvertableParameterBinding<T> : IParameterBinding where T : IConvertible
    {
        public object Bind(IDictionary<string, string> parameters, ParameterInfo parameterInfo)
        {
            if (parameters.ContainsKey(parameterInfo.Name) && !string.IsNullOrEmpty(parameters[parameterInfo.Name]))
                return Convert.ChangeType(parameters[parameterInfo.Name], typeof(T));

            if (parameterInfo.ParameterType.IsNullable()) return null;
            if (parameterInfo.HasDefaultValue) return parameterInfo.DefaultValue;

            throw new ParameterBindingMissingException(string.Format("The parameter ({0}) is missing, and the type isnt nullable",parameterInfo.Name));
        }
    }
}
