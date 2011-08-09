using System.Linq;
using System.Xml.Linq;
using Geckon.Portal.Core.Exception;
using Geckon.Portal.Core.Standard.Extension;
using NUnit.Framework;

namespace Geckon.Portal.Extensions.Standard.Test
{
    [TestFixture]
    public class SubscriptionTest : TestBase
    {
        [Test]
        public void Should_Get_Subscription()
        {
            SubscriptionExtension extension = new SubscriptionExtension( new PortalContextMock() );
            extension.Init( new Result() );

            XDocument xdoc = XDocument.Parse( extension.Get( AdminUser.SessionID.ToString(), Subscription.GUID.ToString() ).Content );

            Assert.AreEqual( Subscription.GUID.ToString(), xdoc.Descendants( "GUID" ).First().Value );
        }

        [Test, ExpectedException( typeof(InsufficientPermissionsExcention) )]
        public void Should_Throw_InssurficientPermissionsException_On_Get_Subscription()
        {
            SubscriptionExtension extension = new SubscriptionExtension( new PortalContextMock() );
            extension.Init( new Result() );

            extension.Get( User.SessionID.ToString(), Subscription.GUID.ToString() );
        }
    }
}
