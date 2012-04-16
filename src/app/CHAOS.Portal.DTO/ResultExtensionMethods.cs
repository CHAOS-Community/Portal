using System.Xml.Linq;
using Geckon.Serialization.JSON;
using Geckon.Serialization.Standard;

namespace CHAOS.Portal.DTO
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
