namespace Chaos.Portal.Test.Indexing.Solr
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml.Linq;

    using Chaos.Portal.Indexing.Solr;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class SolrCoreTest : TestBase
    {
        #region Make

        private SolrCore Make_SolrCore()
        {
            return new SolrCore(this.HttpConnection.Object);
        }

        #endregion
        #region Query

        [Test]
        public void Query_GivenValidQuery_ReturnASolrResponse()
        {
            var core = this.Make_SolrCore();
            var query = new SolrQuery();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(this.SolrResponseSampleWithoutFacet));
            this.HttpConnection.Setup(m => m.Get("select", It.IsAny<string>())).Returns(stream);

            var result = core.Query(query);

            Assert.IsNotNull(result);
            this.HttpConnection.Verify(m => m.Get("select", It.IsAny<string>()));
        }

        #endregion
        #region Index

        [Test]
        public void Index_GivenOneIIndexableObject_CallHttpConnectionWithAddXml()
        {
            var core = this.Make_SolrCore();
            var objectToIndex = new Mock<IIndexable>();
            objectToIndex.Setup(m => m.GetIndexableFields()).Returns(new[] { new KeyValuePair<string, string>("key", "value") });

            core.Index(objectToIndex.Object);

            this.HttpConnection.Verify(m => m.Post("update", It.Is<XElement>(b => b.ToString(SaveOptions.DisableFormatting) == "<add><doc><field name=\"key\">value</field></doc></add>")));
        }

        [Test]
        public void Index_GivenMultipleFields_CallHttpConnectionWithAddXml()
        {
            var core = this.Make_SolrCore();
            var objectToIndex = new Mock<IIndexable>();
            objectToIndex.Setup(m => m.GetIndexableFields()).Returns(new[] { new KeyValuePair<string, string>("key", "1"), new KeyValuePair<string, string>("key", "2") });

            core.Index(objectToIndex.Object);

            this.HttpConnection.Verify(m => m.Post("update", It.Is<XElement>(b => b.ToString(SaveOptions.DisableFormatting) == "<add><doc><field name=\"key\">1</field><field name=\"key\">2</field></doc></add>")));
        }

        [Test]
        public void Index_GivenMultipleObjects_CallHttpConnectionWithAddXml()
        {
            var core = this.Make_SolrCore();
            var objectToIndex1 = new Mock<IIndexable>();
            var objectToIndex2 = new Mock<IIndexable>();
            objectToIndex1.Setup(m => m.GetIndexableFields()).Returns(new[] { new KeyValuePair<string, string>("key", "1"), new KeyValuePair<string, string>("key", "2") });
            objectToIndex2.Setup(m => m.GetIndexableFields()).Returns(new[] { new KeyValuePair<string, string>("key", "3"), new KeyValuePair<string, string>("key", "4") });

            core.Index(new[] { objectToIndex1.Object, objectToIndex2.Object });

            this.HttpConnection.Verify(m => m.Post("update", It.Is<XElement>(b => b.ToString(SaveOptions.DisableFormatting) == "<add><doc><field name=\"key\">1</field><field name=\"key\">2</field></doc><doc><field name=\"key\">3</field><field name=\"key\">4</field></doc></add>")));
        }

        [Test]
        public void Index_WhenIndexing_CallHttpConnectionWithSoftCommitXml()
        {
            var core = this.Make_SolrCore();

            core.Index(new Mock<IIndexable>().Object);

            this.HttpConnection.Verify(m => m.Post("update", It.Is<XElement>(b => b.ToString(SaveOptions.DisableFormatting) == "<commit softCommit=\"true\" />")));
        }

        #endregion
        #region Commit

        [Test]
        public void Commit_ForceHardCommit_CallHttpConnectionWithHardCommitXml()
        {
            var core = this.Make_SolrCore();

            core.Commit();

            this.HttpConnection.Verify(m => m.Post("update", It.Is<XElement>(b => b.ToString(SaveOptions.DisableFormatting) == "<commit softCommit=\"false\" />")));
        }

        #endregion
        #region Optimize

        [Test]
        public void Optimize_SendOptimizeCommandToSolr_CallHttpConnectionWithOptimizeXml()
        {
            var core = this.Make_SolrCore();

            core.Optimize();

            this.HttpConnection.Verify(m => m.Post("update", It.Is<XElement>(b => b.ToString(SaveOptions.DisableFormatting) == "<optimize />")));
        }

        #endregion
        #region Delete

        [Test]
        public void Delete_GivenNoParameters_CallHttpConnectionWithDeleteAllXml()
        {
            var core = this.Make_SolrCore();

            core.Delete();

            this.HttpConnection.Verify(m => m.Post("update", It.Is<XElement>(b => b.ToString(SaveOptions.DisableFormatting) == "<delete><query>*:*</query></delete>")));
        }

        #endregion
    }
}