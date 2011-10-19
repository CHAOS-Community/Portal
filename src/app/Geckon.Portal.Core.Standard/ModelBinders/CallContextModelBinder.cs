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
            var qs = controllerContext.HttpContext.Request.QueryString;

            return new CallContext( new Membase(),
                                    new Solr(),
                                    qs["sessionID"] );
        }
    }
}
