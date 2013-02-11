using System;
using System.Reflection;

namespace Chaos.Portal.Bindings.Standard
{
    public class GuidParameterBinding : IParameterBinding
    {
        public object Bind( ICallContext callContext, ParameterInfo parameterInfo )
        {
            if( callContext.Request.Parameters.ContainsKey( parameterInfo.Name ) && !string.IsNullOrEmpty( callContext.Request.Parameters[ parameterInfo.Name ] ) )
                return new Guid( callContext.Request.Parameters[ parameterInfo.Name ] );

            return null;
        }
    }
}
