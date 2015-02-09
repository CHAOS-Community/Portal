using Newtonsoft.Json;

namespace Chaos.Portal.Core.Response.Specification
{
    using System.IO;

    public class Json3Response : IResponseSpecification
    {
        #region Business Logic

        public Stream GetStream(IPortalResponse response)
        {
          var portalResponse = ((Dto.v2.PortalResponse) response.Output);

          var json = JsonConvert.SerializeObject(portalResponse);

          return new MemoryStream(response.Encoding.GetBytes(json));
        }

      #endregion
    }
}