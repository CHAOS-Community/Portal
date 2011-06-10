using System;
using System.Reflection;

namespace Geckon.Portal.Core
{
    public class AssemblyTypeMap
    {
        #region Properties

        public Assembly Assembly { get; private set; }
        public Type Type { get; private set; }

        #endregion
        #region Construction

        public AssemblyTypeMap(Assembly assembly, Type type)
        {
            Assembly = assembly;
            Type = type;
        }

        #endregion
    }
}