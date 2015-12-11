namespace Chaos.Portal.Core.Indexing.Solr.Response
{
    public interface IFacet
    {
        string DataType { get; set; }
        string Value { get; set; }
        uint Count { get; set; }
    }
}