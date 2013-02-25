namespace Chaos.Portal.IntegrationTest.Indexing.Solr
{
    using System;
    using System.Configuration;
    using System.Xml.Linq;

    using CHAOS.Net;

    using NUnit.Framework;

    public class TestBase
    {
        #region Setup

        protected string _solrUrl;
        protected IHttpConnection _httpConnection;
        protected Guid ResultOne   = new Guid("2e37473f-5f0e-43b7-aaad-4551004a735e");
        protected Guid ResultTwo   = new Guid("3e37473f-5f0e-43b7-aaad-4551004a735e");
        protected Guid ResultThree = new Guid("4e37473f-5f0e-43b7-aaad-4551004a735e");

        [SetUp]
        public void SetUp()
        {
            _solrUrl        = ConfigurationManager.AppSettings["SOLR_URL"];
            _httpConnection = new HttpConnection(_solrUrl);

            _httpConnection.Post("search/update", XElement.Parse("<delete><query>*:*</query></delete>")).Dispose();
            _httpConnection.Post("search/update", XElement.Parse("<commit/>")).Dispose();
            _httpConnection.Post("search/update", XElement.Parse("<add><doc><field name=\"Guid\">" + ResultOne + "</field></doc><doc><field name=\"Guid\">" + ResultTwo + "</field></doc><doc><field name=\"Guid\">" + ResultThree + "</field></doc></add>")).Dispose();
            _httpConnection.Post("search/update", XElement.Parse("<commit softCommit=\"true\"/>")).Dispose();
        }

        #endregion
    }
}
