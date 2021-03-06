namespace Chaos.Portal.Protocol.Tests.v5.Response
{
    using System;
    using System.IO;
    using Core.Request;
    using Core.Response;
    using NUnit.Framework;

    using Core.Data.Model;
    using Core.Response.Dto.v1;

    [TestFixture]
    public class PortalResponseTest
    {
        [Test]
        public void GetResponseStream_ReturnedObjectWrappedWithModuleName_SetTheModuleNameInTheResponseXml()
        {
            var request = new PortalRequest();
            var response = new PortalResponse(request);
            var wrapped = new NamedResult( "Some module name", 1234);
            request.Stopwatch.Reset();

            response.WriteToOutput(wrapped);

            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                Assert.That(stream.ReadToEnd(), Is.EqualTo("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?><PortalResult Duration=\"0\"><ModuleResults><ModuleResult Fullname=\"Some module name\" Duration=\"0\" Count=\"1\"><Results><Result FullName=\"Chaos.Portal.Core.Data.Model.ScalarResult\"><Value>1234</Value></Result></Results></ModuleResult></ModuleResults></PortalResult>"));
            }
        }

        [Test]
        public void GetResponseStream_GivenSimpleIntegerResult_ReturnsXmlContainingScalarResult()
        {
            var request  = new PortalRequest();
            var response = new PortalResponse(request);
            request.Stopwatch.Reset();

            response.WriteToOutput(1234);

            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                Assert.That(stream.ReadToEnd(), Is.EqualTo("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?><PortalResult Duration=\"0\"><ModuleResults><ModuleResult Fullname=\"Portal\" Duration=\"0\" Count=\"1\"><Results><Result FullName=\"Chaos.Portal.Core.Data.Model.ScalarResult\"><Value>1234</Value></Result></Results></ModuleResult></ModuleResults></PortalResult>"));
            }
        }

        [Test]
        public void GetResponseStream_GivenSimpleUnsigndIntegerResult_ReturnsXmlContainingScalarResult()
        {
            var request = new PortalRequest();
            var response = new PortalResponse(request);
            request.Stopwatch.Reset();

            response.WriteToOutput(1234u);

            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                Assert.That(stream.ReadToEnd(), Is.EqualTo("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?><PortalResult Duration=\"0\"><ModuleResults><ModuleResult Fullname=\"Portal\" Duration=\"0\" Count=\"1\"><Results><Result FullName=\"Chaos.Portal.Core.Data.Model.ScalarResult\"><Value>1234</Value></Result></Results></ModuleResult></ModuleResults></PortalResult>"));
            }
        }

        [Test]
        public void GetResponseStream_GivenAComplexDto_ReturnsAsXml()
        {
            var request = new PortalRequest();
            var response = new PortalResponse(request);
            request.Stopwatch.Reset();

            response.WriteToOutput(new Core.Data.Model.Module{ ID = 4321});

            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                Assert.That(stream.ReadToEnd(), Is.EqualTo("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?><PortalResult Duration=\"0\"><ModuleResults><ModuleResult Fullname=\"Portal\" Duration=\"0\" Count=\"1\"><Results><Result FullName=\"Chaos.Portal.Core.Data.Model.Module\"><ID>4321</ID><DateCreated>01-01-0001 00:00:00</DateCreated></Result></Results></ModuleResult></ModuleResults></PortalResult>"));
            }
        }

        [Test]
        public void GetResponseStream_GivenAListOfComplexDtos_ReturnsAsXml()
        {
            var request = new PortalRequest();
            var response = new PortalResponse(request);
            var modules = new[] { new Core.Data.Model.Module { ID = 4321 }, new Core.Data.Model.Module { ID = 1234 }};
            request.Stopwatch.Reset();
            
            response.WriteToOutput(modules);

            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                Assert.That(stream.ReadToEnd(), Is.EqualTo("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?><PortalResult Duration=\"0\"><ModuleResults><ModuleResult Fullname=\"Portal\" Duration=\"0\" Count=\"2\"><Results><Result FullName=\"Chaos.Portal.Core.Data.Model.Module\"><ID>4321</ID><DateCreated>01-01-0001 00:00:00</DateCreated></Result><Result FullName=\"Chaos.Portal.Core.Data.Model.Module\"><ID>1234</ID><DateCreated>01-01-0001 00:00:00</DateCreated></Result></Results></ModuleResult></ModuleResults></PortalResult>"));
            }
        }

        [Test]
        public void GetResponseStream_GivenAPagedListOfComplexDtos_ReturnsAsXml()
        {
            var request = new PortalRequest();
            var response = new PortalResponse(request);
            var modules = new Core.Data.Model.PagedResult<Core.Data.Model.Module>(2, 2, new[] { new Core.Data.Model.Module { ID = 4321 }, new Core.Data.Model.Module { ID = 1234 } });
            request.Stopwatch.Reset();

            response.WriteToOutput(modules);

            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                Assert.That(stream.ReadToEnd(), Is.EqualTo("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?><PortalResult Duration=\"0\"><ModuleResults><ModuleResult Fullname=\"Portal\" Duration=\"0\" Count=\"2\" TotalCount=\"2\"><Results><Result FullName=\"Chaos.Portal.Core.Data.Model.Module\"><ID>4321</ID><DateCreated>01-01-0001 00:00:00</DateCreated></Result><Result FullName=\"Chaos.Portal.Core.Data.Model.Module\"><ID>1234</ID><DateCreated>01-01-0001 00:00:00</DateCreated></Result></Results></ModuleResult></ModuleResults></PortalResult>"));
            }
        }

        [Test]
        public void GetResponseStream_GivenAnException_ReturnsAsErrorXml()
        {
            var request = new PortalRequest();
            var response = new PortalResponse(request);
            request.Stopwatch.Reset();

            response.WriteToOutput(new ArgumentException());

            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                var readToEnd = stream.ReadToEnd();
                Assert.That(readToEnd, Is.EqualTo("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?><PortalResult Duration=\"0\"><ModuleResults><ModuleResult Fullname=\"Portal\" Duration=\"0\" Count=\"1\"><Results><Error FullName=\"System.ArgumentException\"><Message>Value does not fall within the expected range.</Message></Error></Results></ModuleResult></ModuleResults></PortalResult>"));
            }
        }
    }
}