using System.Reflection;

namespace CHAOS.Portal.Core.Request.Bindings
{
    public class CallContextParameterBinding : IParameterBinding
    {
        public object Bind( ICallContext callContext, ParameterInfo parameterInfo )
        {
            return callContext;
        }
    }
}
