using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace Geckon.Portal.Core
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
