namespace Chaos.Portal.Test.Indexing.View
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.Data.Model;
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
            public string Value { get; set; }
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

        [Test]
        public void GroupedQuery_WhenNotOverridden_Throw()
        {
            var stubResponse = new Mock<IIndexResponse<IdResult>>();
            var queryResultStub = new QueryResultStub<IdResult> { Results = new[] { new IdResult { Id = "id", Score = 1 } }, FoundCount = 1, StartIndex = 1, Value = "Group1"};
            var queryResultGroupStub = new QueryResultGroupStub { Groups = new[] { queryResultStub }, FoundCount = 1};
            var query = new SolrQuery();
            var expected = new[] { new ViewData() };
            stubResponse.Setup(p => p.QueryResultGroups).Returns(new[] { queryResultGroupStub });
            CoreMock.Setup(m => m.Query(query)).Returns(stubResponse.Object);
            CacheMock.Setup(m => m.Get<ViewData>(It.IsAny<IEnumerable<string>>())).Returns(expected);

            var result = Make_MockView().GroupedQuery<ViewData>(query);

            Assert.That(result.Groups, Is.Not.Empty);
            Assert.That(result.Groups.First().FoundCount, Is.EqualTo(1));
            Assert.That(result.Groups.First().StartIndex, Is.EqualTo(1));
            Assert.That(result.Groups.First().Value, Is.EqualTo("Group1"));
            Assert.That(result.Groups.First().Results, Is.SameAs(expected));
        }
    }

    public class QueryResultGroupStub : IQueryResultGroup<IdResult>
    {
        public string Name { get; set; }
        public uint FoundCount { get; set; }
        public IList<IQueryResult<IdResult>> Groups { get; set; }
    }
}