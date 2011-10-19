using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Geckon.Portal.Data.Result.Standard
{
    public static class ResultExtensionMethods
    {
        public static XDocument Serialize( this Result result )
        {
            return IResultExtensionMethods.Serialize( (IResult) result );
        }
    }
}
