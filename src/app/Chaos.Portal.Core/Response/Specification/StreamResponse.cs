namespace Chaos.Portal.Core.Response.Specification
{
    using System.IO;

    public class StreamResponse : IResponseSpecification
    {
        #region Business Logic

        public Stream GetStream(IPortalResponse response)
        {
            var stream = (Stream)response.Output;

            if (stream.CanSeek) stream.Position = 0;

            return stream;
        }

        #endregion
    }
}