using System;
using System.Reflection;
using CHAOS.Extensions;
using CHAOS.Portal.Exception;
using CHAOS.Serialization.Standard.String;

namespace Chaos.Portal.Bindings.Standard
{
    public class DateTimeParameterBinding : IParameterBinding
    {
        public object Bind( ICallContext callContext, ParameterInfo parameterInfo )
        {
            if( callContext.Request.Parameters.ContainsKey( parameterInfo.Name ) && !string.IsNullOrEmpty( callContext.Request.Parameters[ parameterInfo.Name ] ) )
                return new StringSerializer().Deserialize<DateTime>( callContext.Request.Parameters[ parameterInfo.Name ], true );

            if( parameterInfo.ParameterType.IsNullable() )
                return null;

            throw new ParameterBindingMissingException("The parameter is missing, and the type isnt nullable");
        }
    }
}
