namespace Chaos.Portal.Test.Indexing.View
{
    using System.Collections.Generic;
    using Core.Cache;
    using Core.Indexing.Solr;
    using Core.Indexing.View;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class TestBase
    {
        protected Mock<IIndex> CoreMock { get; set; }
        protected Mock<ICache> CacheMock { get; set; }

        [SetUp]
        public virtual void SetUp()
        {
            CacheMock = new Mock<ICache>();
            CoreMock = new Mock<IIndex>();
        }

        protected class MockView : IView
        {
            public IList<IViewData> Index(object objectsToIndex)
            {
                return new List<IViewData>(){new ViewData{}};
            }
        }

        protected class ViewData : IViewData
        {
            public string Type = "ViewData";
            public KeyValuePair<string, string> UniqueIdentifier { get { return new KeyValuePair<string, string>("Id", "1"); } }
            public IEnumerable<KeyValuePair<string, string>> GetIndexableFields()
            {
                yield return UniqueIdentifier;
            }

            public string Fullname { get; private set; }
        }

        protected ViewInvoker Make_MockView()
        {
            return new ViewInvoker("MyView", CacheMock.Object, CoreMock.Object, () => new MockView());
        }
    }
}
