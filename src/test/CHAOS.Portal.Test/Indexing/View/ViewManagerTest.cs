namespace Chaos.Portal.Test.Indexing.View
{
    using System;
    using System.Collections.Generic;
    using Core.Cache;
    using Core.Exceptions;
    using Core.Indexing.Solr;
    using Core.Indexing.Solr.Request;
    using Core.Indexing.Solr.Response;
    using Core.Indexing.View;

    using Moq;

    using NUnit.Framework;
    using IView = Core.Indexing.View.IView;


    [TestFixture]
    public class ViewManagerTest
    {
        #region Setup

        private ViewManager _viewManager;

        public Mock<IIndex> CoreMock { get; private set; }
        public Mock<ICache> CacheMock { get; private set; }

        [SetUp]
        public void SetUp()
        {
            CacheMock = new Mock<ICache>();
            CoreMock = new Mock<IIndex>();
            _viewManager = new ViewManager(CacheMock.Object);
        }

        #endregion
        #region AddView

        [Test]
        public void AddView_GivenViewInfo_AddToDictionary()
        {
            var viewInfo = Make_MockView();
            _viewManager.AddView(viewInfo);

            var view = _viewManager.GetView(viewInfo.Name);
            Assert.That(view, Is.Not.Null);
        }

        [Test, ExpectedException(typeof(NullReferenceException))]
        public void AddView_NullView_ThrowNullReferenceException()
        {
            _viewManager.AddView(null);
        }

        [Test, ExpectedException(typeof(DuplicateViewException))]
        public void AddView_DuplicateView_ThrowException()
        {
            _viewManager.AddView(Make_MockView());

            _viewManager.AddView(Make_MockView());
        }
        
        [Test, ExpectedException(typeof(ArgumentException))]
        public void AddView_ViewNameIsNull_ThrowException()
        {
            var view = Make_MockView();
            view.Name = null;

            _viewManager.AddView(view);
        }
        
        [Test, ExpectedException(typeof(ArgumentException))]
        public void AddView_ViewNameEmptyString_ThrowException()
        {
            var view = Make_MockView();
            view.Name = String.Empty;

            _viewManager.AddView(view);
        }

        #endregion

        [Test]
        public void Index_OneObject_CallEachViewsIndexMethodWithTheObject()
        {
            _viewManager.AddView(Make_MockView());

            _viewManager.Index(new object());

            CoreMock.Verify(m => m.Index(It.IsAny<IList<IIndexable>>()));
            CacheMock.Verify(m => m.Store(It.IsAny<string>(), It.IsAny<object>()));
        }

        [Test]
        public void Delete_All_CallDeleteOnCore()
        {
            _viewManager.AddView(Make_MockView());

            _viewManager.Delete();

            CoreMock.Verify(m => m.Delete());
        }

        [Test]
        public void Delete_GivenId_CallDeleteOnCacheAndCore()
        {
            _viewManager.AddView(Make_MockView());

            _viewManager.Delete("Id");

            CacheMock.Verify(m => m.Remove(It.IsAny<string>()));
            CoreMock.Verify(m => m.Delete(It.IsAny<string>()));
        }

        [Test, ExpectedException(typeof(InvalidViewDataException))]
        [TestCase(null)]
        [TestCase("")]
        public void Delete_GivenInvalidInput_Throw(string id)
        {
            _viewManager.AddView(Make_MockView());

            _viewManager.GetView("MyView").Delete("");
        }

        [Test, ExpectedException(typeof(NotImplementedException))]
        public void GroupedQuery_WhenNotOverridden_Throw()
        {
            var view = Make_MockView();

            view.GroupedQuery(null);
        }

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

        internal class MockView : IView
        {
            public IList<IViewData> Index(object objectsToIndex)
            {
                return new List<IViewData>(){new ViewData{}};
            }
        }

        public class QueryResultStub<TReturnType> : IQueryResult<TReturnType> where TReturnType : IIndexResult, new()
        {
            public string Value { get; private set; }
            public uint FoundCount { get; set; }
            public uint StartIndex { get; set; }
            public IEnumerable<TReturnType> Results { get; set; }
        }

        private ViewInfo Make_MockView()
        {
            return new ViewInfo("MyView", CacheMock.Object, CoreMock.Object, () => new MockView());
        }
    }

    public class ViewData : IViewData
    {
        public KeyValuePair<string, string> UniqueIdentifier { get{return new KeyValuePair<string, string>("Id", "1");} }
        public IEnumerable<KeyValuePair<string, string>> GetIndexableFields()
        {
            yield return UniqueIdentifier;
        }

        public string Fullname { get; private set; }
    }
}