namespace Chaos.Portal.Test.Indexing.View
{
    using System;
    using System.Collections.Generic;
    using Core.Exceptions;
    using Core.Indexing.Solr;
    using Core.Indexing.View;

    using Moq;

    using NUnit.Framework;


    [TestFixture]
    public class ViewManagerTest : TestBase
    {
        #region Setup

        private ViewManager _viewManager;

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();

            _viewManager = new ViewManager(CacheMock.Object);
        }

        #endregion

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
        
        [Test]
        public void AddView_DuplicateViewWithForceTrue_ReplaceView()
        {
            _viewManager.AddView(Make_MockView());
            var expected = Make_MockView();
            
            _viewManager.AddView(expected, true);

            var actual = _viewManager.GetView(expected.Name);
            Assert.That(actual, Is.EqualTo(expected));
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
    }
}