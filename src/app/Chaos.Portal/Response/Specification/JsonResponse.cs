using System.IO;
using CHAOS.Serialization.JSON;
using CHAOS.Serialization.Standard;

namespace Chaos.Portal.Response.Specification
{
    public class JsonResponse : IResponseSpecification
    {
        #region Business Logic

        public Stream GetStream(IPortalResponse response)
        { 
            return new MemoryStream(response.Encoding.GetBytes(SerializerFactory.Get<JSON>().Serialize(response.Output, false).Value));
         //   return new MemoryStream(response.Encoding.GetBytes(JsonConvert.SerializeObject(response)));
        }

        #endregion
    }
}