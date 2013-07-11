namespace Chaos.Portal.IntegrationTest.Indexing.Solr
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chaos.Portal.Core.Indexing.Solr;
    using Chaos.Portal.Core.Indexing.Solr.Request;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class SolrCoreTest : TestBase
    {
        #region Make

        private SolrCore Make_SolrCore()
        {
            return new SolrCore(_httpConnection, "search");
        }

        #endregion
        #region Query

        [Test]
        public void Query_GetFirst_ReturnFirstIdFromSolr()
        {
            var solr = Make_SolrCore();

            var result = solr.Query(new SolrQuery().WithQuery("*:*").WithPageSize(1));

            var list = result.QueryResult.Results.ToList();
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(ResultOne, list[0].Id);
        }

        [Test]
        public void Query_GetPageIndexOne_ReturnIdAtPageIndexOne()
        {
            var solr = Make_SolrCore();

            var result = solr.Query(new SolrQuery().WithQuery("*:*").WithPageIndex(1).WithPageSize(1));

            var list = result.QueryResult.Results.ToList();
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(ResultTwo, list[0].Id);
        }

        [Test]
        public void Query_GetMultipleResults_ReturnPageSizeOfTwo()
        {
            var solr = Make_SolrCore();

            var result = solr.Query(new SolrQuery().WithQuery("*:*").WithPageIndex(0).WithPageSize(2));

            var list = result.QueryResult.Results.ToList();
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(ResultOne, list[0].Id);
            Assert.AreEqual(ResultTwo, list[1].Id);
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
            Assert.AreEqual(expected, list[0].Id);
        }

        #endregion
    }
}