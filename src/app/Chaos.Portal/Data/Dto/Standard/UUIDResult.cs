using System.Linq;
using System.Xml.Linq;
using CHAOS;
using CHAOS.Serialization;

namespace Chaos.Portal.Data.Dto.Standard
{
    using Chaos.Portal.Indexing.Solr;

    public class UUIDResult : Result, IIndexResult
    {
        #region Properties

        [Serialize("Guid")]
        public UUID Guid { get; set; }

        #endregion
        #region Construction

        public UUIDResult()
        {

        }

		public UUIDResult(string guid)
        {
            Guid = new UUID( guid );
        }

        #endregion

        public IIndexResult Init( XElement element )
        {
            Guid = new UUID( element.Elements("str").Where( node => node.Attribute("name").Value == "Guid" ).First().Value );

            return this;
        }
    }
}
