namespace Chaos.Portal.Core.Indexing.View
{
    using Solr;

    public class CacheDocument 
    {
        public string Id { get; set; }

        public IIndexable Dto { get; set; }
    }
}