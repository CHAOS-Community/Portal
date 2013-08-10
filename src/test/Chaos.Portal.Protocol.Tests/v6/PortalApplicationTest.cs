namespace Chaos.Portal.Protocol.Tests.v6
{
    using System.Collections.Generic;
    using System.IO;

    using Chaos.Portal.Core;
    using Chaos.Portal.Core.Data;
    using Chaos.Portal.Core.Exceptions;
    using Chaos.Portal.Core.Extension;
    using Chaos.Portal.Core.Module;
    using Chaos.Portal.Core.Request;
    using Chaos.Portal.Core.Response;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class PortalApplicationTest : TestBase
    {
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
            var extension   = new ExtensionMock(application);
            var module      = new Mock<IModule>();
            module.Setup(m => m.GetExtensionNames(Protocol.V6)).Returns(new[] { "ExtensionMock" });
            module.Setup(m => m.GetExtension<ExtensionMock>(Protocol.V6)).Returns(extension);
            application.AddModule(module.Object);

            var result = application.GetExtension<ExtensionMock>(Protocol.V6);

            Assert.That(result, Is.Not.Null);
            Assert.IsInstanceOf<ExtensionMock>(result);
        }

        [Test]
        [ExpectedException(typeof(ExtensionMissingException))]
        public void GetExtension_ByNotLoadedType_ThrowExtensionMissingException()
        {
            var portalApplication = Make_PortalApplication();

            portalApplication.GetExtension<ExtensionMock>(Protocol.V6);
        }

        [Test]
        public void AddModuel_GivenPortalModule_AddTheModuleToLoadedModules()
        {
            var module      = new Mock<IModule>();
            var extension   = new Mock<IExtension>();
            var application = Make_PortalApplication();
            module.Setup(m => m.GetExtensionNames(Protocol.V6)).Returns(new[] { "test" });
            module.Setup(m => m.GetExtension(Protocol.V6, "test")).Returns(extension.Object);

            application.AddModule(module.Object);

            Assert.That(application.LoadedModules.ContainsKey("test"), Is.True);
            Assert.That(application.LoadedModules["test"], Is.EqualTo(module.Object));
        }

        [Test]
        public void ProcessRequest_RequestWithReturnFormatXml_ReturnResponseHasReturnFormatXml()
        {
            var application = Make_PortalApplication();
            var extension   = new ExtensionMock(application);
            var module      = new Mock<IModule>();
            var request     = Make_TestRequest();
            module.Setup(m => m.GetExtensionNames(Protocol.V6)).Returns(new[] { "test" });
            module.Setup(m => m.GetExtension(Protocol.V6, "test")).Returns(extension);
            application.AddModule(module.Object);

            var response = application.ProcessRequest(request);

            Assert.That(response.ReturnFormat, Is.EqualTo(ReturnFormat.XML));
        }

        [Test]
        public void ProcessRequest_RequestWithReturnFormatXml_GetResponseStream()
        {
            var application = Make_PortalApplication();
            var extension   = new ExtensionMock(application);
            var module      = new Mock<IModule>();
            var request     = Make_TestRequest();
            request.Parameters["format"] = "XML2";
            module.Setup(m => m.GetExtensionNames(Protocol.V6)).Returns(new[] { "test" });
            module.Setup(m => m.GetExtension(Protocol.V6, "test")).Returns(extension);
            application.AddModule(module.Object);
            request.Stopwatch.Reset();
            
            var response = application.ProcessRequest(request);

            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                Assert.That(stream.ReadToEnd(), Is.EqualTo("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?><PortalResponse><Header><Duration>0</Duration></Header><Result><Count>1</Count><TotalCount>1</TotalCount><Results><Result FullName=\"Chaos.Portal.Core.Data.Model.ScalarResult\"><Value>1</Value></Result></Results></Result><Error /></PortalResponse>"));
            }
        }

        [Test, ExpectedException(typeof(ActionMissingException))]
        public void ProcessRequest_ActionDoesntExist_ThrowActionMissingException()
        {
            var application = Make_PortalApplication();
            var extension   = new ExtensionMock(application);
            var module      = new Mock<IModule>();
            var parameters  = new Dictionary<string, string> { { "format", "XML" } };
            var request     = new PortalRequest((Protocol)Protocol.V6, (string)"test", (string)"missing", parameters, null);
            module.Setup(m => m.GetExtensionNames(Protocol.V6)).Returns(new[] { "test" });
            module.Setup(m => m.GetExtension(Protocol.V6, "test")).Returns(extension);
            application.AddModule(module.Object);
            request.Stopwatch.Reset();

            application.ProcessRequest(request);
        }

        [Test]
        public void ProcessRequest_ExtensionThrowsAnException_ReturnResponseWithError()
        {
            var application = Make_PortalApplication();
            var extension   = new ExtensionMock(application);
            var module      = new Mock<IModule>();
            var parameters  = new Dictionary<string, string> { { "format", "XML2" } };
            var request     = new PortalRequest((Protocol)Protocol.V6, (string)"test", (string)"error", parameters, null);
            module.Setup(m => m.GetExtensionNames(Protocol.V6)).Returns(new[] { "test" });
            module.Setup(m => m.GetExtension(Protocol.V6, "test")).Returns(extension);
            application.AddModule(module.Object);
            request.Stopwatch.Reset();

            var response = application.ProcessRequest(request);

            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                var readToEnd = stream.ReadToEnd();
                Assert.That(readToEnd, Is.EqualTo("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?><PortalResponse><Header><Duration>0</Duration></Header><Result><Count>0</Count><TotalCount>0</TotalCount><Results /></Result><Error Fullname=\"System.ArgumentException\"><Message>Value does not fall within the expected range.</Message></Error></PortalResponse>"));
            }
        }

        [Test]
        public void ProcessRequest_SimpleRequest_CallWithResponseOnExtension()
        {
            var application = Make_PortalApplication();
            var extension   = new Mock<IExtension>();
            var module      = new Mock<IModule>();
            var request     = Make_TestRequest();
            module.Setup(m => m.GetExtensionNames(Protocol.V6)).Returns(new[] { "test" });
            module.Setup(m => m.GetExtension(Protocol.V6, "test")).Returns(extension.Object);
            application.AddModule(module.Object);

            application.ProcessRequest(request);

            extension.Verify(m => m.WithPortalResponse(It.IsAny<IPortalResponse>()),Times.Exactly(1));
        }

        [Test]
        public void ProcessRequest_SimpleRequest_CallWithRequestOnExtension()
        {
            var application = Make_PortalApplication();
            var extension   = new Mock<IExtension>();
            var module      = new Mock<IModule>();
            var request     = Make_TestRequest();
            module.Setup(m => m.GetExtensionNames(Protocol.V6)).Returns(new[] { "test" });
            module.Setup(m => m.GetExtension(Protocol.V6, "test")).Returns(extension.Object);
            application.AddModule(module.Object);

            application.ProcessRequest(request);

            extension.Verify(m => m.WithPortalRequest(It.IsAny<IPortalRequest>()), Times.Exactly(1));
        }
    }
}
