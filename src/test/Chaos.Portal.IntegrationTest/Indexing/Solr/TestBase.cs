﻿namespace Chaos.Portal.IntegrationTest.Indexing.Solr
{
    using System.Xml.Linq;

    using CHAOS.Net;

    using NUnit.Framework;

    public class TestBase
    {
        #region Setup

        protected string _solrUrl;
        protected IHttpConnection _httpConnection;

        [SetUp]
        public void SetUp()
        {
            _solrUrl        = "http://solr.fyhr.dk:8080/solr/core0/";
            _httpConnection = new HttpConnection(_solrUrl);

            _httpConnection.Post("update", XElement.Parse("<delete><query>*:*</query></delete>")).Dispose();
            _httpConnection.Post("update", XElement.Parse("<commit/>")).Dispose();
            _httpConnection.Post("update", XElement.Parse("<add><doc><field name=\"Guid\">2e37473f-5f0e-43b7-aaad-4551004a735e</field></doc></add>")).Dispose();
            _httpConnection.Post("update", XElement.Parse("<commit softCommit=\"true\"/>")).Dispose();
        }

        #endregion
    }
}