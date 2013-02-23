namespace Chaos.Portal.Test.Module
{
    using Chaos.Portal.Module;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class PortalModuleTest
    {
        [Test]
        public void WithPortalApplication_InitializeModule_ShouldCallWithPortalApplicationOnAllExtensions()
        {
            var module      = new PortalModule();
            var application = new Mock<IPortalApplication>();

            module.WithPortalApplication(application.Object);

            var extensions = module.Extensions;
            Assert.That(extensions[0].PortalApplication, Is.EqualTo(application.Object));
            Assert.That(extensions[1].PortalApplication, Is.EqualTo(application.Object));
            Assert.That(extensions[2].PortalApplication, Is.EqualTo(application.Object));
            Assert.That(extensions[3].PortalApplication, Is.EqualTo(application.Object));
            Assert.That(extensions[4].PortalApplication, Is.EqualTo(application.Object));
            Assert.That(extensions[5].PortalApplication, Is.EqualTo(application.Object));
        }
        
//        [Test]
//        public void WithConfiguration_InitializeModule_ShouldCallWithConfigurationOnAllExtensions()
//        {
//            var module        = new PortalModule();
//            var configuration = "";
//
//            module.WithConfiguration(configuration);
//
//            var extensions = module.Extensions;
//            Assert.That(extensions[0]., Is.EqualTo(application.Object));
//            Assert.That(extensions[1].PortalApplication, Is.EqualTo(application.Object));
//            Assert.That(extensions[2].PortalApplication, Is.EqualTo(application.Object));
//            Assert.That(extensions[3].PortalApplication, Is.EqualTo(application.Object));
//            Assert.That(extensions[4].PortalApplication, Is.EqualTo(application.Object));
//            Assert.That(extensions[5].PortalApplication, Is.EqualTo(application.Object));
//        }
    }
}