using System.Reflection;

namespace Chaos.Portal.Bindings.Standard
{
    public class CallContextParameterBinding : IParameterBinding
    {
        public object Bind( ICallContext callContext, ParameterInfo parameterInfo )
        {
            return callContext;
        }
    }
}
