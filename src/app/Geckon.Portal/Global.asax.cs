using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using Geckon.Portal.Core;
using Geckon.Portal.Core.Entrypoint;
using Geckon.Portal.Core.Module;
using Geckon.Portal.Core.Standard;
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

            PortalContext = new PortalContext();

            // TODO: Implement way of loading modules on the fly
            using( PortalDataContext db = new PortalDataContext( ConfigurationManager.ConnectionStrings["Portal"].ConnectionString ) )
            {
                foreach( Data.Module module in db.Module_Get( null, null ) )
                {
                    Assembly assembly = Assembly.LoadFile( Path.Combine( ServiceDirectoryPath, "Modules", module.Path ) );

                    foreach( Type classType in assembly.GetTypes() )
                    {
                        if( classType.GetInterface( typeof( IModule ).FullName) != null )
                        {
                            IModule portalModule = (IModule) assembly.CreateInstance( classType.FullName );

                            portalModule.Init( PortalContext, module.Configuration );

                            PortalContext.RegisterModule( portalModule );
                        }
                    }
                }
            }
        }

        public void RegisterGlobalFilters( GlobalFilterCollection filters )
        {
            filters.Add( new HandleErrorAttribute() );
        }

        public void RegisterRoutes( RouteCollection routes )
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default", // Route name
                             "{controller}/{action}/{id}", // URL with parameters
                             new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
                            );
        }

        protected void Application_Start()
        {
            // TODO: Assemblies should not just be loaded at application start
            using( PortalDataContext db = new PortalDataContext( ConfigurationManager.ConnectionStrings["Portal"].ConnectionString ) )
            {
                foreach( Entrypoint entrypoint in db.Entrypoint_Get( null, null ) )
                {
                    Assembly assembly = Assembly.LoadFile( Path.Combine( ServiceDirectoryPath, "Extensions", entrypoint.Path ) );

                    foreach( Type classType in assembly.GetTypes() )
                    {
                        if( classType.GetInterface( typeof( IEntrypoint ).FullName) != null )
                            LoadedEntrypoints.Add( classType.Name, new AssemblyTypeMap( assembly, classType ) );
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