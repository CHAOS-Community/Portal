using System.Linq;
using System.Xml.Linq;
using Geckon.Portal.Core;
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
            SubscriptionExtension extension = new SubscriptionExtension( );
            extension.Init( new PortalContextMock(), AdminSession.SessionID.ToString() );
            extension.CallContext.Parameters = new Parameter[0];

            extension.Get( AdminUser.SessionID.ToString(), SubscriptionInfo.GUID.ToString() );

            Assert.AreEqual( SubscriptionInfo.GUID.ToString(), XDocument.Parse(extension.Result).Descendants( "GUID" ).First().Value );
        }

        [Test, ExpectedException( typeof(InsufficientPermissionsExcention) )]
        public void Should_Throw_InssurficientPermissionsException_On_Get_Subscription()
        {
            SubscriptionExtension extension = new SubscriptionExtension(  );
            extension.Init( new PortalContextMock(), Session.SessionID.ToString() );

            extension.Get( User.SessionID.ToString(), SubscriptionInfo.GUID.ToString() );
        }

        [Test]
        public void Should_Create_Subscription()
        {
            SubscriptionExtension extension = new SubscriptionExtension( );
            extension.Init( new PortalContextMock(), AdminSession.SessionID.ToString() );
            extension.CallContext.Parameters = new Parameter[0];

            extension.Create( AdminUser.SessionID.ToString(), "some name" );

            Assert.AreEqual( "some name", XDocument.Parse(extension.Result).Descendants( "Name" ).First().Value );
        }

        [Test, ExpectedException( typeof(InsufficientPermissionsExcention) )]
        public void Should_Throw_InssurficientPermissionsException_On_Create_Subscription()
        {
            SubscriptionExtension extension = new SubscriptionExtension(  );
            extension.Init( new PortalContextMock(), Session.SessionID.ToString() );
            extension.CallContext.Parameters = new Parameter[0];

            extension.Create( User.SessionID.ToString(), "some name" );
        }

        [Test]
        public void Should_Delete_Subscription()
        {
            SubscriptionExtension extension = new SubscriptionExtension(  );
            extension.Init( new PortalContextMock(), AdminSession.SessionID.ToString() );
            extension.CallContext.Parameters = new Parameter[0];

            extension.Delete( AdminUser.SessionID.ToString(), SubscriptionInfo.GUID.ToString() );

            Assert.AreEqual( "1", XDocument.Parse(extension.Result).Descendants( "Value" ).First().Value );
        }

        [Test, ExpectedException( typeof(InsufficientPermissionsExcention) )]
        public void Should_Throw_InsufficientPermissionsExcention_When_Trying_To_Delete_Subscription()
        {
            SubscriptionExtension extension = new SubscriptionExtension(  );
            extension.Init( new PortalContextMock(), Session.SessionID.ToString() );
            extension.CallContext.Parameters = new Parameter[0];

            extension.Delete( User.SessionID.ToString(), SubscriptionInfo.GUID.ToString() );
        }

        [Test]
        public void Should_Update_Subscription()
        {
            SubscriptionExtension extension = new SubscriptionExtension( );
            extension.Init( new PortalContextMock(), AdminSession.SessionID.ToString() );
            extension.CallContext.Parameters = new Parameter[0];

            extension.Update( AdminUser.SessionID.ToString(), SubscriptionInfo.GUID.ToString(), "new subscription name" );

            Assert.AreEqual( "1", XDocument.Parse(extension.Result).Descendants( "Value" ).First().Value );
        }

        [Test, ExpectedException( typeof(InsufficientPermissionsExcention) )]
        public void Should_Throw_InsufficientPermissionsExcention_When_Trying_To_Update_Subscription()
        {
            SubscriptionExtension extension = new SubscriptionExtension(  );
            extension.Init( new PortalContextMock(), Session.SessionID.ToString() );
            extension.CallContext.Parameters = new Parameter[0];

            extension.Update( User.SessionID.ToString(), SubscriptionInfo.GUID.ToString(), "new subscription name" );
        }
    }
}
