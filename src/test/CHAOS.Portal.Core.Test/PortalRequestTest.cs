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
            PortalRequest request = new PortalRequest( "Extension", "Action", new Parameter( "someString", "hello world" ) );
        }
    }
}
