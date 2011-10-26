using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Geckon.Portal.Core.Index;

namespace Geckon.Portal.Core.Standard.Test
{
    [TestFixture]
    public class SolrCoreManagerTest
    {
        [Test]
        public void Should_Get_Index_From_SolrManager()
        {
            IIndexManager manager = new SolrCoreManager();

            manager.AddIndex( "some.module.full.name", new SolrCoreConnection( "http://192.168.56.102:8080/solr/core0" ) );
            manager.AddIndex( "some.module.full.name", new SolrCoreConnection( "http://192.168.56.102:8080/solr/core1" ) );

            Solr solr = (Solr) manager.GetIndex( "some.module.full.name" );

            Assert.AreEqual( 2, solr.Cores.Count );
        }
    }
}
