namespace Chaos.Portal.Bindings.Standard
{
    using System;
    using System.Linq;
    using System.Reflection;

    public class EnumerableOfGuidParameterBinding : IParameterBinding
    {
        public object Bind(ICallContext callContext, ParameterInfo parameterInfo)
        {
            if( !callContext.Request.Parameters.ContainsKey( parameterInfo.Name ) || string.IsNullOrEmpty( callContext.Request.Parameters[ parameterInfo.Name ] ) ) return new Guid[0];

            var sGuids = callContext.Request.Parameters[parameterInfo.Name].Split(',');

            return sGuids.Select(guid => new Guid(guid));
        }
    }
}