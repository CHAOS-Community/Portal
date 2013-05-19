namespace Chaos.Portal.Test.Response
{
    using Chaos.Portal.Core;
    using Chaos.Portal.Core.Data;
    using Chaos.Portal.Core.Exceptions;
    using Chaos.Portal.Core.Request;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class PortalResponseTest
    {
        [Test, ExpectedException(typeof(UnhandledException))]
        public void PortalRepositoryGet_NotSet_ThrowUnhandledException()
        {
            var request = new PortalRequest();

            var result = request.PortalRepository;
        }
        
        [Test]
        public void Constructor_GivenPortalRepository_SetProperty()
        {
            var portalRepository = new Mock<IPortalRepository>();
            var request          = new PortalRequest(Protocol.Latest, null, null, null, portalRepository.Object);

            var result = request.PortalRepository;

            Assert.That(result, Is.EqualTo(portalRepository.Object));
        }
    }
}