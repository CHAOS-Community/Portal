namespace Chaos.Portal.Core.Indexing.Solr
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class FacetResult
    {
        #region Properties

        public IEnumerable<IFacetQueriesResult> FacetQueriesResult { get; set; }
        public IEnumerable<IFacetFieldsResult> FacetFieldsResult { get; set; }
        //   public IEnumerable<IFacetDatesResult> FacetDatesResult { get; set; }
        //     public IEnumerable<IFacetRangesResult> FacetRangesResult { get; set; }

        #endregion
        #region Constructors

        public FacetResult()
        {

        }

        public FacetResult(XContainer element)
        {
            if (element == null)
                return;

            this.FacetQueriesResult = element.Elements("lst").First(item => item.Attribute("name").Value == "facet_queries").Elements().Select(facetFields => new FacetQueriesResult(facetFields));
            this.FacetFieldsResult = element.Elements("lst").First(item => item.Attribute("name").Value == "facet_fields").Elements().Select(facetFields => new FacetFieldsResult(facetFields));
            //        FacetDatesResult   = element.Elements( "lst" ).First( item => item.Attribute( "name" ).Value == "facet_dates" ).Elements().Select( facetFields => new FacetDatesResult( facetFields ) );
            //         FacetRangesResult  = element.Elements( "lst" ).First( item => item.Attribute( "name" ).Value == "facet_ranges" ).Elements().Select( facetFields => new FacetRangesResult( facetFields ) );
        }

        #endregion
    }
}
