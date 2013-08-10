namespace Chaos.Portal.Protocol.Tests.v5.Response
{
    using System;
    using System.IO;

    using Chaos.Portal.Core.Data.Model;
    using Chaos.Portal.Core.Exceptions;
    using Chaos.Portal.Core.Request;
    using Chaos.Portal.Core.Response.Dto;

    using NUnit.Framework;

    [TestFixture]
    public class PortalResponseTest
    {
        [Test]
        public void GetResponseStream_GivenSimpleIntegerResult_ReturnsXmlContainingScalarResult()
        {
            var request  = new PortalRequest();
            var response = new Portal.v5.Response.PortalResponse(request);
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
            var response = new Portal.v5.Response.PortalResponse(request);
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
            var response = new Portal.v5.Response.PortalResponse(request);
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
            var response = new Portal.v5.Response.PortalResponse(request);
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
            var response = new Portal.v5.Response.PortalResponse(request);
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
            var response = new Portal.v5.Response.PortalResponse(request);
            request.Stopwatch.Reset();

            response.WriteToOutput(new ArgumentException());

            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                var readToEnd = stream.ReadToEnd();
                Assert.That(readToEnd, Is.EqualTo("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?><PortalResult Duration=\"0\"><ModuleResults><ModuleResult Fullname=\"Portal\" Duration=\"0\" Count=\"1\"><Results><Error FullName=\"System.ArgumentException\"><Message>Value does not fall within the expected range.</Message></Error></Results></ModuleResult></ModuleResults></PortalResult>"));
            }
        }

        [Test, ExpectedException(typeof(UnsupportedExtensionReturnTypeException))]
        public void GetResponseStream_GivenAnUnsupportedResult_ReturnsAsErrorXml()
        {
            var request = new PortalRequest();
            var response = new Portal.v5.Response.PortalResponse(request);
            request.Stopwatch.Reset();

            response.WriteToOutput(new object());
        }

        [Test]
        public void GetResponseStream_GivenAStream_ReturnsAStream()
        {
            var request = new PortalRequest();
            request.Parameters.Add("format", "attachment");

            using (var response = new Portal.v5.Response.PortalResponse(request))
            {
                request.Stopwatch.Reset();

                var memoryStream = new MemoryStream();
                var writer = new StreamWriter(memoryStream);

                writer.Write("OK!");
                writer.Flush();
                response.WriteToOutput(memoryStream);

                var stream = new StreamReader(response.GetResponseStream());
                
                Assert.That(stream.ReadToEnd(), Is.EqualTo("OK!"));
            }
        }
    }
}