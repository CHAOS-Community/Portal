﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Geckon.Portal.Core.Standard.Index;

namespace Geckon.Portal.Core.Standard.ModelBinders
{
    public class SolrQueryModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var qs = controllerContext.HttpContext.Request.QueryString;

            if( !string.IsNullOrEmpty( qs["query"] ) )
                return new SolrQuery( qs["query"], qs["sort"] );

            return null;
        }
    }
}
