namespace Chaos.Portal.Core.Request
{
    using System.IO;

    public class FileStream
	{
		#region Properties

		public Stream InputStream { get; protected set; }
		public string FileName { get; protected set; }
		public string ContentType { get; protected set; }
		public int ContentLength { get; protected set; }

		#endregion
		#region Construction

		public FileStream( Stream inputStream, string fileName, string contenType, int contentLength )
		{
			InputStream   = inputStream;
			FileName      = fileName;
			ContentType   = contenType;
			ContentLength = contentLength;
		}
		
		#endregion
	}
}
