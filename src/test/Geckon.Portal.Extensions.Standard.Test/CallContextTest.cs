using System.Linq;
using Geckon.Portal.Core.Standard.Extension;
using Geckon.Portal.Test;
using NUnit.Framework;

namespace Geckon.Portal.Extensions.Standard.Test
{
    [TestFixture]
    public class CallContextTest : TestBase
    {
        [Test]
        public void Should_Get_Groups()
        {
            Assert.Greater( AdminCallContext.Groups.Count(),0 );
        }

        [Test]
        public void Should_Get_Subscriptions()
        {
            Assert.Greater( AdminCallContext.Subscriptions.Count(), 0);
        }
    }
}
