namespace Chaos.Portal.Indexing.Solr
{
    using System.Collections.Generic;

    public interface IIndexable
    {
        KeyValuePair<string, string> UniqueIdentifier { get; }
        IEnumerable<KeyValuePair<string, string>> GetIndexableFields();
    }
}
