namespace Chaos.Portal.Core.Response.Specification
{
    using System.IO;

    using CHAOS.Serialization.JSON;
    using CHAOS.Serialization.Standard;

    public class JsonResponse : IResponseSpecification
    {
        #region Business Logic

        public Stream GetStream(IPortalResponse response)
        { 
            return new MemoryStream(response.Encoding.GetBytes(SerializerFactory.Get<JSON>().Serialize(response.Output, false).Value));
        }

        #endregion
    }
}