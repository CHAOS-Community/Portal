using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Geckon.Serialization.Standard;

namespace Geckon.Portal.Data.Result
{
    public static class IResultExtensionMethods
    {
        public static XDocument Serialize(this IResult result)
        {
            return SerializerFactory.Get<XDocument>().Serialize(result, false);
        }
    }
}
