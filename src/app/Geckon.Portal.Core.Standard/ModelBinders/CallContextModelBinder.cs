using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Geckon.Portal.Core.Standard.Extension;

namespace Geckon.Portal.Core.Standard.ModelBinders
{
    public class CallContextModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            switch( controllerContext.HttpContext.Request.HttpMethod )
            {
                case "GET":
                    return new CallContext( APortalApplication.Cache,
                                            APortalApplication.IndexManager,
                                            controllerContext.HttpContext.Request.QueryString["sessionID"] );
                case "POST":
                    return new CallContext( APortalApplication.Cache,
                                            APortalApplication.IndexManager,
                                            controllerContext.HttpContext.Request.Form["sessionID"] );
                case "PUT":
                    return new CallContext( APortalApplication.Cache,
                                            APortalApplication.IndexManager,
                                            controllerContext.HttpContext.Request.Form["sessionID"]);
                case "DELETE":
                    return new CallContext( APortalApplication.Cache,
                                            APortalApplication.IndexManager,
                                            controllerContext.HttpContext.Request.Form["sessionID"]);
            }

            throw new NotImplementedException( "Unknown HttpMethod" );
        }
    }
}
