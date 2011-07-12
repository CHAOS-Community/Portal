using System.Web.Mvc;
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
            if( Application.LoadedExtensions.ContainsKey( controllerName ) )
            {
                AssemblyTypeMap map  = Application.LoadedExtensions[ controllerName ];

                AExtension extension = (AExtension) map.Assembly.CreateInstance( map.Type.FullName );

                extension.Init( new Result() );

                return (IController) extension;
            }

            return base.CreateController( requestContext, controllerName );
        }
    }
}
