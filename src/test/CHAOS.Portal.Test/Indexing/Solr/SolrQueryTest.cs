namespace Chaos.Portal.Test.Indexing.Solr
{
	using Core.Indexing.Solr.Request;
	using NUnit.Framework;

	[TestFixture]
	public class SolrQueryTest
	{
		[Test]
		public void EDismax_NormalQuery()
		{
			var query = new EDismaxQuery
			{
				QueryFields = "somefield^0.2",
				Query = "search terms"
			};

			var result = query.SolrQueryString;

			Assert.That(result,
									Is.EqualTo("fl=Id,score&q=search terms&sort=&start=0&rows=0&fq=&qf=somefield^0.2&defType=edismax&facet=false"));
		}

		[Test]
		public void EDismax_WithQuotes()
		{
			var query = new EDismaxQuery
			{
				QueryFields = "somefield^0.2",
				Query = "\"some search\" terms"
			};

			var result = query.SolrQueryString;

			Assert.That(result,
									Is.EqualTo("fl=Id,score&q=\"some search\" terms&sort=&start=0&rows=0&fq=&qf=somefield^0.2&defType=edismax&facet=false"));
		}


		[Test]
		public void EDismax_EndingWithQuotes()
		{
			var query = new EDismaxQuery
			{
				QueryFields = "somefield^0.2",
				Query = "\"some search terms\""
			};

			var result = query.SolrQueryString;

			Assert.That(result,
									Is.EqualTo("fl=Id,score&q=\"some search terms\"&sort=&start=0&rows=0&fq=&qf=somefield^0.2&defType=edismax&facet=false"));
		}

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

			Assert.That(result, Is.EqualTo("fl=Id,score&q=*:*&sort=&start=0&rows=0&fq=&facet=true&facet.range=date_val&f.date_val.facet.range.start=1992-01-01T00:00:00Z&f.date_val.facet.range.end=1993-01-01T00:00:00Z&f.date_val.facet.range.gap=%2B1MONTH&group=false&group.limit=0&group.field=&group.offset=0"));
		}

		[Test]
		public void SolrQueryString_WithBothRangeAndFieldFacet_ReturnQuery()
		{
			var query = new SolrQuery
				{
					Facet = "field:something(range date_val 1992-01-01T00:00:00Z 1993-01-01T00:00:00Z 1MONTH)"
				};

			var result = query.SolrQueryString;

			Assert.That(result, Is.EqualTo("fl=Id,score&q=*:*&sort=&start=0&rows=0&fq=&facet=true&facet.range=date_val&f.date_val.facet.range.start=1992-01-01T00:00:00Z&f.date_val.facet.range.end=1993-01-01T00:00:00Z&f.date_val.facet.range.gap=%2B1MONTH&facet.field=something&group=false&group.limit=0&group.field=&group.offset=0"));
		}
	}
}