namespace Chaos.Portal.Test.Index
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CHAOS;
    using CHAOS.Index;
    using CHAOS.Index.Solr;

    using Chaos.Portal.Cache;
    using Chaos.Portal.Data.Dto;
    using Chaos.Portal.Data.Dto.Standard;
    using Chaos.Portal.Index.Standard;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class SessionViewTest
    {
        #region Fields

        private Mock<IIndex> _indexMock;
        private Mock<ICache> _cacheMock;

            #endregion
        #region Initialization

        [SetUp]
        public void SetUp()
        {
            _indexMock = new Mock<IIndex>();
            _cacheMock = new Mock<ICache>();
        }

        #endregion
        #region Index

        [Test]
        public void Index_SingleObject_CallUnderlyingIndexInfrastructureWithFieldsToIndex()
        {
            var view    = Make_SessionView();
            var session = Make_Session();

            var report = view.Index(new object[] { session });

            _indexMock.Verify(m => m.Set(It.IsAny<IEnumerable<IIndexable>>()));
            Assert.AreEqual(1, report.NumberOfIndexedDocuments);
        }

        [Test]
        public void Index_MultipleObjects_CallUnderlyingIndexInfrastructireWithFieldsToIndex()
        {
            var view    = Make_SessionView();
            var session = Make_Session();

            var report = view.Index(new object[] { session, session });

            _indexMock.Verify(m => m.Set(It.IsAny<IEnumerable<IIndexable>>()));
            Assert.AreEqual(2, report.NumberOfIndexedDocuments);
        }

        [Test]
        public void Index_SingleObject_CallUnderlyingCachingInfrastructureWithSerializableDto()
        {
            var view    = Make_SessionView();
            var session = Make_Session();

            var report = view.Index(new object[] { session });

            _cacheMock.Verify(m => m.Store(session));
            Assert.AreEqual(1, report.NumberOfIndexedDocuments);
        }

        #endregion
        #region Query
      
//        [Test]
//        public void Query_SolrQuery_CallsIndexAndRetrievesResultFromCacheAndReturnsDto()
//        {
//            var view    = this.Make_SessionView();
//            var query   = new SolrQuery();
//            var session = Make_Session();
//            _indexMock.Setup(m => m.Get<IndexableSession>(query)).Returns(new Response<IndexableSession>());
//            _cacheMock.Setup(p => p.Get<Session>(It.IsAny<IEnumerable<string>>())).Returns(new []{session});
//
//            var results = view.Query(query);
//
//            Assert.AreEqual(1, results.Count());
//            Assert.AreEqual(session.Guid.ToString(), (results.First() as ISession).Guid.ToString());
//            _indexMock.Verify(m => m.Get<IndexableSession>(query));
//        }

        #endregion
        #region Factory methods

        private static Session Make_Session()
        {
            var session = new Session
                {
                    Guid         = new Guid("149e26c8-4c64-40ca-9338-15305dc17b5f"),
                    UserGuid     = new Guid("c63f54ec-769d-4d6b-9092-96e64a28eaba"),
                    DateCreated  = new DateTime(1990, 5, 5),
                    DateModified = new DateTime(1990, 5, 6)
                };

            return session;
        }

        private SessionView Make_SessionView()
        {
            return new SessionView(_indexMock.Object,_cacheMock.Object);
        }

        #endregion

    }
}