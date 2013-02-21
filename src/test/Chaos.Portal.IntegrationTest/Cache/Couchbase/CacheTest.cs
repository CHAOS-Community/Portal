namespace Chaos.Portal.IntegrationTest.Cache.Couchbase
{
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

        #endregion
    }
}