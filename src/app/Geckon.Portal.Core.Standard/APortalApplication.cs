using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Geckon.Portal.Core.Extension;
using Geckon.Portal.Core.Standard.Extension;
using Geckon.Index;
using Geckon.Portal.Core.Cache;
using Geckon.Portal.Data;
using Geckon.Index.Solr;

namespace Geckon.Portal.Core.Standard
{
    public abstract class APortalApplication : System.Web.HttpApplication
    {
        #region Properties

        public IPortalContext PortalContext { get; protected set; }
        public static ICache Cache { get; protected set; }
        public static IIndexManager IndexManager { get; protected set; }

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

        protected void InitializePortalApplication()
        {
            if( !System.Web.Mvc.ModelBinders.Binders.ContainsKey( typeof(ICallContext) ) )
                System.Web.Mvc.ModelBinders.Binders.Add( typeof( ICallContext ), new ModelBinders.CallContextModelBinder() );

            if( !System.Web.Mvc.ModelBinders.Binders.ContainsKey( typeof( CallContext ) ) )
                System.Web.Mvc.ModelBinders.Binders.Add( typeof( CallContext ), new ModelBinders.CallContextModelBinder() );

            // Sets the modelbinding of index queries
            if( !System.Web.Mvc.ModelBinders.Binders.ContainsKey( typeof( IQuery ) ) )
                System.Web.Mvc.ModelBinders.Binders.Add( typeof( IQuery ), new ModelBinders.SolrQueryModelBinder() );

            if( !System.Web.Mvc.ModelBinders.Binders.ContainsKey( typeof( UUID ) ) )
                System.Web.Mvc.ModelBinders.Binders.Add( typeof( UUID ), new ModelBinders.UUIDModelBinder() );

            Cache        = new Membase();
            IndexManager = new SolrCoreManager<UUIDResult>();
        }

        #endregion
    }
}
