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
                                            controllerContext.HttpContext.Request.QueryString["sessionGUID"] );
                case "POST":
                    return new CallContext( APortalApplication.Cache,
                                            APortalApplication.IndexManager,
                                            controllerContext.HttpContext.Request.Form["sessionGUID"] );
                case "PUT":
                    return new CallContext( APortalApplication.Cache,
                                            APortalApplication.IndexManager,
                                            controllerContext.HttpContext.Request.Form["sessionGUID"]);
                case "DELETE":
                    return new CallContext( APortalApplication.Cache,
                                            APortalApplication.IndexManager,
                                            controllerContext.HttpContext.Request.Form["sessionGUID"]);
            }

            throw new NotImplementedException( "Unknown HttpMethod" );
        }
    }
}
