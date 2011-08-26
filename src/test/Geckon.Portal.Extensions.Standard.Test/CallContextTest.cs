using System.Linq;
using Geckon.Portal.Core.Standard.Extension;
using NUnit.Framework;

namespace Geckon.Portal.Extensions.Standard.Test
{
    [TestFixture]
    public class CallContextTest : TestBase
    {
        [Test]
        public void Should_Get_Groups()
        {
            CallContext callContext = new CallContext( new MockCache(), new MockSolr(), AdminSession.SessionID.ToString() );

            Assert.Greater(callContext.Groups.Count(),0);
        }
    }
}
