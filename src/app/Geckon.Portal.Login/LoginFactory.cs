using System.Collections.Generic;
using System.Reflection;
using log4net;

namespace Geckon.Portal.Login
{
    public static class LoginFactory
    {
        #region Fields

        //public static IDictionary<string,Assembly> _LoadedAssemblies = new Dictionary<string, Assembly>();
        private static readonly ILog log = LogManager.GetLogger(typeof(LoginFactory));

        #endregion
        #region Properties

        //public static IDictionary<string, Assembly> LoadedAssemblies
        //{
        //    get { return _LoadedAssemblies; }
        //    set { _LoadedAssemblies = value; }
        //}

        #endregion

        public static ILogin Create(string fullname, string assemblyPath)
        {
            //if( !LoadedAssemblies.ContainsKey( fullname ) )
            //    LoadedAssemblies.Add( fullname, Assembly.LoadFrom( assemblyPath ) );

            log.Debug("Loading assembly \"" + fullname + "\" from " + assemblyPath);
            Assembly assembly = Assembly.LoadFrom(assemblyPath);// LoadedAssemblies[fullname];

            return (ILogin)assembly.CreateInstance(fullname);
        }
    }
}
