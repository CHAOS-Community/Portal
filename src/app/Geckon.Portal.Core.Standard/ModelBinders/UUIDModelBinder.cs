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
            switch( controllerContext.HttpContext.Request.HttpMethod )
            {
                case "GET":
                    if( string.IsNullOrEmpty( controllerContext.RequestContext.HttpContext.Request.QueryString[ bindingContext.ModelName ] ) )
                        return null;

                    return new UUID( controllerContext.RequestContext.HttpContext.Request.QueryString[ bindingContext.ModelName ] );
                default:
                    if( string.IsNullOrEmpty( controllerContext.RequestContext.HttpContext.Request.Form[ bindingContext.ModelName ] ) )
                        return null;

                    return new UUID( controllerContext.RequestContext.HttpContext.Request.Form[ bindingContext.ModelName ] );
            }
        }
    }
}
