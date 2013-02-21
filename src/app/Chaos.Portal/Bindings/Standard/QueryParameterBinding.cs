using System.Reflection;

namespace Chaos.Portal.Bindings.Standard
{
    using Chaos.Portal.Indexing.Solr;

    public class QueryParameterBinding : IParameterBinding
    {
        public object Bind( ICallContext callContext, ParameterInfo parameterInfo )
        {
            var qs        = callContext.Request.Parameters;
            var query     = qs.ContainsKey("query") ? qs["query"] : null;
            var facet     = qs.ContainsKey("facet") ? qs["facet"] : null;
            var sort      = qs.ContainsKey("sort")  ? qs["sort"] : null;
            var pageIndex = qs.ContainsKey("pageIndex") && !string.IsNullOrEmpty( qs["pageIndex"] ) ? uint.Parse( qs["pageIndex"] ) : 0;
            var pageSize  = qs.ContainsKey("pageSize")  && !string.IsNullOrEmpty( qs["pageSize"]  ) ? uint.Parse( qs["pageSize"]  ) : 0;

            if( string.IsNullOrEmpty( query ) )
                query = "*:*";

            return new SolrQuery( query, facet, sort, pageIndex, pageSize );
        }
    }
}
