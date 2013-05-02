namespace Chaos.Portal.Core.Bindings
{
    using System.Collections.Generic;
    using System.Reflection;

    public interface IParameterBinding
    {
        object Bind(IDictionary<string, string> parameters, ParameterInfo parameterInfo);
    }
}
