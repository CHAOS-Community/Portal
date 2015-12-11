namespace Chaos.Portal.Core.Indexing.Solr.Response
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Xml.Linq;

	public class FacetResponse
	{
		#region Properties

		public IEnumerable<IFacetQueriesResult> FacetQueriesResult { get; set; }
		public IEnumerable<IFacetFieldsResult> FacetFieldsResult { get; set; }
		public IEnumerable<IFacetsResult> Ranges { get; set; }

		#endregion

		#region Constructors

		public FacetResponse()
		{
		}

		public FacetResponse(XContainer element)
		{
			if (element == null)
				return;

			FacetQueriesResult =
				element.Elements("lst")
				       .First(item => item.Attribute("name").Value == "facet_queries")
				       .Elements()
				       .Select(facetFields => new FacetQueriesResult(facetFields));
			FacetFieldsResult =
				element.Elements("lst")
				       .First(item => item.Attribute("name").Value == "facet_fields")
				       .Elements()
				       .Select(facetFields => new FacetFieldsResult(facetFields));
			Ranges = element.Elements("lst")
				       .First(item => item.Attribute("name").Value == "facet_ranges")
				       .Elements()
				       .Select(facetFields => new FacetResults(facetFields));
		}

		#endregion
	}
}