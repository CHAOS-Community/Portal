using System.Web.Mvc;
using Geckon.Portal.Core.Exception;
using Geckon.Portal.Core.Standard.Extension;

namespace Geckon.Portal.Core.Standard
{
    public class ExtensionFactory : DefaultControllerFactory
    {
        #region Properties

        protected APortalApplication Application { get; set; }

        #endregion
        #region Constructors

        public ExtensionFactory( APortalApplication application )
        {
            Application = application;
        }

        #endregion
        public override IController CreateController( System.Web.Routing.RequestContext requestContext, string controllerName )
        {
            if( Application.PortalContext.LoadedExtensions.ContainsKey( controllerName ) )
            {
                IExtensionLoader loader = Application.PortalContext.LoadedExtensions[controllerName];
                
                AExtension extension = (AExtension) loader.Assembly.CreateInstance( loader.Extension.FullName );

				if( extension == null )
					throw new ExtensionMissingException( string.Format( "The extension ({0}) could not be loaded", loader.Extension.FullName == null ? "unknown" : loader.Extension.FullName ) );

                extension.Init( Application.PortalContext,
                                requestContext.HttpContext.Request.QueryString["format"] ?? "GXML",
                                requestContext.HttpContext.Request.QueryString["useHttpStatusCodes"] ?? "true" );
                
                return extension;
            }

            return base.CreateController( requestContext, controllerName );
        }
    }
}
