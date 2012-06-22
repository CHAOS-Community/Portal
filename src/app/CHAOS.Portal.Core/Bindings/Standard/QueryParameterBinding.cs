using System.Reflection;
using CHAOS.Index.Solr;

namespace CHAOS.Portal.Core.Bindings.Standard
{
    public class QueryParameterBinding : IParameterBinding
    {
        public object Bind( ICallContext callContext, ParameterInfo parameterInfo )
        {
            var qs        = callContext.PortalRequest.Parameters;
            var query     = qs.ContainsKey("query") ? qs["query"] : null;
            var sort      = qs.ContainsKey("sort")  ? qs["sort"] : null;
            var pageIndex = qs.ContainsKey("pageIndex") ? qs["pageIndex"] : null;
            var pageSize  = qs.ContainsKey("pageSize") ? qs["pageSize"] : null;

            if( string.IsNullOrEmpty( query ) || string.IsNullOrEmpty( pageIndex ) || string.IsNullOrEmpty( pageSize ) )
                return null;

            return new SolrQuery( query, sort, int.Parse( pageIndex ), int.Parse( pageSize ) );
        }
    }
}
