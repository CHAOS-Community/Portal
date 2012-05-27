using System.Reflection;
using CHAOS.Extensions;
using CHAOS.Portal.Exception;

namespace CHAOS.Portal.Core.Bindings.Standard
{
    public class DateTimeParameterBinding : IParameterBinding
    {
        public object Bind( ICallContext callContext, ParameterInfo parameterInfo )
        {
            if( callContext.PortalRequest.Parameters.ContainsKey( parameterInfo.Name ) && !string.IsNullOrEmpty( callContext.PortalRequest.Parameters[ parameterInfo.Name ] ) )
                return System.DateTime.Parse( callContext.PortalRequest.Parameters[ parameterInfo.Name ] );

            if( parameterInfo.ParameterType.IsNullable() )
                return null;

            throw new ParameterBindingMissingException("The parameter is missing, and the type isnt nullable");
        }
    }
}
