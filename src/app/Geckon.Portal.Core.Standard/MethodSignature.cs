using System.Reflection;

namespace Geckon.Portal.Core.Standard
{
    public class MethodSignature : IMethodSignature
    {
        #region Properties

        public IDatatype Datatype { get; protected set; }
        public MethodInfo Method { get; protected set; }
        public IParameter[] Parameters { get; protected set; }

        #endregion
        #region Construction

        public MethodSignature( IDatatype datatype, MethodInfo method, IParameter[] parameters )
        {
            Datatype   = datatype;
            Method     = method;
            Parameters = parameters;
        }

        #endregion
    }
}