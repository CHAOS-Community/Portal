using System.Reflection;

namespace CHAOS.Portal.Core.Bindings.Standard
{
    public class CallContextParameterBinding : IParameterBinding
    {
        public object Bind( ICallContext callContext, ParameterInfo parameterInfo )
        {
            return callContext;
        }
    }
}
