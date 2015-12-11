namespace Chaos.Portal.Test.Indexing.Solr
{
	using System.IO;
	using System.Linq;
	using System.Text;
	using System.Xml.Linq;
	using Chaos.Portal.Core.Indexing.Solr;
	using Chaos.Portal.Core.Indexing.Solr.Response;
	using NUnit.Framework;

	[TestFixture]
	public class ResponseTest
	{
		[Test]
		public void Constructor_GivenStreamContainingAQuery_ReturnResponseWithSingleGroupContainingTheResults()
		{
			var xml = Make_SolrResponseXml();

			var result = new Response<DataStub>(xml);

			Assert.That(result.QueryResultGroups.Count, Is.EqualTo(1));
			Assert.That(result.QueryResultGroups[0].Groups.Count, Is.EqualTo(1));
			Assert.That(result.QueryResultGroups[0].Groups[0].Value, Is.Null);
			Assert.That(result.QueryResultGroups[0].Groups[0].FoundCount, Is.EqualTo(72));
			Assert.That(result.QueryResultGroups[0].Groups[0].Results.Count(), Is.EqualTo(3));
		}

		[Test]
		public void Constructor_GivenStreamContainingFacets_InstanciateResponseWithFieldFacets()
		{
			var xml = Make_SolrResponseXmlWithFacets();

			var result = new Response<DataStub>(xml);

			Assert.That(result.FacetResponse.FacetFieldsResult.First().Facets.Count(), Is.EqualTo(4));
			Assert.That(result.FacetResponse.FacetFieldsResult.First().Facets[0].Count, Is.EqualTo(71));
			Assert.That(result.FacetResponse.FacetFieldsResult.First().Facets[1].Count, Is.EqualTo(2));
			Assert.That(result.FacetResponse.FacetFieldsResult.First().Facets[2].Count, Is.EqualTo(1));
			Assert.That(result.FacetResponse.FacetFieldsResult.First().Facets[3].Count, Is.EqualTo(1));
		}

		[Test]
		public void Constructor_GivenStreamContainingGroups_ReturnResponseWithMultipleGroups()
		{
			var xml = Make_SolrResponseXmlWithGroup();

			var result = new Response<DataStub>(xml);

			Assert.That(result.QueryResultGroups.Count, Is.EqualTo(1));
			Assert.That(result.QueryResultGroups[0].Groups.Count, Is.EqualTo(3));
			Assert.That(result.QueryResultGroups[0].Groups[0].Value, Is.EqualTo("00000000-0000-0000-0000-000000000001"));
			Assert.That(result.QueryResultGroups[0].Groups[0].FoundCount, Is.EqualTo(69));
			Assert.That(result.QueryResultGroups[0].Groups[1].FoundCount, Is.EqualTo(1));
			Assert.That(result.QueryResultGroups[0].Groups[2].FoundCount, Is.EqualTo(2));
			Assert.That(result.QueryResultGroups[0].Groups[0].Results.Count(), Is.EqualTo(3));
			Assert.That(result.QueryResultGroups[0].Groups[1].Results.Count(), Is.EqualTo(1));
			Assert.That(result.QueryResultGroups[0].Groups[2].Results.Count(), Is.EqualTo(2));
		}

		[Test]
		public void Constructor_GivenStreamWithRangeFacets_ReturnResponseWithRangeFacets()
		{
			var xml = Make_SolrResponseXmlWithRangeFacets();

			var result = new Response<DataStub>(xml);

			Assert.That(result.FacetResponse.Ranges, Is.Not.Empty);
			Assert.That(result.FacetResponse.Ranges.First().Value, Is.EqualTo("field_name"));
			Assert.That(result.FacetResponse.Ranges.First().Facets.Count, Is.EqualTo(12));
			Assert.That(result.FacetResponse.Ranges.First().Facets.First().Count, Is.EqualTo(3));
		}

		[Test]
		public void Constructor_GivenStreamWithBothRangeAndFieldFacets_ReturnResponseWithBothFacets()
		{
			var xml = Make_SolrResponseXmlWithRangeFacets();

			var result = new Response<DataStub>(xml);

			Assert.That(result.FacetResponse.Ranges.First().Facets.Count, Is.EqualTo(12));
			Assert.That(result.FacetResponse.FacetFieldsResult.First().Facets.Count, Is.EqualTo(4));
		}

		protected Stream Make_SolrResponseXml()
		{
			var text = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>"
			           + "<response>"
			           + "<lst name=\"responseHeader\">"
			           + "<int name=\"status\">0</int>"
			           + "<int name=\"QTime\">1</int>"
			           + "</lst>"
			           + "<result name=\"doclist\" numFound=\"72\" start=\"0\">"
			           + "<doc>"
			           + "<str name=\"Id\">89</str>"
			           + "<str name=\"UserId\">1c7d983c-df67-47bb-9a76-75cbe1244c94</str>"
			           + "<str name=\"NetworkId\">00000000-0000-0000-0000-000000000001</str>"
			           + "<str name=\"QuestionText\">Jeg laver lige en test.. okay</str>"
			           + "<date name=\"DateCreated\">2013-04-24T01:32:02Z</date>"
			           + "<str name=\"_version_\">1439674271830900736</str>"
			           + "</doc>"
			           + "<doc>"
			           + "<str name=\"Id\">91</str>"
			           + "<str name=\"UserId\">2777d6b0-423f-4ce2-91a1-317930861214</str>"
			           + "<str name=\"NetworkId\">00000000-0000-0000-0000-000000000001</str>"
			           + "<str name=\"QuestionText\">Jeg laver lige en test :)</str>"
			           + "<date name=\"DateCreated\">2013-04-24T01:34:32Z</date>"
			           + "<str name=\"_version_\">1439674271838240768</str>"
			           + "</doc>"
			           + "<doc>"
			           + "<str name=\"Id\">99</str>"
			           + "<str name=\"UserId\">e1e8e986-6f1b-4ef2-9ebf-01baa78a795d</str>"
			           + "<str name=\"NetworkId\">00000000-0000-0000-0000-000000000001</str>"
			           + "<str name=\"QuestionText\">What is the source of human morality?</str>"
			           + "<date name=\"DateCreated\">2013-07-04T23:00:42Z</date>"
			           + "<str name=\"_version_\">1439674271769034752</str>"
			           + "</doc>"
			           + "</result>"
			           + "</response>";

			return new MemoryStream(Encoding.UTF8.GetBytes(text));
		}

		protected Stream Make_SolrResponseXmlWithFacets()
		{
			var text = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>"
			           + "<response>"
			           + "<lst name=\"responseHeader\">"
			           + "<int name=\"status\">0</int>"
			           + "<int name=\"QTime\">1</int>"
			           + "</lst>"
			           + "<result name=\"doclist\" numFound=\"72\" start=\"0\">"
			           + "<doc>"
			           + "<str name=\"Id\">89</str>"
			           + "<str name=\"UserId\">1c7d983c-df67-47bb-9a76-75cbe1244c94</str>"
			           + "<str name=\"NetworkId\">00000000-0000-0000-0000-000000000001</str>"
			           + "<str name=\"QuestionText\">Jeg laver lige en test.. okay</str>"
			           + "<date name=\"DateCreated\">2013-04-24T01:32:02Z</date>"
			           + "<str name=\"_version_\">1439674271830900736</str>"
			           + "</doc>"
			           + "<doc>"
			           + "<str name=\"Id\">91</str>"
			           + "<str name=\"UserId\">2777d6b0-423f-4ce2-91a1-317930861214</str>"
			           + "<str name=\"NetworkId\">00000000-0000-0000-0000-000000000001</str>"
			           + "<str name=\"QuestionText\">Jeg laver lige en test :)</str>"
			           + "<date name=\"DateCreated\">2013-04-24T01:34:32Z</date>"
			           + "<str name=\"_version_\">1439674271838240768</str>"
			           + "</doc>"
			           + "<doc>"
			           + "<str name=\"Id\">99</str>"
			           + "<str name=\"UserId\">e1e8e986-6f1b-4ef2-9ebf-01baa78a795d</str>"
			           + "<str name=\"NetworkId\">00000000-0000-0000-0000-000000000001</str>"
			           + "<str name=\"QuestionText\">What is the source of human morality?</str>"
			           + "<date name=\"DateCreated\">2013-07-04T23:00:42Z</date>"
			           + "<str name=\"_version_\">1439674271769034752</str>"
			           + "</doc>"
			           + "</result>"
			           + "<lst name=\"facet_counts\">"
			           + "<lst name=\"facet_queries\">"
			           + "<int name=\"*:*\">75</int>"
			           + "</lst>"
			           + "<lst name=\"facet_fields\">"
			           + "<lst name=\"NetworkId\">"
			           + "<int name=\"00000000-0000-0000-0000-000000000001\">71</int>"
			           + "<int name=\"fc2e10f7-3eee-49d8-b441-3235f96e5c0a\">2</int>"
			           + "<int name=\"3cbcdafc-af3b-4955-9d79-64d397b6393d\">1</int>"
			           + "<int name=\"3ec2f894-bdf2-4281-a91f-46a84b982039\">1</int>"
			           + "</lst>"
			           + "</lst>"
			           + "<lst name=\"facet_dates\"/>"
			           + "<lst name=\"facet_ranges\"/>"
			           + "</lst>"
			           + "</response>";

			return new MemoryStream(Encoding.UTF8.GetBytes(text));
		}

		protected Stream Make_SolrResponseXmlWithRangeFacets()
		{
			var text = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>"
			           + "<response>"
			           + "  <lst name=\"responseHeader\">"
			           + "    <int name=\"status\">0</int>"
			           + "    <int name=\"QTime\">3</int>"
			           + "  </lst>"
			           + "  <result name=\"response\" numFound=\"1004\" start=\"0\">"
			           + "  </result>"
			           + "  <lst name=\"facet_counts\">"
			           + "    <lst name=\"facet_queries\"/>"
								 + "    <lst name=\"facet_fields\">"
								 + "      <lst name=\"NetworkId\">"
								 + "        <int name=\"00000000-0000-0000-0000-000000000001\">71</int>"
								 + "        <int name=\"fc2e10f7-3eee-49d8-b441-3235f96e5c0a\">2</int>"
								 + "        <int name=\"3cbcdafc-af3b-4955-9d79-64d397b6393d\">1</int>"
								 + "        <int name=\"3ec2f894-bdf2-4281-a91f-46a84b982039\">1</int>"
								 + "      </lst>"
								 + "    </lst>"
			           + "    <lst name=\"facet_dates\"/>"
			           + "    <lst name=\"facet_ranges\">"
			           + "      <lst name=\"field_name\">"
			           + "        <lst name=\"counts\">"
			           + "          <int name=\"1900-01-01T00:00:00Z\">3</int>"
			           + "          <int name=\"1910-01-01T00:00:00Z\">0</int>"
			           + "          <int name=\"1920-01-01T00:00:00Z\">0</int>"
			           + "          <int name=\"1930-01-01T00:00:00Z\">0</int>"
			           + "          <int name=\"1940-01-01T00:00:00Z\">1</int>"
			           + "          <int name=\"1950-01-01T00:00:00Z\">2</int>"
			           + "          <int name=\"1960-01-01T00:00:00Z\">4</int>"
			           + "          <int name=\"1970-01-01T00:00:00Z\">6</int>"
			           + "          <int name=\"1980-01-01T00:00:00Z\">50</int>"
			           + "          <int name=\"1990-01-01T00:00:00Z\">366</int>"
			           + "          <int name=\"2000-01-01T00:00:00Z\">330</int>"
			           + "          <int name=\"2010-01-01T00:00:00Z\">240</int>"
			           + "        </lst>"
			           + "        <str name=\"gap\">+10YEAR</str>"
			           + "        <date name=\"start\">1900-01-01T00:00:00Z</date>"
			           + "        <date name=\"end\">2020-01-01T00:00:00Z</date>"
			           + "      </lst>"
			           + "    </lst>"
			           + "    <lst name=\"facet_intervals\"/>"
			           + "  </lst>"
			           + "</response>";

			return new MemoryStream(Encoding.UTF8.GetBytes(text));
		}

		protected Stream Make_SolrResponseXmlWithGroup()
		{
			var text = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>"
			           + "<response>"
			           + "<lst name=\"responseHeader\">"
			           + "<int name=\"status\">0</int>"
			           + "<int name=\"QTime\">1</int>"
			           + "</lst>"
			           + "<lst name=\"grouped\">"
			           + "<lst name=\"NetworkId\">"
			           + "<int name=\"matches\">72</int>"
			           + "<arr name=\"groups\">"
			           + "<lst>"
			           + "<str name=\"groupValue\">00000000-0000-0000-0000-000000000001</str>"
			           + "<result name=\"doclist\" numFound=\"69\" start=\"0\">"
			           + "<doc>"
			           + "<str name=\"Id\">89</str>"
			           + "<str name=\"UserId\">1c7d983c-df67-47bb-9a76-75cbe1244c94</str>"
			           + "<str name=\"NetworkId\">00000000-0000-0000-0000-000000000001</str>"
			           + "<str name=\"QuestionText\">Jeg laver lige en test.. okay</str>"
			           + "<date name=\"DateCreated\">2013-04-24T01:32:02Z</date>"
			           + "<str name=\"_version_\">1439674271830900736</str>"
			           + "</doc>"
			           + "<doc>"
			           + "<str name=\"Id\">91</str>"
			           + "<str name=\"UserId\">2777d6b0-423f-4ce2-91a1-317930861214</str>"
			           + "<str name=\"NetworkId\">00000000-0000-0000-0000-000000000001</str>"
			           + "<str name=\"QuestionText\">Jeg laver lige en test :)</str>"
			           + "<date name=\"DateCreated\">2013-04-24T01:34:32Z</date>"
			           + "<str name=\"_version_\">1439674271838240768</str>"
			           + "</doc>"
			           + "<doc>"
			           + "<str name=\"Id\">99</str>"
			           + "<str name=\"UserId\">e1e8e986-6f1b-4ef2-9ebf-01baa78a795d</str>"
			           + "<str name=\"NetworkId\">00000000-0000-0000-0000-000000000001</str>"
			           + "<str name=\"QuestionText\">What is the source of human morality?</str>"
			           + "<date name=\"DateCreated\">2013-07-04T23:00:42Z</date>"
			           + "<str name=\"_version_\">1439674271769034752</str>"
			           + "</doc>"
			           + "</result>"
			           + "</lst>"
			           + "<lst>"
			           + "<str name=\"groupValue\">3cbcdafc-af3b-4955-9d79-64d397b6393d</str>"
			           + "<result name=\"doclist\" numFound=\"1\" start=\"0\">"
			           + "<doc>"
			           + "<str name=\"Id\">103</str>"
			           + "<str name=\"UserId\">06dad248-bf4e-44c8-b30c-fe9c6a7119ce</str>"
			           + "<str name=\"NetworkId\">3cbcdafc-af3b-4955-9d79-64d397b6393d</str>"
			           + "<str name=\"QuestionText\">This is in SDF, right?</str>"
			           + "<date name=\"DateCreated\">2013-07-06T20:27:57Z</date>"
			           + "<str name=\"_version_\">1439927563448221696</str>"
			           + "</doc>"
			           + "</result>"
			           + "</lst>"
			           + "<lst>"
			           + "<str name=\"groupValue\">fc2e10f7-3eee-49d8-b441-3235f96e5c0a</str>"
			           + "<result name=\"doclist\" numFound=\"2\" start=\"0\">"
			           + "<doc>"
			           + "<str name=\"Id\">104</str>"
			           + "<str name=\"UserId\">5f9610c6-dfdb-432d-86bb-daf423ed1e51</str>"
			           + "<str name=\"NetworkId\">fc2e10f7-3eee-49d8-b441-3235f96e5c0a</str>"
			           + "<str name=\"QuestionText\">AndreasNetworkTest1</str>"
			           + "<date name=\"DateCreated\">2013-07-08T14:04:21.051Z</date>"
			           + "<str name=\"_version_\">1440001307691712512</str>"
			           + "</doc>"
			           + "<doc>"
			           + "<str name=\"Id\">105</str>"
			           + "<str name=\"UserId\">5f9610c6-dfdb-432d-86bb-daf423ed1e51</str>"
			           + "<str name=\"NetworkId\">fc2e10f7-3eee-49d8-b441-3235f96e5c0a</str>"
			           + "<str name=\"QuestionText\">AndreasNetworkTest2</str>"
			           + "<date name=\"DateCreated\">2013-07-08T14:04:44.473Z</date>"
			           + "<str name=\"_version_\">1440001331010994176</str>"
			           + "</doc>"
			           + "</result>"
			           + "</lst>"
			           + "</arr>"
			           + "</lst>"
			           + "</lst>"
			           + "</response>";

			return new MemoryStream(Encoding.UTF8.GetBytes(text));
		}
	}

	internal class DataStub : IIndexResult
	{
		#region Implementation of IIndexResult

		public IIndexResult Init(XElement element)
		{
			return this;
		}

		#endregion
	}
}