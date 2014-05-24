namespace Chaos.Portal.Core.Bindings.Standard
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Reflection;

    using CHAOS.Extensions;

    using Exceptions;

    public class DateTimeParameterBinding : IParameterBinding
    {
        public object Bind(IDictionary<string, string> parameters, ParameterInfo parameterInfo)
        {
            if( parameters.ContainsKey( parameterInfo.Name ) && !string.IsNullOrEmpty( parameters[ parameterInfo.Name ] ) )
                return DateTime.ParseExact(parameters[parameterInfo.Name], "dd'-'MM'-'yyyy HH':'mm':'ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);

            if( parameterInfo.ParameterType.IsNullable() )
                return null;

            throw new ParameterBindingMissingException("The parameter is missing, and the type isnt nullable");
        }
    }
}
