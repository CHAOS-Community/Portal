using System.IO;

namespace Chaos.Portal.Response.Specification
{
    public class StreamResponse : IResponseSpecification
    {
        #region Business Logic

        public Stream GetStream(IPortalResponse response)
        {
            return response.Stream;
        }

        #endregion
    }
}