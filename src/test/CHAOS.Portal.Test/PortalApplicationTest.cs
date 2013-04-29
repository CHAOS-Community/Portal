namespace Chaos.Portal.Test
{
    using Chaos.Portal.Core.Exceptions;

    using NUnit.Framework;

    [TestFixture]
    public class PortalApplicationTest : TestBase
    {
        private PortalApplication Make_PortalApplication()
        {
            return new PortalApplication( this.Cache.Object, this.ViewManager.Object, this.PortalRepository.Object, this.LoggingFactory.Object );
        }

        [Test]
        public void Constructor_WithMockObjects_AllPropertiesInitialized()
        {
            var portalApplication = Make_PortalApplication();

            Assert.Greater(portalApplication.Bindings.Count, 0);
            Assert.IsNotNull(portalApplication.Cache);
            Assert.IsNotNull(portalApplication.LoadedExtensions);
            Assert.IsNotNull(portalApplication.Log);
            Assert.IsNotNull(portalApplication.PortalRepository);
            Assert.IsNotNull(portalApplication.ViewManager);
        }

        [Test]
        public void GetExtension_ByType_ReturnAInstanceOfTheExtension()
        {
            var portalApplication = Make_PortalApplication();
            var extensionName     = "test";
            var extensionObject   = new ExtensionMock();
            portalApplication.LoadedExtensions.Add( extensionName, extensionObject );

            var result = portalApplication.GetExtension<ExtensionMock>();

            Assert.That(result, Is.Not.Null);
            Assert.IsInstanceOf<ExtensionMock>(result);
        }

        [Test]
        public void GetExtension_ByTypeCalledTwice_ReturnDifferentInsances()
        {
            var portalApplication = Make_PortalApplication();
            var extensionName = "test";
            var extensionObject = new ExtensionMock();
            portalApplication.LoadedExtensions.Add(extensionName, extensionObject);

            var result1 = portalApplication.GetExtension<ExtensionMock>();
            var result2 = portalApplication.GetExtension<ExtensionMock>();

            Assert.AreNotEqual(result1, result2);
        }

        [Test]
        [ExpectedException(typeof(ExtensionMissingException))]
        public void GetExtension_ByNotLoadedType_ThrowExtensionMissingException()
        {
            var portalApplication = Make_PortalApplication();

            portalApplication.GetExtension<ExtensionMock>();
        }
    }
}
