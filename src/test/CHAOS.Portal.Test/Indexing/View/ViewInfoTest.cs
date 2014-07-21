namespace Chaos.Portal.Test.Indexing.View
{
    using System;
    using System.Collections.Generic;
    using Core.Exceptions;
    using Core.Indexing.Solr;
    using Core.Indexing.Solr.Request;
    using Core.Indexing.Solr.Response;
    using Moq;
    using NUnit.Framework;

    public class ViewInfoTest : TestBase
    {
        [Test]
        public void Query_GivenValidQuery_ReturnPagedResult()
        {
            var stubResponse = new Mock<IIndexResponse<IdResult>>();
            var view = Make_MockView();
            var query = new SolrQuery();
            var queryResultStub = new QueryResultStub<IdResult> {Results = new[] {new IdResult {Id = "id", Score = 1}}};
            var expected = new[] {new ViewData()};
            stubResponse.Setup(p => p.QueryResult).Returns(queryResultStub);
            CoreMock.Setup(m => m.Query(query)).Returns(stubResponse.Object);
            CacheMock.Setup(m => m.Get<ViewData>(It.IsAny<IEnumerable<string>>())).Returns(expected);

            var result = view.Query<ViewData>(query);

            Assert.That(result.Results, Is.EqualTo(expected));
        }

        public class QueryResultStub<TReturnType> : IQueryResult<TReturnType> where TReturnType : IIndexResult, new()
        {
            public string Value { get; private set; }
            public uint FoundCount { get; set; }
            public uint StartIndex { get; set; }
            public IEnumerable<TReturnType> Results { get; set; }
        }

        [Test, ExpectedException(typeof(InvalidViewDataException))]
        [TestCase(null)]
        [TestCase("")]
        public void Delete_GivenInvalidInput_Throw(string id)
        {
            Make_MockView().Delete("");
        }

        [Test, ExpectedException(typeof(NotImplementedException))]
        public void GroupedQuery_WhenNotOverridden_Throw()
        {
            Make_MockView().GroupedQuery(null);
        }
    }
}