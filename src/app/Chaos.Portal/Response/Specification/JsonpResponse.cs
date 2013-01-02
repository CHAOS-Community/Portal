using System.IO;
using System.Text;
using CHAOS.Serialization.JSON;
using CHAOS.Serialization.Standard;

namespace Chaos.Portal.Response.Specification
{
    public class JsonpResponse : IResponseSpecification
    {
        #region MyRegion
        
        #endregion
        #region Business Logic

        public Stream GetStream(IPortalResponse response)
        {
            var callback = response.Header.Callback ?? "portal_callback";

            return new MemoryStream(Encoding.UTF8.GetBytes(SerializerFactory.Get<JSON>().Serialize(response, false).GetAsJSONP(callback)));
        }

        #endregion
    }
}