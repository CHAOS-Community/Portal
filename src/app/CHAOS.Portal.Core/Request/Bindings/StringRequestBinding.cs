using System.Reflection;

namespace CHAOS.Portal.Core.Request.Bindings
{
    public class StringParameterBinding : IParameterBinding
    {
        public object Bind( ICallContext callContext, ParameterInfo parameterInfo )
        {
            if( callContext.PortalRequest.Parameters.ContainsKey( parameterInfo.Name ) )
                return callContext.PortalRequest.Parameters[ parameterInfo.Name ];

            return null;
        }
    }
}
