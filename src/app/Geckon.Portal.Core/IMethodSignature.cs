using System.Reflection;

namespace Geckon.Portal.Core
{
    public interface IMethodSignature
    {
        IDatatype Datatype { get; }
        MethodInfo Method { get; }
        IParameter[] Parameters { get; }
    }
}