using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Chaos.Portal.Core.Indexing.Solr.Response
{
	public class FacetResults : IFacetsResult
	{
		#region Properties

		public string Value { get; set; }
		public IList<IFacet> Facets { get; set; }

		#endregion
		#region Constructors

		public FacetResults(XElement element)
		{
			Value = element.Attribute("name").Value;
			Facets = element.Descendants("int").Select(item => (IFacet)new Facet("int", item.Attribute("name").Value, uint.Parse(item.Value))).ToList();
		}

		#endregion
	}
}