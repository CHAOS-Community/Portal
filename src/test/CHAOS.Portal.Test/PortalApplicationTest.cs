using Chaos.Portal.Standard;
using NUnit.Framework;

namespace Chaos.Portal.Test
{
    [TestFixture]
    public class PortalApplicationTest : TestBase
    {
        [Test]
        public void Should_Initialize_Application()
        {
            var portalApplication = new PortalApplication(Cache.Object, Index.Object, PortalRepository.Object, LoggingFactory.Object);

            Assert.Greater(portalApplication.Bindings.Count, 0);
            Assert.IsNotNull(portalApplication.Cache);
            Assert.IsNotNull(portalApplication.IndexManager);
            Assert.IsNotNull(portalApplication.LoadedExtensions);
            Assert.IsNotNull(portalApplication.Log);
            Assert.IsNotNull(portalApplication.PortalRepository);
        }

        [Test]
        public void Should_Process_Request()
        {
            PortalResponse.SetupGet(p => p.Header).Returns(PortalHeader.Object);
            PortalRequest.SetupGet(p => p.Extension).Returns("Test");
            PortalRequest.SetupGet(p => p.ReturnFormat).Returns(ReturnFormat.XML);

            var portalApplication = new PortalApplication(Cache.Object, Index.Object, PortalRepository.Object, LoggingFactory.Object);
            portalApplication.LoadedExtensions.Add("Test", Extension.Object);

            var response = portalApplication.ProcessRequest(PortalRequest.Object, PortalResponse.Object);

            Assert.IsNotNull(response);
        }
    }
}
