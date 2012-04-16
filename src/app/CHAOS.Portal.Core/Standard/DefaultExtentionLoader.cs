using System.Reflection;
using CHAOS.Portal.Core.Extension;

namespace CHAOS.Portal.Core.Standard
{
    public class DefaultExtentionLoader : IExtensionLoader
    {
        #region Properties

        protected Assembly ExtensionAssembly { get; set; }
        protected string   FullName { get; set; }

        #endregion
        #region Constructors

        public DefaultExtentionLoader( string assemblyPath, string fullName )
        {
            ExtensionAssembly = Assembly.LoadFrom( assemblyPath );
            FullName          = fullName;
        }

        #endregion
        #region Business Logic

        public IExtension CreateInstance()
        {
            return (IExtension) ExtensionAssembly.CreateInstance( FullName );
        }

        #endregion
    }
}
