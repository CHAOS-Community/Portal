namespace Chaos.Portal.Test.Response
{
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    using Chaos.Portal.Core;
    using Chaos.Portal.Core.Data;
    using Chaos.Portal.Core.Data.Model;
    using Chaos.Portal.Core.Exceptions;
    using Chaos.Portal.Core.Indexing.Solr.Response;
    using Chaos.Portal.Core.Request;

    using Moq;

    using NUnit.Framework;

    using PortalResponse = Chaos.Portal.Core.Response.PortalResponse;

    [TestFixture]
    public class PortalResponseTest
    {
        [Test, ExpectedException(typeof(UnhandledException))]
        public void PortalRepositoryGet_NotSet_ThrowUnhandledException()
        {
            var request = new PortalRequest();

            var result = request.PortalRepository;
        }
        
        [Test]
        public void Constructor_GivenPortalRepository_SetProperty()
        {
            var portalRepository = new Mock<IPortalRepository>();
            var request          = new PortalRequest(Protocol.Latest, null, null, null, portalRepository.Object);

            var result = request.PortalRepository;

            Assert.That(result, Is.EqualTo(portalRepository.Object));
        }

        [Test]
        public void WriteToOutput_GivenFacetResult_OutputStreamShouldIncludeTheFacetsInXML2()
        {
            var request      = new PortalRequest();
            var response     = new PortalResponse(request) { ReturnFormat = ReturnFormat.XML2 };
            var solrResponse = Make_SolrResponseWithFacets();
            var faceted      = new QueryResult
                {
                    FieldFacets = solrResponse.FacetResult.FacetFieldsResult.Select(item => new FieldFacet(item.Value, item.Facets.Select(facet => new Core.Data.Model.Facet(facet.Value, facet.Count)).ToList())).ToList()
                };
            response.ReturnFormat = ReturnFormat.XML2;
            request.Stopwatch.Reset();

            response.WriteToOutput(faceted);

            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                Assert.That(stream.ReadToEnd(), Is.EqualTo("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?>"
                                                           + "<PortalResponse>"
                                                             + "<Header>"
                                                               + "<Duration>0</Duration>"
                                                             + "</Header>"
                                                             + "<Body>"
                                                               + "<FieldFacets>"
                                                                 + "<FieldFacet>"
                                                                   + "<Value>NetworkId</Value>"
                                                                   + "<Facets>"
                                                                     + "<Facet>"
                                                                       + "<Key>00000000-0000-0000-0000-000000000001</Key>"
                                                                       + "<Count>71</Count>"
                                                                     + "</Facet>"
                                                                     + "<Facet>"
                                                                       + "<Key>fc2e10f7-3eee-49d8-b441-3235f96e5c0a</Key>"
                                                                       + "<Count>2</Count>"
                                                                     + "</Facet>"
                                                                     + "<Facet>"
                                                                       + "<Key>3cbcdafc-af3b-4955-9d79-64d397b6393d</Key>"
                                                                       + "<Count>1</Count>"
                                                                     + "</Facet>"
                                                                     + "<Facet>"
                                                                       + "<Key>3ec2f894-bdf2-4281-a91f-46a84b982039</Key>"
                                                                       + "<Count>1</Count>"
                                                                     + "</Facet>"
                                                                   + "</Facets>"
                                                                 + "</FieldFacet>"
                                                               + "</FieldFacets>"
                                                             + "</Body>"
                                                             + "<Error />"
                                                           + "</PortalResponse>"));
            }
        }

        private ResponseBase Make_SolrResponseWithFacets()
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
            var xml = XDocument.Parse(text);
            return new ResponseBase(xml.Root);
        }
    }

    class IndexSampleData : AResult
    {
        #region Properties

        public string Id { get; set; }

        #endregion
    }
}