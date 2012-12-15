using System;
using System.Reflection;
using CHAOS.Extensions;
using CHAOS.Portal.Exception;

namespace Chaos.Portal.Bindings.Standard
{
    public class ConvertableParameterBinding<T> : IParameterBinding where T : IConvertible
    {
        public object Bind( ICallContext callContext, ParameterInfo parameterInfo )
        {
            if( callContext.Request.Parameters.ContainsKey( parameterInfo.Name ) && !string.IsNullOrEmpty( callContext.Request.Parameters[ parameterInfo.Name ] ) )
                return Convert.ChangeType( callContext.Request.Parameters[ parameterInfo.Name ], typeof(T) );
            
            if( parameterInfo.ParameterType.IsNullable() )
                return null;

            throw new ParameterBindingMissingException(string.Format("The parameter ({0}) is missing, and the type isnt nullable",parameterInfo.Name));
        }
    }
}
