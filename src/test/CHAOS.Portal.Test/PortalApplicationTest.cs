namespace Chaos.Portal.Test
{
    using System.Collections.Generic;

    using Chaos.Portal.Core.Exceptions;
    using Chaos.Portal.Extension;
    using Chaos.Portal.Module;
    using Chaos.Portal.Request;
    using Chaos.Portal.Response;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class PortalApplicationTest : TestBase
    {
        private PortalApplication Make_PortalApplication()
        {
            return new PortalApplication( Cache.Object, ViewManager.Object, PortalRepository.Object, LoggingFactory.Object );
        }

        [Test]
        public void Constructor_WithMockObjects_AllPropertiesInitialized()
        {
            var portalApplication = Make_PortalApplication();

            Assert.Greater(portalApplication.Bindings.Count, 0);
            Assert.IsNotNull(portalApplication.Cache);
            Assert.IsNotNull(portalApplication.LoadedModules);
            Assert.IsNotNull(portalApplication.Log);
            Assert.IsNotNull(portalApplication.PortalRepository);
            Assert.IsNotNull(portalApplication.ViewManager);
        }

        [Test]
        public void GetExtension_ByType_ReturnAInstanceOfTheExtension()
        {
            var application = Make_PortalApplication();
            var extension = new ExtensionMock();
            var module = new Mock<IModule>();
            module.Setup(m => m.GetExtensionNames()).Returns(new[] { "ExtensionMock" });
            module.Setup(m => m.GetExtension<ExtensionMock>()).Returns(extension);
            application.AddModule(module.Object);

            var result = application.GetExtension<ExtensionMock>();

            Assert.That(result, Is.Not.Null);
            Assert.IsInstanceOf<ExtensionMock>(result);
        }

        [Test]
        [ExpectedException(typeof(ExtensionMissingException))]
        public void GetExtension_ByNotLoadedType_ThrowExtensionMissingException()
        {
            var portalApplication = Make_PortalApplication();

            portalApplication.GetExtension<ExtensionMock>();
        }

        [Test]
        public void AddModuel_GivenPortalModule_AddTheModuleToLoadedModules()
        {
            var module      = new Mock<IModule>();
            var extension   = new Mock<IExtension>();
            var application = Make_PortalApplication();
            module.Setup(m => m.GetExtensionNames()).Returns(new[] { "test" });
            module.Setup(m => m.GetExtension("test")).Returns(extension.Object);

            application.AddModule(module.Object);

            Assert.That(application.LoadedModules.ContainsKey("test"), Is.True);
            Assert.That(application.LoadedModules["test"], Is.EqualTo(module.Object));
        }

        [Test]
        public void ProcessRequest_RequestWithReturnFormatXml_ReturnResponseHasReturnFormatXml()
        {
            var application = Make_PortalApplication();
            var extension   = new ExtensionMock();
            var module      = new Mock<IModule>();
            var parameters  = new Dictionary<string, string> { { "format", "XML" } };
            var request     = new PortalRequest("test", "test", parameters);
            module.Setup(m => m.GetExtensionNames()).Returns(new[] { "test" });
            module.Setup(m => m.GetExtension("test")).Returns(extension);
            application.AddModule(module.Object);

            var response = application.ProcessRequest(request);

            Assert.That(response.Header.ReturnFormat, Is.EqualTo(ReturnFormat.XML));
        }

        [Test]
        public void ProcessRequest_SimpleRequest_CallWithResponseOnExtension()
        {
            var application = Make_PortalApplication();
            var extension   = new Mock<IExtension>();
            var module      = new Mock<IModule>();
            var parameters  = new Dictionary<string, string> { { "format", "XML" } };
            var request     = new PortalRequest("test", "test", parameters);
            module.Setup(m => m.GetExtensionNames()).Returns(new[] { "test" });
            module.Setup(m => m.GetExtension("test")).Returns(extension.Object);
            application.AddModule(module.Object);

            application.ProcessRequest(request);

            extension.Verify(m => m.WithPortalResponse(It.IsAny<IPortalResponse>()),Times.Exactly(1));
        }

        [Test]
        public void ProcessRequest_SimpleRequest_CallWithRequestOnExtension()
        {
            var application = Make_PortalApplication();
            var extension = new Mock<IExtension>();
            var module = new Mock<IModule>();
            var parameters = new Dictionary<string, string> { { "format", "XML" } };
            var request = new PortalRequest("test", "test", parameters);
            module.Setup(m => m.GetExtensionNames()).Returns(new[] { "test" });
            module.Setup(m => m.GetExtension("test")).Returns(extension.Object);
            application.AddModule(module.Object);

            application.ProcessRequest(request);

            extension.Verify(m => m.WithPortalRequest(It.IsAny<IPortalRequest>()), Times.Exactly(1));
        }
    }
}
