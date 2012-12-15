using System.Reflection;

namespace Chaos.Portal.Bindings.Standard
{
    public class StringParameterBinding : IParameterBinding
    {
        public object Bind( ICallContext callContext, ParameterInfo parameterInfo )
        {
            if( callContext.Request.Parameters.ContainsKey( parameterInfo.Name ) )
                return callContext.Request.Parameters[ parameterInfo.Name ];

            return null;
        }
    }
}
