using System.Reflection;

namespace CHAOS.Portal.Core.Bindings
{
    public interface IParameterBinding
    {
        object Bind( ICallContext callContext, ParameterInfo parameterInfo );
    }
}
