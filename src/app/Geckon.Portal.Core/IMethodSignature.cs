using System.Reflection;

namespace Geckon.Portal.Core
{
    public interface IMethodSignature
    {
        Datatype Datatype { get; }
        MethodInfo Method { get; }
        Parameter[] Parameters { get; }
    }
}