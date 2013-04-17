namespace Chaos.Portal.Test.Module
{
    using Chaos.Portal.Core.Data;
    using Chaos.Portal.Extension;
    using Chaos.Portal.Module;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class PortalModuleTest
    {

        [Test]
        public void Load_InitializeModule_ShouldCallWithPortalApplicationOnAllExtensions()
        {
            var module      = new PortalModule();
            var application = new Mock<IPortalApplication>();
            var repository  = new Mock<IPortalRepository>();
            application.SetupGet(p => p.PortalRepository).Returns(repository.Object);

            module.Load(application.Object);

            var extensions = module.Extensions;
            Assert.That(extensions[0].PortalApplication, Is.EqualTo(application.Object));
            Assert.That(extensions[1].PortalApplication, Is.EqualTo(application.Object));
            Assert.That(extensions[2].PortalApplication, Is.EqualTo(application.Object));
            Assert.That(extensions[3].PortalApplication, Is.EqualTo(application.Object));
            Assert.That(extensions[4].PortalApplication, Is.EqualTo(application.Object));
            Assert.That(extensions[5].PortalApplication, Is.EqualTo(application.Object));
            Assert.That(extensions[6].PortalApplication, Is.EqualTo(application.Object));
        }

        [Test]
        public void Load_InitializeModule_ShouldLoadAddExtensionsToPortalApplication()
        {
            var module      = new PortalModule();
            var application = new Mock<IPortalApplication>();
            var repository  = new Mock<IPortalRepository>();
            application.SetupGet(p => p.PortalRepository).Returns(repository.Object);

            module.Load(application.Object);

            application.Verify(m => m.AddExtension(It.IsAny<string>(), It.IsAny<IExtension>()),Times.Exactly(7));
        }
    }
}