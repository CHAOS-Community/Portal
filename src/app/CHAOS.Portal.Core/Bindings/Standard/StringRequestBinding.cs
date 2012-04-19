using System.Reflection;

namespace CHAOS.Portal.Core.Bindings.Standard
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
