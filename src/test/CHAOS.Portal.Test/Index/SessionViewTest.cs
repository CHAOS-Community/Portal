namespace Chaos.Portal.Test.Index
{
    using System;
    using System.Collections.Generic;

    using CHAOS;
    using CHAOS.Index;

    using Chaos.Portal.Data.Dto;
    using Chaos.Portal.Index.Standard;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class SessionViewTest
    {
        #region Fields

        private Mock<IIndex> _indexMock;

        #endregion
        #region Initialization

        [SetUp]
        public void SetUp()
        {
            _indexMock = new Mock<IIndex>();
        }

        #endregion
        #region Index

        [Test]
        public void Index_SingleObject_CallUnderlyingIndexInfrastructureWithFieldsToIndex()
        {
            var view    = Make_SessionView();
            var session = Make_SessionMock();

            var report = view.Index(new object[] { session.Object });

            _indexMock.Verify(m => m.Set(It.IsAny<IEnumerable<IIndexable>>()));
            Assert.AreEqual(1, report.NumberOfIndexedDocuments);
        }

        [Test]
        public void Index_MultipleObjects_CallUnderlyingIndexInfrastructireWithFieldsToIndex()
        {
            var view    = Make_SessionView();
            var session = Make_SessionMock();

            var report = view.Index(new object[] { session.Object, session.Object });

            _indexMock.Verify(m => m.Set(It.IsAny<IEnumerable<IIndexable>>()));
            Assert.AreEqual(2, report.NumberOfIndexedDocuments);
        }

        #endregion
        #region Factory methods

        private static Mock<ISession> Make_SessionMock()
        {
            var session = new Mock<ISession>();

            session.SetupGet(p => p.GUID).Returns(new UUID("149e26c8-4c64-40ca-9338-15305dc17b5f"));
            session.SetupGet(p => p.UserGUID).Returns(new UUID("c63f54ec-769d-4d6b-9092-96e64a28eaba"));
            session.SetupGet(p => p.DateCreated).Returns(new DateTime(1990, 5, 5));
            session.SetupGet(p => p.DateModified).Returns(new DateTime(1990, 5, 6));

            return session;
        }

        private SessionView Make_SessionView()
        {
            return new SessionView(_indexMock.Object);
        }

        #endregion

    }
}