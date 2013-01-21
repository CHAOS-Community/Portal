namespace Chaos.Portal.Test.Index
{
    using System.Collections.Generic;
    using System.Linq;

    using Chaos.Portal.Data.Dto.Standard;
    using Chaos.Portal.Index;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class ViewManagerTest
    {
        #region Setup

        private ViewManager _viewManager;
        private Mock<IDictionary<string, IView>> _dictionary;
        private Mock<IView> _view;
        private string _viewName;

        [SetUp]
        public void SetUp()
        {
            _dictionary = new Mock<IDictionary<string, IView>>();
            _viewManager = new ViewManager(_dictionary.Object);
            _view = new Mock<IView>();
            _viewName = "ViewName";
        }

        #endregion
        #region AddView

        [Test]
        public void AddView_MockView_TheViewShouldBeStoredInTheDictionary()
        {
            _dictionary.Setup(m => m.ContainsKey(_viewName)).Returns(false);

            _viewManager.AddView(_viewName, _view.Object);

            _dictionary.Verify(m => m.Add(_viewName, _view.Object));
        }

        [Test, ExpectedException(typeof(System.NullReferenceException))]
        public void AddView_NullView_ThrowNullReferenceException()
        {
            _viewManager.AddView(_viewName, null);
        }

        [Test, ExpectedException(typeof(System.NullReferenceException))]
        public void AddView_NullKey_ThrowNullReferenceException()
        {
            _viewManager.AddView(null, new Mock<IView>().Object);
        }

        [Test]
        public void AddView_DuplicateView_ThrowEsception()
        {
            _dictionary.Setup(m => m.ContainsKey(_viewName)).Returns(true);

            _viewManager.AddView(_viewName, _view.Object);

            _dictionary.Verify(m => m.Add(_viewName, _view.Object), Times.Never());
        }

        #endregion
        #region Index

        [Test]
        public void Index_Object_ReturnIndexReportWithOneIndexedObject()
        {
            var obj = new object();

            _dictionary.SetupGet(p => p.Values).Returns((new[] { _view.Object }));

            _view.Setup(m => m.Index(new[] { obj })).Returns(new ViewReport { NumberOfIndexedDocuments = 1 });

            var report = _viewManager.Index(obj);

            _view.Verify(m => m.Index(new []{obj}));
            Assert.AreEqual(1, report.Views.First().NumberOfIndexedDocuments);
        }

        [Test]
        public void Index_Object_ReturnIndexReportWithZeroIndexedObjects()
        {
            var obj = new object();

            _dictionary.SetupGet(p => p.Values).Returns((new[] { _view.Object }));

            _view.Setup(m => m.Index(new[] { obj })).Returns(new ViewReport ());

            var report = _viewManager.Index(obj);

            _view.Verify(m => m.Index(new[] { obj }));
            Assert.AreEqual(0, report.Views.Count);
        }

        #endregion
    }
}