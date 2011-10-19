using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Geckon.Portal.Core.Extension;
using Geckon.Portal.Core.Standard.Extension;

namespace Geckon.Portal.Core.Standard
{
    public class APortalApplication : System.Web.HttpApplication
    {
        #region Properties

        public IPortalContext PortalContext { get; protected set; }

        public string ServiceDirectoryPath
        {
            get
            {
                return string.IsNullOrEmpty( ConfigurationManager.AppSettings["ServiceDirectory"]) ? Environment.CurrentDirectory : ConfigurationManager.AppSettings["ServiceDirectory"];
            }
        }

        #endregion
        #region Constructors

        public APortalApplication()
        {
            if( !System.Web.Mvc.ModelBinders.Binders.ContainsKey( typeof(ICallContext) ) )
                System.Web.Mvc.ModelBinders.Binders.Add( typeof( ICallContext ), new ModelBinders.CallContextModelBinder() );

            if( !System.Web.Mvc.ModelBinders.Binders.ContainsKey( typeof( CallContext ) ) )
                System.Web.Mvc.ModelBinders.Binders.Add( typeof( CallContext ), new ModelBinders.CallContextModelBinder() );
        }

        #endregion
        #region Business Logic

        protected IEnumerable<FileInfo> GetAllExtensionFiles()
        {
            return new DirectoryInfo( Path.Combine( ServiceDirectoryPath, "Extensions" ) ).GetFiles( "*.dll" );
        }

        #endregion
    }
}
