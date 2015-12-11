namespace Chaos.Portal.Test.Indexing.Solr
{
    using CHAOS.Net;

    using Moq;

    using NUnit.Framework;

    public class TestBase
    {
        #region Properties

        protected string SolrResponseSample { get { return "<response><lst name=\"responseHeader\"><int name=\"status\">0</int><int name=\"QTime\">150</int></lst><result name=\"response\" numFound=\"537622\" start=\"0\"><doc><str name=\"Guid\">19e603f2-1263-904b-b6cc-5313739d3e30</str></doc><doc><str name=\"Guid\">17c559ea-cdde-fa40-a846-5fc33c8ba0af</str></doc><doc><str name=\"Guid\">1347dbed-2d04-4040-b91f-14d17610828c</str></doc><doc><str name=\"Guid\">140775be-3b83-214c-84d7-522e383d392a</str></doc><doc><str name=\"Guid\">15f2025a-c1ef-9d48-8d05-aca23f87aa87</str></doc></result><lst name=\"facet_counts\"><lst name=\"facet_queries\"><int name=\"DKA-Organization:DR\">6235</int></lst><lst name=\"facet_fields\"><lst name=\"DKA-Organization\"><int name=\"Det Kongelige Bibliotek\">38266</int><int name=\"DR\">6235</int><int name=\"Kulturarvsstyrelsen\">1014</int><int name=\"SMK\">336</int><int name=\"\">220</int><int name=\"DR br />br />\">8</int><int name=\"DRbr />br />\">3</int><int name=\"DR  \">1</int><int name=\"Danmarks Radio\">1</int><int name=\"Statens Museum for Kunst\">1</int></lst><lst name=\"ObjectTypeID\"><int name=\"24\">282551</int><int name=\"39\">198440</int><int name=\"36\">46254</int><int name=\"5\">3496</int><int name=\"23\">1851</int><int name=\"22\">1333</int><int name=\"13\">1128</int><int name=\"21\">586</int><int name=\"10\">522</int><int name=\"11\">319</int><int name=\"20\">303</int><int name=\"4\">198</int><int name=\"25\">122</int><int name=\"6\">117</int><int name=\"51\">102</int><int name=\"45\">62</int><int name=\"44\">51</int><int name=\"50\">45</int><int name=\"9\">21</int><int name=\"41\">18</int><int name=\"42\">14</int><int name=\"33\">10</int><int name=\"31\">9</int><int name=\"32\">9</int><int name=\"30\">8</int><int name=\"47\">8</int><int name=\"34\">7</int><int name=\"38\">7</int><int name=\"46\">7</int><int name=\"1\">3</int><int name=\"27\">3</int><int name=\"29\">3</int><int name=\"37\">3</int><int name=\"48\">2</int><int name=\"54\">2</int><int name=\"8\">2</int><int name=\"15\">1</int><int name=\"19\">1</int><int name=\"28\">1</int><int name=\"35\">1</int><int name=\"43\">1</int><int name=\"52\">1</int></lst></lst><lst name=\"facet_dates\"><lst name=\"DateCreated\"><int name=\"2012-06-14T00:00:00Z\">58</int><int name=\"2012-06-15T00:00:00Z\">194</int><int name=\"2012-06-16T00:00:00Z\">21</int><int name=\"2012-06-17T00:00:00Z\">147</int><int name=\"2012-06-18T00:00:00Z\">46</int><int name=\"2012-06-19T00:00:00Z\">5</int><str name=\"gap\">+1DAY</str><date name=\"start\">2012-06-14T00:00:00Z</date><date name=\"end\">2012-06-20T00:00:00Z</date></lst></lst><lst name=\"facet_ranges\"/></lst></response>"; } }
        protected string SolrResponseSampleWithoutFacet { get { return "<response><lst name=\"responseHeader\"><int name=\"status\">0</int><int name=\"QTime\">150</int></lst><result name=\"response\" numFound=\"537622\" start=\"0\"><doc><str name=\"Guid\">19e603f2-1263-904b-b6cc-5313739d3e30</str></doc><doc><str name=\"Guid\">17c559ea-cdde-fa40-a846-5fc33c8ba0af</str></doc><doc><str name=\"Guid\">1347dbed-2d04-4040-b91f-14d17610828c</str></doc><doc><str name=\"Guid\">140775be-3b83-214c-84d7-522e383d392a</str></doc><doc><str name=\"Guid\">15f2025a-c1ef-9d48-8d05-aca23f87aa87</str></doc></result></response>"; } }
        protected Mock<IHttpConnection> HttpConnection { get; set; }

        #endregion
        #region Set Up

        [SetUp]
        public void InitializeFakes()
        {
            this.HttpConnection = new Mock<IHttpConnection>();
        }

        #endregion
    }
}