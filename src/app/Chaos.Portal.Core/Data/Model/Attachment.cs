using System;
using System.IO;

namespace Chaos.Portal.Core.Data.Model
{
    public class Attachment
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public ulong ContentLength { get; set; }
        public IDisposable Disposable { get; set; }
        public Stream Stream { get; set; }
		public bool AsAttachment { get; set; }
    }
}
