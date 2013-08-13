namespace Chaos.Portal.Protocol.Tests.v6.Response
{
    using System.Collections.Generic;
    using System.IO;

    using CHAOS.Serialization;

    using Chaos.Portal.Core;
    using Chaos.Portal.Core.Data.Model;
    using Chaos.Portal.Core.Exceptions;
    using Chaos.Portal.Core.Indexing.View;
    using Chaos.Portal.Core.Request;
    using Chaos.Portal.Core.Response;

    using NUnit.Framework;

    [TestFixture]
    public class PortalResponseTest
    {
        [Test]
        public void GetResponseStream_GivenGroupedResult_ReturnsXmlContainingGroupedResult()
        {
            var request  = new PortalRequest();
            var response = new PortalResponse(request){ReturnFormat = ReturnFormat.XML2};
            var grouped  = new GroupedResult<ResultMock>(new[] { new ResultGroup<ResultMock>(2, 0, new[] { new ResultMock(), new ResultMock() }) });
            request.Stopwatch.Reset();

            response.WriteToOutput(grouped);

            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                Assert.That(stream.ReadToEnd(), Is.EqualTo("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?><PortalResponse><Header><Duration>0</Duration></Header><Result><Groups><ResultGroup><Count>2</Count><TotalCount>2</TotalCount><Results><ResultMock><Fullname>test</Fullname></ResultMock><ResultMock><Fullname>test</Fullname></ResultMock></Results></ResultGroup></Groups></Result><Error /></PortalResponse>"));
            }
        }

        [Test]
        public void GetResponseStream_GivenGroupedResultWithIViewData_ReturnsXmlContainingGroupedResult()
        {
            var request  = new PortalRequest();
            var response = new PortalResponse(request){ReturnFormat = ReturnFormat.XML2};
            var grouped  = new GroupedResult<IResult>(new[] { new ResultGroup<ViewDataResultMock>(2, 0, new[] { new ViewDataResultMock(), new ViewDataResultMock() }) });
            request.Stopwatch.Reset();

            response.WriteToOutput(grouped);

            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                Assert.That(stream.ReadToEnd(), Is.EqualTo("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?><PortalResponse><Header><Duration>0</Duration></Header><Result><Groups><ResultGroup><Count>2</Count><TotalCount>2</TotalCount><Results><ViewDataResultMock><Fullname>test</Fullname></ViewDataResultMock><ViewDataResultMock><Fullname>test</Fullname></ViewDataResultMock></Results></ResultGroup></Groups></Result><Error /></PortalResponse>"));
            }
        }

        [Test, ExpectedException(typeof(UnsupportedExtensionReturnTypeException))]
        public void GetResponseStream_GivenAnUnsupportedResult_ReturnsAsErrorXml()
        {
            var request = new PortalRequest();
            var response = new PortalResponse(request);
            request.Stopwatch.Reset();

            response.WriteToOutput(new object());
        }

        [Test]
        public void GetResponseStream_GivenAStream_ReturnsAStream()
        {
            var request = new PortalRequest();
            request.Parameters.Add("format", "attachment");

            using (var response = new PortalResponse(request))
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
    public class ViewDataResultMock : IViewData
    {
        #region Implementation of IResult

        [Serialize]
        public string Fullname { get { return "test"; } }

        #endregion

        #region Implementation of IIndexable

        public KeyValuePair<string, string> UniqueIdentifier { get; private set; }

        public IEnumerable<KeyValuePair<string, string>> GetIndexableFields()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }

    public class ResultMock : IResult
    {
        #region Implementation of IResult

        [Serialize]
        public string Fullname { get { return "test"; } }

        #endregion
    }
}