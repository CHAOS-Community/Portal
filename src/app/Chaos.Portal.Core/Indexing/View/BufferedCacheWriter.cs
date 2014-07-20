namespace Chaos.Portal.Core.Indexing.View
{
    using System;
    using System.Collections.Generic;
    using Cache;
    using Solr;

    public class BufferedCacheWriter : ICacheWriter, IDisposable
    {
        private ICache Cache { get; set; }
        private IList<CacheDocument> Buffer { get; set; }

        public BufferedCacheWriter(ICache cache)
        {
            Cache = cache;
            Buffer = new List<CacheDocument>();
        }

        public void Write(IEnumerable<CacheDocument> document)
        {
            foreach (var doc in document)
            {
                Write(doc);
            }
        }

        public void Write(CacheDocument document)
        {
            Buffer.Add(document);
        }

        public void Dispose()
        {
            foreach (var document in Buffer)
            {
                Cache.Store(document.Id, document);
            }
        }
    }

    public class CacheDocument 
    {
        public string Id { get; set; }

        public IIndexable Dto { get; set; }
    }

    public interface ICacheWriter
    {
        void Write(IEnumerable<CacheDocument> document);
        void Write(CacheDocument document);
    }
}