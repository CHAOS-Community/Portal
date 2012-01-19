using System.Xml.Linq;
using Geckon.Serialization.JSON;

namespace Geckon.Portal.Data.Result.Standard
{
    public static class ResultExtensionMethods
    {
        public static XDocument ToXML( this Result result )
        {
            return IResultExtensionMethods.ToXML( (IResult) result );
        }

        public static JSON ToJSON(this Result result)
        {
            return IResultExtensionMethods.ToJSON((IResult)result);
        }
    }
}
