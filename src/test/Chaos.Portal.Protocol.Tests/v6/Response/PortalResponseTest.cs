namespace Chaos.Portal.Protocol.Tests.v6.Response
{
    using System.IO;

    using CHAOS.Serialization;

    using Chaos.Portal.Core.Data.Model;
    using Chaos.Portal.Core.Exceptions;
    using Chaos.Portal.Core.Request;

    using NUnit.Framework;

    [TestFixture]
    public class PortalResponseTest
    {
        [Test]
        public void GetResponseStream_GivenSimpleIntegerResult_ReturnsXmlContainingScalarResult()
        {
            var request  = new PortalRequest();
            var response = new Portal.v6.Response.PortalResponse(request);
            var grouped = new Portal.Core.Data.Model.GroupedResult<ResultMock>(new[] { new ResultGroup<ResultMock>(2, 0, new[] { new ResultMock(), new ResultMock() }) });
            request.Stopwatch.Reset();

            response.WriteToOutput(grouped);

            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                Assert.That(stream.ReadToEnd(), Is.EqualTo("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?><PortalResponse><Header><Duration>0</Duration></Header><Result><Groups><ResultGroup><Count>2</Count><TotalCount>2</TotalCount><Results><ResultMock><Fullname>test</Fullname></ResultMock><ResultMock><Fullname>test</Fullname></ResultMock></Results></ResultGroup></Groups></Result><Error /></PortalResponse>"));
            }
        }

        [Test, ExpectedException(typeof(UnsupportedExtensionReturnTypeException))]
        public void GetResponseStream_GivenAnUnsupportedResult_ReturnsAsErrorXml()
        {
            var request = new PortalRequest();
            var response = new Portal.v6.Response.PortalResponse(request);
            request.Stopwatch.Reset();

            response.WriteToOutput(new object());
        }

        [Test]
        public void GetResponseStream_GivenAStream_ReturnsAStream()
        {
            var request = new PortalRequest();
            request.Parameters.Add("format", "attachment");

            using (var response = new Portal.v6.Response.PortalResponse(request))
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

    public class ResultMock : IResult
    {
        #region Implementation of IResult

        [Serialize]
        public string Fullname { get { return "test"; } }

        #endregion
    }
}