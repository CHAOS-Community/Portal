namespace Chaos.Portal.Protocol.Tests.v5.Module
{
    using Chaos.Portal.Core;
    using Chaos.Portal.Core.Data;
    using Chaos.Portal.Module;
    using Chaos.Portal.v5.Extension;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class PortalModuleTest
    {
        [Test]
        public void GetExtension_GivenClientSettings_ReturnAnInstanceOfTheExtension()
        {
            var module      = new PortalModule();
            var application = new Mock<IPortalApplication>();
            var repository  = new Mock<IPortalRepository>();
            application.SetupGet(p => p.PortalRepository).Returns(repository.Object);
            module.Load(application.Object);

            var clientSettingsExtension = (ClientSettings)module.GetExtension(Protocol.V5, "ClientSettings");

            Assert.That(clientSettingsExtension.PortalApplication, Is.EqualTo(application.Object));
        }

        [Test]
        public void GetExtension_GivenGroup_ReturnAnInstanceOfTheExtension()
        {
            var module      = new PortalModule();
            var application = new Mock<IPortalApplication>();
            var repository  = new Mock<IPortalRepository>();
            application.SetupGet(p => p.PortalRepository).Returns(repository.Object);
            module.Load(application.Object);

            var clientSettingsExtension = (Group)module.GetExtension(Protocol.V5, "Group");

            Assert.That(clientSettingsExtension.PortalApplication, Is.EqualTo(application.Object));
        }

        [Test]
        public void GetExtension_GivenSubscription_ReturnAnInstanceOfTheExtension()
        {
            var module      = new PortalModule();
            var application = new Mock<IPortalApplication>();
            var repository  = new Mock<IPortalRepository>();
            application.SetupGet(p => p.PortalRepository).Returns(repository.Object);
            module.Load(application.Object);

            var clientSettingsExtension = (Subscription)module.GetExtension(Protocol.V5, "Subscription");

            Assert.That(clientSettingsExtension.PortalApplication, Is.EqualTo(application.Object));
        }

        [Test]
        public void GetExtension_GivenUser_ReturnAnInstanceOfTheExtension()
        {
            var module      = new PortalModule();
            var application = new Mock<IPortalApplication>();
            var repository  = new Mock<IPortalRepository>();
            application.SetupGet(p => p.PortalRepository).Returns(repository.Object);
            module.Load(application.Object);

            var clientSettingsExtension = (Portal.v5.Extension.User) module.GetExtension(Protocol.V5, "User");

            Assert.That(clientSettingsExtension.PortalApplication, Is.EqualTo(application.Object));
        }
    }
}