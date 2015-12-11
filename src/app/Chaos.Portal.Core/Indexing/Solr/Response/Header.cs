namespace Chaos.Portal.Core.Indexing.Solr.Response
{
    using System.Linq;
    using System.Xml.Linq;

    public class Header
    {
        #region Properties

        public uint Status { get; set; }
        public uint Duration { get; set; }

        #endregion
        #region Constructors

        public Header()
        {

        }

        public Header(XElement element)
        {
            this.Status = uint.Parse(element.Elements("int").First(item => item.Attribute("name").Value == "status").Value);
            this.Duration = uint.Parse(element.Elements("int").First(item => item.Attribute("name").Value == "QTime").Value);
        }

        #endregion
    }
}