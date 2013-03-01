namespace Chaos.Portal.Indexing.Solr
{
    using System.Linq;
    using System.Xml.Linq;

    public class IdResult : IIndexResult
    {
        #region Properties

        public string Id { get; private set; }

        #endregion
        #region Business Logic

        public IIndexResult Init(XElement element)
        {
            Id = element.Elements("str").Where(node => node.Attribute("name").Value == "Id").First().Value;

            return this;
        }

        #endregion
    }
}