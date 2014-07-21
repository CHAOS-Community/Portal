namespace Chaos.Portal.Core.Indexing.View
{
    using System.Collections.Generic;

    public interface ICacheWriter
    {
        void Write(IEnumerable<CacheDocument> document);
        void Write(CacheDocument document);
    }
}