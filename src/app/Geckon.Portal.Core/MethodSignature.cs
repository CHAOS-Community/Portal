using System.Reflection;

namespace Geckon.Portal.Core
{
    public class MethodSignature : IMethodSignature
    {
        #region Properties

        public Datatype Datatype { get; protected set; }
        public MethodInfo Method { get; protected set; }
        public Parameter[] Parameters { get; protected set; }

        #endregion
        #region Construction

        public MethodSignature( Datatype datatype, MethodInfo method, Parameter[] parameters )
        {
            Datatype   = datatype;
            Method     = method;
            Parameters = parameters;
        }

        #endregion
    }
}