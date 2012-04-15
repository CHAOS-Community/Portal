using System.Reflection;
using Geckon;

namespace CHAOS.Portal.Core.Request.Bindings
{
    public class UUIDParameterBinding : IParameterBinding
    {
        public object Bind( ICallContext callContext, ParameterInfo parameterInfo )
        {
            if( callContext.PortalRequest.Parameters.ContainsKey( parameterInfo.Name ) && !string.IsNullOrEmpty( callContext.PortalRequest.Parameters[ parameterInfo.Name ] ) )
                return new UUID( callContext.PortalRequest.Parameters[ parameterInfo.Name ] );

            return null;
        }
    }
}
