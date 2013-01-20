namespace Chaos.Portal.Test
{
    using NUnit.Framework;

    [TestFixture]
    public class PortalApplicationTest : TestBase
    {
        [Test]
        public void Should_Initialize_Application()
        {
            var portalApplication = new PortalApplication(Cache.Object, Index.Object, ViewManager.Object, PortalRepository.Object, LoggingFactory.Object);

            Assert.Greater(portalApplication.Bindings.Count, 0);
            Assert.IsNotNull(portalApplication.Cache);
            Assert.IsNotNull(portalApplication.IndexManager);
            Assert.IsNotNull(portalApplication.LoadedExtensions);
            Assert.IsNotNull(portalApplication.Log);
            Assert.IsNotNull(portalApplication.PortalRepository);
            Assert.IsNotNull(portalApplication.ViewManager);
        }
    }
}
