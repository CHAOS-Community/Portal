namespace Chaos.Portal.Core.Indexing.View
{
    using Solr;

    public class IndexDocument
    {
        public string Id { get; set; }
        public IIndexable Fields { get; set; } 
    }
}