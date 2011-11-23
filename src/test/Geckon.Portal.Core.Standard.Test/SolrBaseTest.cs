using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Geckon.Portal.Data;
using Geckon.Index.Solr;

namespace Geckon.Portal.Core.Standard.Test
{
    public class SolrBaseTest
    {
        #region Properties

        protected Solr<GuidResult> Solr { get; set; }

        #endregion

        [SetUp]
        public void SetUp()
        {
            Solr = new Solr<GuidResult>();

            Solr.AddCore(new SolrCoreConnection("http://192.168.56.103:8080/solr/core0"));
            Solr.AddCore(new SolrCoreConnection("http://192.168.56.103:8080/solr/core1"));

            Solr.RemoveAll( true );
        }
    }
}
