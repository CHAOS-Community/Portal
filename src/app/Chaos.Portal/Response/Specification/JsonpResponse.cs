using System.IO;
using CHAOS.Serialization.JSON;
using CHAOS.Serialization.Standard;

namespace Chaos.Portal.Response.Specification
{
    public class JsonpResponse : IResponseSpecification
    {
        #region Business Logic

        public Stream GetStream(IPortalResponse response)
        {
            var callback = response.Callback ?? "portal_callback";

            return new MemoryStream(response.Encoding.GetBytes(SerializerFactory.Get<JSON>().Serialize(response, false).GetAsJSONP(callback)));
        }

        #endregion
    }
}