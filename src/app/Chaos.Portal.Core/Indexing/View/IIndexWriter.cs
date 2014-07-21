namespace Chaos.Portal.Core.Indexing.View
{
    using System.Collections.Generic;

    public interface IIndexWriter
    {
        void Write(IEnumerable<IndexDocument> document);
        void Write(IndexDocument document);
    }
}