namespace Chaos.Portal.Test.Indexing.Solr
{
    using Chaos.Portal.Core.Indexing.Solr.Request;

    using NUnit.Framework;

    [TestFixture]
    public class SolrQueryTest
    {
        [Test]
        public void SolrQueryString_GroupFieldIsSet_ReturnQueryWithGroupingEnabled()
        {
            var query = new SolrQuery
                {
                    Group = new SolrGroup{ Field = "TypeId", Limit = 3 }
                };

            var result = query.SolrQueryString;

            Assert.That(result, Is.EqualTo("fl=Id,score&q=*:*&sort=&start=0&rows=0&fq=&facet=false&group=true&group.limit=3&group.field=TypeId"));
        } 
    }
}