namespace Chaos.Portal.Core.Indexing.Solr.Response
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class ResponseBase
    {
        public Header Header { get; set; }
        public FacetResponse FacetResponse { get; set; }

        #region Initialization

        public ResponseBase(XContainer response)
        {
            Header      = new Header(response.Elements("lst").First(item => item.Attribute("name").Value == "responseHeader"));
            FacetResponse = new FacetResponse(response.Elements("lst").FirstOrDefault(item => item.Attribute("name").Value == "facet_counts"));
        }

        #endregion
    }

    public class Response<TReturnType> : ResponseBase, IIndexResponse<TReturnType> where TReturnType : IIndexResult, new()
    {
        #region Properties

        public IList<IQueryResultGroup<TReturnType>> QueryResultGroups { get; set; }
        public IQueryResult<TReturnType> QueryResult
        {
            get
            {
                return QueryResultGroups[0].Groups[0];
            }
        }

        #endregion
        public Response(XContainer response) : base(response)
        {
            QueryResultGroups = new List<IQueryResultGroup<TReturnType>>();

            // todo: refactoring needed
            var groups = response.Elements("lst").Where(item => item.Attributes("name").Any(atr => atr.Value == "grouped")).Elements();
            
            if (groups.Any())
            {
                foreach (var group in groups)
                {
                    var name       = group.Attribute("name").Value;
                    var foundCount = uint.Parse(group.Element("int").Value);
                    var results     = new List<IQueryResult<TReturnType>>();

                    foreach(var docs in group.Element("arr").Elements("lst"))
                    {
                        var value = docs.Element("str").Value;

                        results.Add(new QueryResult<TReturnType>(value, docs));
                    }

                    QueryResultGroups.Add(new QueryResultGroup<TReturnType>(name, foundCount, results));
                }
            }

            var result = response.Element("result");

            if (result != null)
            {
                var queryResult = new QueryResult<TReturnType>(null, (XElement)response);

                var name       = "Query";
                var foundCount = queryResult.FoundCount;
                var results     = new List<IQueryResult<TReturnType>> { queryResult };

                QueryResultGroups.Add(new QueryResultGroup<TReturnType>(name, foundCount, results));
            }
        }

        public Response(string xml) : this(XDocument.Parse(xml).Root)
        {

        }

        public Response(System.IO.Stream xml) : this(XDocument.Load(xml).Root)
        {

        }
    }
}
