namespace Chaos.Portal.Protocol.Tests.v5.Module
{
    using Chaos.Portal.Core;
    using Chaos.Portal.Core.Data;
    using Chaos.Portal.v5.Extension;
    using Chaos.Portal.v5.Module;

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

            var clientSettingsExtension = module.GetExtension("ClientSettings");

            Assert.That(clientSettingsExtension, Is.InstanceOf<ClientSettings>());
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

            var clientSettingsExtension = module.GetExtension("Group");

            Assert.That(clientSettingsExtension, Is.InstanceOf<Group>());
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

            var clientSettingsExtension = module.GetExtension("Subscription");

            Assert.That(clientSettingsExtension, Is.InstanceOf<Subscription>());
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

            var clientSettingsExtension = module.GetExtension("User");

            Assert.That(clientSettingsExtension, Is.InstanceOf<Portal.v5.Extension.User>());
            Assert.That(clientSettingsExtension.PortalApplication, Is.EqualTo(application.Object));
        }
    }
}