using System.Xml.Linq;
using CHAOS.Serialization.JSON;
using CHAOS.Serialization.Standard;

namespace Chaos.Portal.Data.Dto
{
    public static class ResultExtensionMethods
    {
        public static XDocument ToXML(this IResult result)
        {
            return SerializerFactory.Get<XDocument>().Serialize(result, false);
        }

        public static JSON ToJSON(this IResult result)
        {
            return SerializerFactory.Get<JSON>().Serialize(result, false);
        }
    }
}
