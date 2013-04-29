using System.Reflection;

namespace Chaos.Portal.Bindings.Standard
{
    using System.Collections.Generic;

    using Chaos.Portal.Indexing.Solr;

    public class QueryParameterBinding : IParameterBinding
    {
        public object Bind(IDictionary<string, string> parameters, ParameterInfo parameterInfo)
        {
            var query     = parameters.ContainsKey("query") ? parameters["query"] : null;
            var facet     = parameters.ContainsKey("facet") ? parameters["facet"] : null;
            var sort      = parameters.ContainsKey("sort")  ? parameters["sort"] : null;
            var pageIndex = parameters.ContainsKey("pageIndex") && !string.IsNullOrEmpty( parameters["pageIndex"] ) ? uint.Parse( parameters["pageIndex"] ) : 0;
            var pageSize  = parameters.ContainsKey("pageSize")  && !string.IsNullOrEmpty( parameters["pageSize"]  ) ? uint.Parse( parameters["pageSize"]  ) : 0;

            if( string.IsNullOrEmpty( query ) )
                query = "*:*";

            return new SolrQuery( query, facet, sort, pageIndex, pageSize );
        }
    }
}
