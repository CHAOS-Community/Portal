using System.Collections.Generic;
using System.Linq;
using CHAOS.Portal.Core.Request;
using NUnit.Framework;

namespace CHAOS.Portal.Core.Test
{
    [TestFixture]
    public class PortalRequestTest : TestBase
    {
        [Test]
        public void Should_Create_Portal_Request()
        {
            IDictionary<string,string> parameters = new Dictionary<string,string>();
            parameters.Add( "someString", "hello world" );
            PortalRequest request = new PortalRequest( "Extension", "Action", parameters );

            Assert.AreEqual( "Extension", request.Extension );
            Assert.AreEqual( "Action", request.Action );
            Assert.AreEqual( "someString", request.Parameters.Keys.First() );
            Assert.AreEqual( "hello world", request.Parameters.Values.First() );
        }
    }
}
