namespace Chaos.Portal.Test.Indexing.View
{
    using System;
    using System.Collections.Generic;

    using Core.Cache.Couchbase;
    using Core.Exceptions;
    using Core.Indexing.View;

    using Couchbase;

    using Moq;

    using NUnit.Framework;
    using IView = Core.Indexing.View.IView;


    [TestFixture]
    public class ViewManagerTest
    {
        #region Setup

        private ViewManager _viewManager;
        private IDictionary<string, IView> _dictionary;
        private Mock<IView> _view;
        private string _viewName;

        private Mock<ICouchbaseClient> _couchbaseClient;

        [SetUp]
        public void SetUp()
        {
            _couchbaseClient = new Mock<ICouchbaseClient>();
            _dictionary      = new Dictionary<string, IView>();
            _viewManager     = new ViewManager(this._dictionary, new Cache(this._couchbaseClient.Object));
            _view            = new Mock<IView>();
            _viewName        = "ViewName";
            _view.SetupGet(p => p.Name).Returns(_viewName);
        }

        #endregion
        #region AddView

        [Test]
        public void AddView_MockView_TheViewShouldBeStoredInTheDictionary()
        {
            _viewManager.AddView(_view.Object);

            Assert.IsTrue(_dictionary.ContainsKey(_viewName));
        }

        [Test, ExpectedException(typeof(NullReferenceException))]
        public void AddView_NullView_ThrowNullReferenceException()
        {
            this._viewManager.AddView(null);
        }

        [Test, ExpectedException(typeof(DuplicateViewException))]
        public void AddView_DuplicateView_ThrowException()
        {
            _viewManager.AddView(_view.Object);

            _viewManager.AddView(_view.Object);
        }
        
        [Test, ExpectedException(typeof(ArgumentException))]
        public void AddView_ViewNameIsNull_ThrowException()
        {
            _view.SetupGet(p => p.Name).Returns((string)null);

            _viewManager.AddView(_view.Object);
        }
        
        [Test, ExpectedException(typeof(ArgumentException))]
        public void AddView_ViewNameEmptystring_ThrowException()
        {
            _view.SetupGet(p => p.Name).Returns(string.Empty);

            _viewManager.AddView(_view.Object);
        }

        #endregion
        #region Index

        [Test]
        public void Index_OneObject_CallEachViewsIndexMethodWithTheObject()
        {
            var expected = new object();
            _viewManager.AddView(_view.Object);

            _viewManager.Index(expected);

            _view.Verify(m => m.Index(new[]{expected}));
        }

        [Test]
        public void Index_MultipleObjects_CallEachViewsIndexMethodWithTheObjects()
        {
            var expected = new[] { new object(), new object() };
            _viewManager.AddView(_view.Object);

            _viewManager.Index(expected);

            _view.Verify(m => m.Index(expected));
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