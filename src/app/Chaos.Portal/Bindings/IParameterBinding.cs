using System.Reflection;

namespace Chaos.Portal.Bindings
{
    public interface IParameterBinding
    {
        object Bind( ICallContext callContext, ParameterInfo parameterInfo );
    }
}
