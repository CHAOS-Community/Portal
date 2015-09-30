namespace Chaos.Portal.Test.Indexing.Solr
{
	using Core.Indexing.Solr.Request;
	using NUnit.Framework;

	[TestFixture]
	public class SolrQueryTest
	{
		[Test]
		public void SolrQueryString_GroupFieldIsSet_ReturnQueryWithGroupingEnabled()
		{
			var query = new SolrQuery
				{
					Group = new SolrGroup {Field = "TypeId", Limit = 3}
				};

			var result = query.SolrQueryString;

			Assert.That(result,
			            Is.EqualTo(
				            "fl=Id,score&q=*:*&sort=&start=0&rows=0&fq=&facet=false&group=true&group.limit=3&group.field=TypeId&group.offset=0"));
		}

		[Test]
		public void SolrQueryString_WithFacet_ReturnQueryWithFacet()
		{
			var query = new SolrQuery
				{
					Facet = "field:something"
				};

			var result = query.SolrQueryString;

			Assert.That(result,
			            Is.EqualTo(
				            "fl=Id,score&q=*:*&sort=&start=0&rows=0&fq=&facet=true&facet.field=something&group=false&group.limit=0&group.field=&group.offset=0"));
		}

		[Test]
		public void SolrQueryString_WithRangeFacet_ReturnQuery()
		{
			var query = new SolrQuery
				{
					Facet = "(range date_val 1992-01-01T00:00:00Z 1993-01-01T00:00:00Z 1MONTH)"
				};

			var result = query.SolrQueryString;

			Assert.That(result, Is.EqualTo("fl=Id,score&q=*:*&sort=&start=0&rows=0&fq=&facet=true&facet.range=date_val&f.date_field.facet.range.start=1992-01-01T00:00:00Z&f.date_field.facet.range.end=1993-01-01T00:00:00Z&f.date_field.facet.range.gap=%2B1MONTH&group=false&group.limit=0&group.field=&group.offset=0"));
		}
	}
}