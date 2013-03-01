namespace Chaos.Portal.Test.Indexing.View
{
    using System;
    using System.Collections.Generic;

    using Chaos.Portal.Cache.Couchbase;
    using Chaos.Portal.Indexing.View;

    using Couchbase;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class ViewManagerTest
    {
        #region Setup

        private ViewManager _viewManager;
        private IDictionary<string, IViewMapping> _dictionary;
        private Mock<IViewMapping> _view;
        private string _viewName;

        private Mock<ICouchbaseClient> _couchbaseClient;

        [SetUp]
        public void SetUp()
        {
            this._couchbaseClient = new Mock<ICouchbaseClient>();
            this._dictionary      = new Dictionary<string, IViewMapping>();
            this._viewManager     = new ViewManager(this._dictionary, new Cache(this._couchbaseClient.Object));
            this._view            = new Mock<IViewMapping>();
            this._viewName        = "ViewName";
        }

        #endregion
        #region AddView

        [Test]
        public void AddView_MockView_TheViewShouldBeStoredInTheDictionary()
        {
            this._viewManager.AddView(this._viewName, this._view.Object);

            Assert.IsTrue(this._dictionary.ContainsKey(this._viewName));
        }

        [Test, ExpectedException(typeof(System.NullReferenceException))]
        public void AddView_NullView_ThrowNullReferenceException()
        {
            this._viewManager.AddView(this._viewName, null);
        }

        [Test, ExpectedException(typeof(NullReferenceException))]
        public void AddView_NullKey_ThrowNullReferenceException()
        {
            this._viewManager.AddView(null, new Mock<IViewMapping>().Object);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void AddView_DuplicateView_ThrowException()
        {
            this._viewManager.AddView(this._viewName, this._view.Object);

            this._viewManager.AddView(this._viewName, this._view.Object);
        }

        #endregion
        #region Index

        [Test]
        public void Index_OneObject_CallEachViewsIndexMethodWithTheObject()
        {
            var expected = new object();
            this._viewManager.AddView(this._viewName, this._view.Object);

            this._viewManager.Index(expected);

            this._view.Verify(m => m.Index(new[]{expected}));
        }

        [Test]
        public void Index_MultipleObjects_CallEachViewsIndexMethodWithTheObjects()
        {
            var expected = new[] { new object(), new object() };
            this._viewManager.AddView(this._viewName, this._view.Object);

            this._viewManager.Index(expected);

            this._view.Verify(m => m.Index(expected));
        }

        #endregion
        #region Query

//        [Test]
//        public void Query_GivenQueryToViewThatExist_CallViewsQueryMethodWithQueryAndReturnResult()
//        {
//            var query = new Mock<IQuery>();
//            var expected = new ;
//            _view.Setup(m => m.Query(query.Object)).Returns(expected);
//
//            var result = _viewManager.Query(_viewName, query.Object);
//
//            
//
//        }

        #endregion
    }
}