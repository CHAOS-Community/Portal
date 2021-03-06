﻿namespace Chaos.Portal.Protocol.Tests.v5
{
    using System.Collections.Generic;
    using System.IO;

    using Core;
    using Core.Module;
    using Core.Request;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class PortalApplicationTest : TestBase
    {
        [Test]
        public void ProcessRequest_ExtensionThrowsAnException_ReturnResponseWithError()
        {
            var application = Make_PortalApplication();
            var module      = new Mock<IModule>();
            var parameters  = new Dictionary<string, string> { { "½ormat", "XML" } };
            var request     = new PortalRequest((Protocol)Protocol.V5, (string)"test", (string)"error", parameters, null);
            var extension   = new ExtensionMock(application);
            module.Setup(m => m.GetExtensionNames(Protocol.V5)).Returns(new[] { "test" });
            module.Setup(m => m.GetExtension(Protocol.V5, "test")).Returns(extension);
            application.AddModule(module.Object);
            request.Stopwatch.Reset();

            var response = application.ProcessRequest(request);

            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                var readToEnd = stream.ReadToEnd();
                Assert.That(readToEnd, Is.EqualTo("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?><PortalResult Duration=\"0\"><ModuleResults><ModuleResult Fullname=\"Portal\" Duration=\"0\" Count=\"1\"><Results><Error FullName=\"System.ArgumentException\"><Message>Value does not fall within the expected range.</Message></Error></Results></ModuleResult></ModuleResults></PortalResult>"));
            }
        }
    }
}