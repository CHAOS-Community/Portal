namespace Chaos.Portal.IntegrationTest.Indexing.Solr
{
    using System;
    using System.Linq;

    using Chaos.Portal.Indexing.Solr;

    using NUnit.Framework;

    [TestFixture]
    public class SolrCoreTest : TestBase
    {
        #region Make

        private SolrCore Make_SolrCore()
        {
            return new SolrCore(_httpConnection);
        }

        #endregion
        #region Query

        [Test]
        public void Query_GetFirst_ReturnFirstGuidFromSolr()
        {
            var solr = Make_SolrCore();

            var result = solr.Query(new SolrQuery().WithQuery("*:*").WithPageIndex(0).WithPageSize(1));

            var list = result.QueryResult.Results.ToList();
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(new Guid("2e37473f-5f0e-43b7-aaad-4551004a735e"), list[0].Guid);
        }

        #endregion
    }
}