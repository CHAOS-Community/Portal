using System;
using System.Collections.Generic;
using System.Linq;
using Geckon.Portal.Core.Index;

namespace Geckon.Portal.Core.Standard
{
    public class SolrCoreConnection : IIndexConnection
    {
        #region Properties

        public string URL { get; set; }

        #endregion
        #region Construction

        public SolrCoreConnection( string url )
        {
            URL = url;
        }

        #endregion
    }
}
