using System.Reflection;

namespace Chaos.Portal.Bindings
{
    using System.Collections.Generic;

    public interface IParameterBinding
    {
        object Bind(IDictionary<string, string> parameters, ParameterInfo parameterInfo);
    }
}
