using System.Linq;
using System.Xml.Linq;
using Geckon.Portal.Core;
using Geckon.Portal.Core.Exception;
using Geckon.Portal.Core.Standard.Extension;
using Geckon.Portal.Test;
using NUnit.Framework;

namespace Geckon.Portal.Extensions.Standard.Test
{
    [TestFixture]
    public class SubscriptionTest : TestBase
    {
        [Test]
        public void Should_Get_Subscription()
        {
            SubscriptionExtension extension = new SubscriptionExtension( );

            extension.Init( new PortalContextMock() );
            extension.Get( AdminCallContext, SubscriptionInfo.GUID.ToString() );

            Assert.AreEqual( SubscriptionInfo.GUID.ToString(), XDocument.Parse(extension.Result).Descendants( "GUID" ).First().Value );
        }

        [Test, ExpectedException( typeof(InsufficientPermissionsExcention) )]
        public void Should_Throw_InssurficientPermissionsException_On_Get_Subscription()
        {
            SubscriptionExtension extension = new SubscriptionExtension(  );

            extension.Init( new PortalContextMock() );
            extension.Get( AnonCallContext, SubscriptionInfo.GUID.ToString() );
        }

        [Test]
        public void Should_Create_Subscription()
        {
            SubscriptionExtension extension = new SubscriptionExtension( );

            extension.Init( new PortalContextMock() );
            extension.Create( AdminCallContext, "some name" );

            Assert.AreEqual( "some name", XDocument.Parse(extension.Result).Descendants( "Name" ).First().Value );
        }

        [Test, ExpectedException( typeof(InsufficientPermissionsExcention) )]
        public void Should_Throw_InssurficientPermissionsException_On_Create_Subscription()
        {
            SubscriptionExtension extension = new SubscriptionExtension(  );

            extension.Init( new PortalContextMock() );
            extension.Create( AnonCallContext, "some name" );
        }

        [Test]
        public void Should_Delete_Subscription()
        {
            SubscriptionExtension extension = new SubscriptionExtension(  );

            extension.Init( new PortalContextMock() );
            extension.Delete( AdminCallContext, SubscriptionInfo.GUID.ToString() );

            Assert.AreEqual( "1", XDocument.Parse(extension.Result).Descendants( "Value" ).First().Value );
        }

        [Test, ExpectedException( typeof(InsufficientPermissionsExcention) )]
        public void Should_Throw_InsufficientPermissionsExcention_When_Trying_To_Delete_Subscription()
        {
            SubscriptionExtension extension = new SubscriptionExtension(  );

            extension.Init( new PortalContextMock() );
            extension.Delete( AnonCallContext, SubscriptionInfo.GUID.ToString() );
        }

        [Test]
        public void Should_Update_Subscription()
        {
            SubscriptionExtension extension = new SubscriptionExtension( );

            extension.Init( new PortalContextMock() );
            extension.Update( AdminCallContext, SubscriptionInfo.GUID.ToString(), "new subscription name" );

            Assert.AreEqual( "1", XDocument.Parse(extension.Result).Descendants( "Value" ).First().Value );
        }

        [Test, ExpectedException( typeof(InsufficientPermissionsExcention) )]
        public void Should_Throw_InsufficientPermissionsExcention_When_Trying_To_Update_Subscription()
        {
            SubscriptionExtension extension = new SubscriptionExtension(  );

            extension.Init( new PortalContextMock() );
            extension.Update( AnonCallContext, SubscriptionInfo.GUID.ToString(), "new subscription name" );
        }
    }
}
