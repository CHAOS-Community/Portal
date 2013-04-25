using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CHAOS.Portal.DTO.Standard
{
    using System.IO;

    public class Attachment
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public IDisposable Disposable { get; set; }
        public Stream Stream { get; set; }
    }
}
