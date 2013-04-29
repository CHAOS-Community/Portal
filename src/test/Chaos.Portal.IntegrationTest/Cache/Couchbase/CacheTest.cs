namespace Chaos.Portal.IntegrationTest.Cache.Couchbase
{
    using System;
    using System.Linq;

    using Chaos.Portal.Core.Data.Model;

    using NUnit.Framework;

    using Chaos.Portal.Cache.Couchbase;

    using global::Couchbase;

    [TestFixture]
    public class CacheTest
    {
        #region Setup

        private Cache _cache;

        [SetUp]
        public void SetUp()
        {
            var client = new CouchbaseClient();
            _cache     = new Cache(client);
        }

        private static Session Make_Session()
        {
            return new Session
            {
                Guid        = new Guid("10000000-0000-0000-0000-000000000001"),
                UserGuid    = new Guid("20000000-0000-0000-0000-000000000002"),
                DateCreated = new DateTime(2000, 01, 01)
            };
        }

        #endregion
        
        [Test]
        public void Store_GivenSessionDto_ReturnTrue()
        {
            var session = Make_Session();

            var result = _cache.Store("test", session);

            Assert.IsTrue(result);
        }

        [Test]
        public void Get_GivenTestKey_ReturnSession()
        {
            var expected = Make_Session();
            _cache.Store("test", expected);

            var actual = _cache.Get<Session>("test");

            Assert.That(actual.Guid, Is.EqualTo(expected.Guid));
            Assert.That(actual.UserGuid, Is.EqualTo(expected.UserGuid));
            Assert.That(actual.DateCreated, Is.EqualTo(expected.DateCreated));
            Assert.That(actual.DocumentID, Is.EqualTo(expected.DocumentID));
        }
        
        [Test]
        public void Get_GivenMultipleKey_ReturnSessions()
        {
            var expected1 = Make_Session();
            var expected2 = Make_Session();
            expected2.Guid = new Guid("11000000-0000-0000-0000-000000000011");
            
            _cache.Store("test1", expected1);
            _cache.Store("test2", expected2);
            var actual = _cache.Get<Session>(new string[] { "test1", "test2" }).ToList();

            Assert.That(actual[0].Guid, Is.EqualTo(expected1.Guid));
            Assert.That(actual[0].UserGuid, Is.EqualTo(expected1.UserGuid));
            Assert.That(actual[0].DateCreated, Is.EqualTo(expected1.DateCreated));
            Assert.That(actual[0].DocumentID, Is.EqualTo(expected1.DocumentID));
            Assert.That(actual[1].Guid, Is.EqualTo(expected2.Guid));
            Assert.That(actual[1].UserGuid, Is.EqualTo(expected2.UserGuid));
            Assert.That(actual[1].DateCreated, Is.EqualTo(expected2.DateCreated));
            Assert.That(actual[1].DocumentID, Is.EqualTo(expected2.DocumentID));
        }
    }
}