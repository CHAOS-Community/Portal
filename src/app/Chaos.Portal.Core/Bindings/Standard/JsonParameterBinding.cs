using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;

namespace Chaos.Portal.Core.Bindings.Standard
{
  public class JsonParameterBinding<T> : IParameterBinding
  {
    public object Bind(IDictionary<string, string> parameters, ParameterInfo parameterInfo)
    {
      var name = parameterInfo.Name;

      if (!parameters.ContainsKey(name))
        throw new ArgumentException("Parameter not given", parameterInfo.Name);

      return JsonConvert.DeserializeObject<T>(parameters[name]);
    }
  }
}