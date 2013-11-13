namespace Chaos.Portal.Protocol.Tests.v6.Response
{
    using System.Collections.Generic;
    using System.IO;

    using CHAOS.Serialization;

    using Core;
    using Core.Data.Model;
    using Core.Indexing.View;
    using Core.Request;
    using Core.Response;

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
                Assert.That(stream.ReadToEnd(), Is.EqualTo("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?><PortalResponse><Header><Duration>0</Duration></Header><Body><Groups><ResultGroup><Count>2</Count><TotalCount>2</TotalCount><Results><ResultMock><Fullname>test</Fullname></ResultMock><ResultMock><Fullname>test</Fullname></ResultMock></Results></ResultGroup></Groups></Body><Error /></PortalResponse>"));
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
                var readToEnd = stream.ReadToEnd();
                Assert.That(readToEnd, Is.EqualTo("<?xml version=\"1.0\" encoding=\"utf-8\" standalone=\"yes\"?><PortalResponse><Header><Duration>0</Duration></Header><Body><Groups><ResultGroup><Count>2</Count><TotalCount>2</TotalCount><Results><ViewDataResultMock><Fullname>test</Fullname></ViewDataResultMock><ViewDataResultMock><Fullname>test</Fullname></ViewDataResultMock></Results></ResultGroup></Groups></Body><Error /></PortalResponse>"));
            }
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
                var attachment = new Attachment
                {
                    Stream = memoryStream
                };

                writer.Write("OK!");
                writer.Flush();
                response.WriteToOutput(attachment);

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