namespace Chaos.Portal.Protocol.Tests.v6.Module
{
    using System.Configuration;
    using System.Linq;

    using Chaos.Portal.Core.Data;
    using Chaos.Portal.Extension;
    using Chaos.Portal.v5;
    using Chaos.Portal.v5.Extension;
    using Chaos.Portal.v6.Module;

    using Moq;

    using NUnit.Framework;

    using User = Chaos.Portal.v6.Extension.User;

    [TestFixture]
    public class PortalModuleTest
    {

        [Test]
        public void GetExtension_GivenPortalExtensionNames_ReturnAnInstanceOfTheExtension()
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
        public void GetExtension_GivenGroupExtensionName_ReturnAnInstanceOfTheGroupExtension()
        {
            var module      = new PortalModule();
            var application = new Mock<IPortalApplication>();
            var repository  = new Mock<IPortalRepository>();
            application.SetupGet(p => p.PortalRepository).Returns(repository.Object);
            module.Load(application.Object);

            var groupExtension = module.GetExtension("Group");

            Assert.That(groupExtension, Is.InstanceOf<Group>());
            Assert.That(groupExtension.PortalApplication, Is.EqualTo(application.Object));
        }

        [Test]
        public void GetExtension_GivenSessionExtensionName_ReturnAnInstanceOfTheSessionExtension()
        {
            var module      = new PortalModule();
            var application = new Mock<IPortalApplication>();
            var repository  = new Mock<IPortalRepository>();
            application.SetupGet(p => p.PortalRepository).Returns(repository.Object);
            module.Load(application.Object);

            var groupExtension = module.GetExtension("Session");

            Assert.That(groupExtension, Is.InstanceOf<Session>());
            Assert.That(groupExtension.PortalApplication, Is.EqualTo(application.Object));
        }

        [Test]
        public void GetExtension_GivenSubscriptionExtensionName_ReturnAnInstanceOfTheSubscriptionExtension()
        {
            var module      = new PortalModule();
            var application = new Mock<IPortalApplication>();
            var repository  = new Mock<IPortalRepository>();
            application.SetupGet(p => p.PortalRepository).Returns(repository.Object);
            module.Load(application.Object);

            var groupExtension = module.GetExtension("Subscription");

            Assert.That(groupExtension, Is.InstanceOf<Subscription>());
            Assert.That(groupExtension.PortalApplication, Is.EqualTo(application.Object));
        }

        [Test]
        public void GetExtension_GivenUserExtensionName_ReturnAnInstanceOfTheUserExtension()
        {
            var module      = new PortalModule();
            var application = new Mock<IPortalApplication>();
            var repository  = new Mock<IPortalRepository>();
            application.SetupGet(p => p.PortalRepository).Returns(repository.Object);
            module.Load(application.Object);

            var groupExtension = module.GetExtension("User");

            Assert.That(groupExtension, Is.InstanceOf<User>());
            Assert.That(groupExtension.PortalApplication, Is.EqualTo(application.Object));
        }

        [Test]
        public void GetExtension_GivenUserSettingsExtensionName_ReturnAnInstanceOfTheUserSettingsExtension()
        {
            var module      = new PortalModule();
            var application = new Mock<IPortalApplication>();
            var repository  = new Mock<IPortalRepository>();
            application.SetupGet(p => p.PortalRepository).Returns(repository.Object);
            module.Load(application.Object);

            var groupExtension = module.GetExtension("UserSettings");

            Assert.That(groupExtension, Is.InstanceOf<UserSettings>());
            Assert.That(groupExtension.PortalApplication, Is.EqualTo(application.Object));
        }

        [Test]
        public void GetExtension_GivenViewExtensionName_ReturnAnInstanceOfTheViewExtension()
        {
            var module      = new PortalModule();
            var application = new Mock<IPortalApplication>();
            var repository  = new Mock<IPortalRepository>();
            application.SetupGet(p => p.PortalRepository).Returns(repository.Object);
            module.Load(application.Object);

            var groupExtension = module.GetExtension("View");

            Assert.That(groupExtension, Is.InstanceOf<View>());
            Assert.That(groupExtension.PortalApplication, Is.EqualTo(application.Object));
        }

        [Test, ExpectedException(typeof(ConfigurationErrorsException))]
        public void GetExtension_LoadNotCalledPrior_ThrowsConfigurationException()
        {
            var module = new PortalModule();
            var application = new Mock<IPortalApplication>();
            var repository = new Mock<IPortalRepository>();
            application.SetupGet(p => p.PortalRepository).Returns(repository.Object);

            module.GetExtension("View");
        }

        [Test]
        public void GetExtensionNames_All_ReturnAllPortalExtensionNames()
        {
            var module = new PortalModule();

            var names = module.GetExtensionNames().ToList();

            Assert.That(names[0], Is.EqualTo("ClientSettings"));
            Assert.That(names[1], Is.EqualTo("Group"));
            Assert.That(names[2], Is.EqualTo("Session"));
            Assert.That(names[3], Is.EqualTo("Subscription"));
            Assert.That(names[4], Is.EqualTo("User"));
            Assert.That(names[5], Is.EqualTo("UserSettings"));
            Assert.That(names[6], Is.EqualTo("View"));
            Assert.That(names.Count, Is.EqualTo(7));
        }
    }
}