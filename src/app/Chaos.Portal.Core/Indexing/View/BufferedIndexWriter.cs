namespace Chaos.Portal.Core.Indexing.View
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Solr;

    public class BufferedIndexWriter : IIndexWriter, IDisposable
    {
        private IIndex Core { get; set; }
        private IList<IndexDocument> Buffer { get; set; }

        public BufferedIndexWriter(IIndex core)
        {
            Core = core;
            Buffer = new List<IndexDocument>();
        }

        public void Write(IEnumerable<IndexDocument> document)
        {
            foreach (var doc in document)
            {
                Write(doc);
            }
        }

        public void Write(IndexDocument document)
        {
            Buffer.Add(document);
        }

        public void Dispose()
        {
            Core.Index(Buffer.Select(i => i.Fields));
        }
    }
}