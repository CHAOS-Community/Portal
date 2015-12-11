namespace Chaos.Portal.Core.Indexing.Solr
{
    using System.Xml.Linq;

    public interface IIndexResult
    {
        IIndexResult Init(XElement element);
    }
}
