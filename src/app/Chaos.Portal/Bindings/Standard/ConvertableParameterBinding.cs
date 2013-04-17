using System;
using System.Reflection;
using CHAOS.Extensions;

namespace Chaos.Portal.Bindings.Standard
{
    using Chaos.Portal.Core.Exceptions;

    public class ConvertableParameterBinding<T> : IParameterBinding where T : IConvertible
    {
        public object Bind( ICallContext callContext, ParameterInfo parameterInfo )
        {
            if( callContext.Request.Parameters.ContainsKey( parameterInfo.Name ) && !string.IsNullOrEmpty( callContext.Request.Parameters[ parameterInfo.Name ] ) )
                return Convert.ChangeType( callContext.Request.Parameters[ parameterInfo.Name ], typeof(T) );

            if (parameterInfo.ParameterType.IsNullable()) return null;
            if (parameterInfo.HasDefaultValue) return parameterInfo.DefaultValue;

            throw new ParameterBindingMissingException(string.Format("The parameter ({0}) is missing, and the type isnt nullable",parameterInfo.Name));
        }
    }
}
