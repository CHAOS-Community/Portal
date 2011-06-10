using System;
using System.Web.Mvc;

namespace Geckon.Portal.Core
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
            if( Application.LoadedEntrypoints.ContainsKey( controllerName ) )
            {
                AssemblyTypeMap map  = Application.LoadedEntrypoints[ controllerName ];

                return (IController) map.Assembly.CreateInstance( map.Type.FullName );
            }

            return base.CreateController( requestContext, controllerName );
        }
    }
}
