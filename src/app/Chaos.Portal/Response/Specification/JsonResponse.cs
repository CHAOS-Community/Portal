using System.IO;
using System.Text;
using CHAOS.Serialization.JSON;
using CHAOS.Serialization.Standard;

namespace Chaos.Portal.Response.Specification
{
    public class JsonResponse : IResponseSpecification
    {
        #region Fields

        private string _callback;

        #endregion
        #region Initialization

        public JsonResponse WithCallback(string callback)
        {
            _callback = callback;

            return this;
        }

        #endregion
        #region Business Logic

        public Stream GetStream(IPortalResponse response)
        {
            if(_callback == null)
                return new MemoryStream(Encoding.UTF8.GetBytes(SerializerFactory.Get<JSON>().Serialize(response, false).Value));

            return new MemoryStream(Encoding.UTF8.GetBytes(SerializerFactory.Get<JSON>().Serialize(response, false).GetAsJSONP(_callback)));
        }

        #endregion
    }
}