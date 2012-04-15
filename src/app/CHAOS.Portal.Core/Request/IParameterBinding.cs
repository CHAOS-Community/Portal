using System.Reflection;

namespace CHAOS.Portal.Core.Request
{
    public interface IParameterBinding
    {
        object Bind( ICallContext callContext, ParameterInfo parameterInfo );
    }
}
