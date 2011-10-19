using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Geckon.Portal.Core.Standard.Extension;
using Geckon.Portal.Core.Extension;

//namespace Geckon.Portal.Core.Standard.ModelBinders
//{
//    public class CallContextModelBinder : IModelBinder
//    {
//        public object BindModel( ControllerContext controllerContext, ModelBindingContext bindingContext )
//        {
//            var qs = controllerContext.HttpContext.Request.QueryString;

//            ICallContext callContext = new CallContext() { SessionID = qs["sessionID"] };

//            return callContext;
//        }
//    }
//}
