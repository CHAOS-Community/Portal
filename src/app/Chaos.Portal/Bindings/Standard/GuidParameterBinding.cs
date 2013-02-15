using System;
using System.Reflection;

namespace Chaos.Portal.Bindings.Standard
{
    using CHAOS.Extensions;

    using Chaos.Portal.Exceptions;

    public class GuidParameterBinding : IParameterBinding
    {
        public object Bind( ICallContext callContext, ParameterInfo parameterInfo )
        {
            if( callContext.Request.Parameters.ContainsKey( parameterInfo.Name ) && !string.IsNullOrEmpty( callContext.Request.Parameters[parameterInfo.Name] )&& callContext.Request.Parameters[parameterInfo.Name] != "00000000-0000-0000-0000-000000000000")
                return new Guid(callContext.Request.Parameters[parameterInfo.Name]);

            if(!parameterInfo.ParameterType.IsNullable()) throw new ParameterBindingMissingException( parameterInfo.Name );
                
            return null;
        }
    }
}
