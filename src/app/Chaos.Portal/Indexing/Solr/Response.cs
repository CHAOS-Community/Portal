namespace Chaos.Portal.Indexing.Solr
{
    using System.Linq;
    using System.Xml.Linq;

    public class Response<TReturnType> : IIndexResponse<TReturnType> where TReturnType : IIndexResult, new()
    {
        #region Properties

        public Header Header { get; set; }
        public QueryResult<TReturnType> QueryResult { get; set; }
        public FacetResult FacetResult { get; set; }

        #endregion
        public Response(XContainer element)
        {
            Header = new Header(element.Elements("lst").First(item => item.Attribute("name").Value == "responseHeader"));
            QueryResult = new QueryResult<TReturnType>(element.Elements("result").First(item => item.Attribute("name").Value == "response"));
            FacetResult = new FacetResult(element.Elements("lst").FirstOrDefault(item => item.Attribute("name").Value == "facet_counts"));
        }

        public Response(string xml)
            : this(XDocument.Parse(xml).Root)
        {

        }

        public Response(System.IO.Stream xml)
            : this(XDocument.Load(xml).Root)
        {

        }
    }
}
