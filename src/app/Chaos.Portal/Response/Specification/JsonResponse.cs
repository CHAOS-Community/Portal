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
            return new MemoryStream(response.Header.Encoding.GetBytes(SerializerFactory.Get<JSON>().Serialize(response, false).Value));
         //   return new MemoryStream(response.Header.Encoding.GetBytes(JsonConvert.SerializeObject(response)));
        }

        #endregion
    }
}