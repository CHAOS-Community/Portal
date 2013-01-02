using System.IO;
using System.Text;
using CHAOS.Serialization.JSON;
using CHAOS.Serialization.Standard;

namespace Chaos.Portal.Response.Specification
{
    public class JsonResponse : IResponseSpecification
    {
        #region Business Logic

        public Stream GetStream(IPortalResponse response)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(SerializerFactory.Get<JSON>().Serialize(response, false).Value));
        }

        #endregion
    }
}