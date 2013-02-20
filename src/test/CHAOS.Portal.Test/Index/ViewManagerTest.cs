namespace Chaos.Portal.Test.Index
{
    using System;
    using System.Collections.Generic;

    using CHAOS.Index;

    using Chaos.Portal.Index;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class ViewManagerTest
    {
        #region Setup

        private ViewManager _viewManager;
        private IDictionary<string, IView> _dictionary;
        private Mock<IView> _view;
        private string _viewName;

        [SetUp]
        public void SetUp()
        {
            _dictionary = new Dictionary<string, IView>();
            _viewManager = new ViewManager(_dictionary);
            _view = new Mock<IView>();
            _viewName = "ViewName";
        }

        #endregion
        #region AddView

        [Test]
        public void AddView_MockView_TheViewShouldBeStoredInTheDictionary()
        {
            _viewManager.AddView(_viewName, _view.Object);

            Assert.IsTrue(_dictionary.ContainsKey(_viewName));
        }

        [Test, ExpectedException(typeof(System.NullReferenceException))]
        public void AddView_NullView_ThrowNullReferenceException()
        {
            _viewManager.AddView(_viewName, null);
        }

        [Test, ExpectedException(typeof(NullReferenceException))]
        public void AddView_NullKey_ThrowNullReferenceException()
        {
            _viewManager.AddView(null, new Mock<IView>().Object);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void AddView_DuplicateView_ThrowException()
        {
            _viewManager.AddView(_viewName, _view.Object);

            _viewManager.AddView(_viewName, _view.Object);
        }

        #endregion
        #region Index

        [Test]
        public void Index_OneObject_CallEachViewsIndexMethodWithTheObject()
        {
            var expected = new object();
            _viewManager.AddView(_viewName, _view.Object);

            _viewManager.Index(expected);

            _view.Verify(m => m.Index(new[]{expected}));
        }

        [Test]
        public void Index_MultipleObjects_CallEachViewsIndexMethodWithTheObjects()
        {
            var expected = new[] { new object(), new object() };
            _viewManager.AddView(_viewName, _view.Object);

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