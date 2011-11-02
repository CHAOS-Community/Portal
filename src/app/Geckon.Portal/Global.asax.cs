using System;
using System.Linq;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using Geckon.Portal.Core.Module;
using Geckon.Portal.Core.Standard;
using Geckon.Portal.Core.Standard.Extension;
using Geckon.Portal.Data;

namespace Geckon.Portal
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : APortalApplication
    {
        public override void Init()
        {
            base.Init();
        }

        public void RegisterGlobalFilters( GlobalFilterCollection filters )
        {
            filters.Add( new HandleErrorAttribute() );
        }

        public void RegisterRoutes( RouteCollection routes )
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("Scripts/");
            
            routes.MapRoute( "Default", // Route name
                             "{controller}/{action}/{id}", // URL with parameters
                             new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
                           );
        }

        // REVIEW: Refactoring needed, portal initialization logic should recide in the base class
        protected void Application_Start()
        {
            InitializePortalApplication();

            PortalContext = new PortalContext();

            // TODO: Assemblies should not just be loaded at application start
            using( PortalDataContext db = new PortalDataContext( ConfigurationManager.ConnectionStrings["Portal"].ConnectionString ) )
            {
                foreach( Extension extension in db.Extension_Get( null, null ) )
                {
                    PortalContext.RegisterExtension( new ExtensionLoader( extension, this ) );
                }
            }

            // TODO: Implement way of loading modules on the fly
            using( PortalDataContext db = new PortalDataContext( ConfigurationManager.ConnectionStrings["Portal"].ConnectionString ) )
            {
                foreach( Data.Module module in db.Module_Get( null, null ) )
                {
                    Assembly assembly = Assembly.LoadFile( Path.Combine( ServiceDirectoryPath, "Modules", module.Path ) );

                    foreach( Type classType in assembly.GetTypes() )
                    {
                        if( classType.IsClass && classType.GetInterface( typeof( IModule ).FullName) != null )
                        {
                            IModule portalModule = (IModule) assembly.CreateInstance( classType.FullName );

                            portalModule.Init( PortalContext, module.Configuration );

                            PortalContext.RegisterModule( portalModule );

                            // Initializes index per module
                            IndexSetting indexSettings = db.IndexSettings_Get( module.ID ).FirstOrDefault();
                    
                            if( indexSettings != null )
                            {
                                foreach( string url in indexSettings.Settings.Elements("Core").Select( core => core.Attribute( "url" ).Value ) )
	                            {
                                    IndexManager.AddIndex( portalModule.Name, new SolrCoreConnection( url ) );
	                            }    
                            }
                        }
                    }
                }
            }

            AreaRegistration.RegisterAllAreas();
            ControllerBuilder.Current.SetControllerFactory( new ExtensionFactory( this ) );
            RegisterGlobalFilters( GlobalFilters.Filters );
            RegisterRoutes( RouteTable.Routes );
        }
    }
}