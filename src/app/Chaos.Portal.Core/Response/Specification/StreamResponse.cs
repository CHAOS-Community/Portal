using System;
using Chaos.Portal.Core.Data.Model;

namespace Chaos.Portal.Core.Response.Specification
{
    using System.IO;

    public class StreamResponse : IResponseSpecification
    {
        #region Business Logic

        public Stream GetStream(IPortalResponse response)
        {
	        var attachment = (Attachment) response.Output;

	        if (attachment == null)
	        {
				if(response.Output == null)
					throw new Exception("Response.Output must not be null");

				throw new Exception("Response.Output is not an Attachment but a " + response.Output.GetType().FullName);
	        }

			if (attachment.Stream.CanSeek)
				attachment.Stream.Position = 0;

			return attachment.Stream;
        }

        #endregion
    }
}