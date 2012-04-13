using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Geckon.Portal.Core.Standard.ModelBinders
{
    public class UUIDModelBinder : IModelBinder
    {
        public object BindModel( ControllerContext controllerContext, ModelBindingContext bindingContext )
        {
            return new UUID( controllerContext.RequestContext.HttpContext.Request.QueryString[ bindingContext.ModelName ] );
        }
    }
}
