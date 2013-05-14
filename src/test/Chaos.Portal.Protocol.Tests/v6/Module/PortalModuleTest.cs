namespace Chaos.Portal.Protocol.Tests.v6.Module
{
    using System.Configuration;
    using System.Linq;

    using Chaos.Portal.Core;
    using Chaos.Portal.Core.Data;
    using Chaos.Portal.Core.Module;
    using Chaos.Portal.Module;
    using Chaos.Portal.v5;
    using Chaos.Portal.v5.Extension;

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

            var clientSettingsExtension = (ClientSettings) module.GetExtension(Protocol.V6, "ClientSettings");

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

            var groupExtension = (Group) module.GetExtension(Protocol.V6, "Group");

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

            var sessionExtension = (Chaos.Portal.v6.Extension.Session) module.GetExtension(Protocol.V6, "Session");

            Assert.That(sessionExtension.PortalApplication, Is.EqualTo(application.Object));
        }

        [Test]
        public void GetExtension_GivenSubscriptionExtensionName_ReturnAnInstanceOfTheSubscriptionExtension()
        {
            var module      = new PortalModule();
            var application = new Mock<IPortalApplication>();
            var repository  = new Mock<IPortalRepository>();
            application.SetupGet(p => p.PortalRepository).Returns(repository.Object);
            module.Load(application.Object);

            var groupExtension = (Subscription) module.GetExtension(Protocol.V6, "Subscription");

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

            var groupExtension = (User) module.GetExtension(Protocol.V6, "User");

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

            var groupExtension = (UserSettings) module.GetExtension(Protocol.V6, "UserSettings");

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

            var groupExtension = (View) module.GetExtension(Protocol.V6, "View");

            Assert.That(groupExtension.PortalApplication, Is.EqualTo(application.Object));
        }

        [Test, ExpectedException(typeof(ConfigurationErrorsException))]
        public void GetExtension_LoadNotCalledPrior_ThrowsConfigurationException()
        {
            var module = new PortalModule();
            var application = new Mock<IPortalApplication>();
            var repository = new Mock<IPortalRepository>();
            application.SetupGet(p => p.PortalRepository).Returns(repository.Object);

            module.GetExtension(Protocol.V6, "View");
        }

        [Test]
        public void GetExtensionNames_All_ReturnAllPortalExtensionNames()
        {
            var module = new PortalModule();

            var names = module.GetExtensionNames(Protocol.V6).ToList();

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