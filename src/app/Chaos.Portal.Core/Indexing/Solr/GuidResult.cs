namespace Chaos.Portal.Core.Indexing.Solr
{
    using System;
    using System.Linq;
    using System.Xml.Linq;

    public class GuidResult : IIndexResult
    {
        #region Properties

        public Guid Guid { get; private set; }

        #endregion
        #region Business Logic

        public IIndexResult Init(XElement element)
        {
            this.Guid = new Guid(element.Elements("str").Where(node => node.Attribute("name").Value == "Guid").First().Value);

            return this;
        }

        #endregion
    }
}