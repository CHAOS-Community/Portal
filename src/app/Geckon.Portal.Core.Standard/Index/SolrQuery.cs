using System;
using System.Collections.Generic;
using System.Linq;
using Geckon.Portal.Core.Index;

namespace Geckon.Portal.Core.Standard.Index
{
    public class SolrQuery : IQuery
    {
        #region Properties

        public string Query { get; set; }
        public string Sort { get; set; }

        public string SolrQueryString
        {
            get
            {
                return string.Format( "q={0}&sort={1}", Query, Sort ?? "" );
            }
        }

        #endregion
        #region Construction

        public SolrQuery( string query, string sort )
        {
            Init( query, sort );
        }

        public SolrQuery( )
        {
        }

        #endregion
        #region Business Logic

        public void Init( string query, string sort )
        {
            Query = query;
            Sort  = sort;
        }

        #endregion
    }
}
