namespace Chaos.Portal.Test.Indexing.View
{
    using System;
    using System.Collections.Generic;
    using Core.Cache;
    using Core.Cache.Couchbase;
    using Core.Exceptions;
    using Core.Indexing.Solr;
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
        private Mock<IView> _view;
        private string _viewName;

        public Mock<ICache> CacheMock { get; private set; }

        [SetUp]
        public void SetUp()
        {
            CacheMock = new Mock<ICache>();

            _viewManager = new ViewManager(CacheMock.Object);
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

            var view = _viewManager.GetView(_viewName);
            Assert.That(view, Is.EqualTo(_view.Object));
        }
        
        [Test]
        public void AddView_ViewFactoryMethod_AddToDictionary()
        {
            _viewManager.AddView(_viewName ,() => _view.Object);

            var view = _viewManager.GetView(_viewName);
            Assert.That(view, Is.EqualTo(_view.Object));
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
            var coreMock = new Mock<IIndex>();
            _viewManager.AddView("MyView", () => new MockView{ Core = coreMock.Object});

            _viewManager.Index(new object());

            coreMock.Verify(m => m.Index(It.IsAny<IList<IIndexable>>()));
            CacheMock.Verify(m => m.Store(It.IsAny<string>(), It.IsAny<object>()));
        }

        [Test]
        public void Delete_GivenId_CallDeleteOnCache()
        {
            var coreMock = new Mock<IIndex>();
            _viewManager.AddView("MyView", () => new MockView { Core = coreMock.Object });

            _viewManager.GetView("MyView").Delete("Id");

            CacheMock.Verify(m => m.Remove(It.IsAny<string>()));
            coreMock.Verify(m => m.Delete(It.IsAny<string>()));
        }

        [Test, ExpectedException(typeof(InvalidViewDataException))]
        [TestCase(null)]
        [TestCase("")]
        public void Delete_GivenInvalidInput_Throw(string id)
        {
            _viewManager.AddView("MyView", () => new MockView());

            _viewManager.GetView("MyView").Delete(id);
        }
        
        public class MockView : AView
        {
            public MockView() : base("MyView")
            {
            }

            public override IList<IViewData> Index(object objectsToIndex)
            {
                return new List<IViewData>(){new ViewData{}};
            }
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