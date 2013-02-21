namespace Chaos.Portal.IntegrationTest.Indexing.Solr
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chaos.Portal.Indexing.Solr;

    using Moq;

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

            var result = solr.Query(new SolrQuery().WithQuery("*:*").WithPageSize(1));

            var list = result.QueryResult.Results.ToList();
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(new Guid("2e37473f-5f0e-43b7-aaad-4551004a735e"), list[0].Guid);
        }

        [Test]
        public void Query_GetPageIndexOne_ReturnGuidAtPageIndexOne()
        {
            var solr = Make_SolrCore();

            var result = solr.Query(new SolrQuery().WithQuery("*:*").WithPageIndex(1).WithPageSize(1));

            var list = result.QueryResult.Results.ToList();
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(new Guid("3e37473f-5f0e-43b7-aaad-4551004a735e"), list[0].Guid);
        }

        [Test]
        public void Query_GetMultipleResults_ReturnPageSizeOfTwo()
        {
            var solr = Make_SolrCore();

            var result = solr.Query(new SolrQuery().WithQuery("*:*").WithPageIndex(0).WithPageSize(2));

            var list = result.QueryResult.Results.ToList();
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(new Guid("2e37473f-5f0e-43b7-aaad-4551004a735e"), list[0].Guid);
            Assert.AreEqual(new Guid("3e37473f-5f0e-43b7-aaad-4551004a735e"), list[1].Guid);
        }

        #endregion
        #region Index

        [Test]
        public void Index_IndexSimpleObjectWithOneGuid_ShouldAddToSolrAndBeQueriableAfterwards()
        {
            var solr = Make_SolrCore();
            var indexable = new Mock<IIndexable>();
            var expected = new Guid("00102030-4050-6070-8090-a0b0c0d0e0f0");
            var field = new KeyValuePair<string, string>("Guid", expected.ToString());
            indexable.Setup(m => m.GetIndexableFields()).Returns(new[] { field });

            solr.Index(indexable.Object);

            var result = solr.Query(new SolrQuery().WithQuery(expected.ToString()).WithPageSize(1));
            var list = result.QueryResult.Results.ToList();
            Assert.AreEqual(expected, list[0].Guid);
        }

        #endregion
    }
}