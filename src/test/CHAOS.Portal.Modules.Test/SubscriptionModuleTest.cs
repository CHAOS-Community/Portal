using System.Configuration;
using CHAOS.Portal.Core.Test;
using CHAOS.Portal.Exception;
using NUnit.Framework;

namespace CHAOS.Portal.Modules.Test
{
    [TestFixture]
    public class SubscriptionModuleTest : TestBase
    {
        #region Constructors

        private SubscriptionModule SubscriptionModule { get; set; }

        [SetUp]
        public void SetUp()
        {
            base.SetUp();

            SubscriptionModule = new SubscriptionModule();
            SubscriptionModule.Initialize( ConfigurationManager.ConnectionStrings["PortalEntities"].ConnectionString );
        }

        #endregion

        [Test]
        public void Should_Get_Subscription()
        {
            var subscription = SubscriptionModule.Get( AdminCallContext, SubscriptionInfo.GUID );

            Assert.AreEqual( SubscriptionInfo.GUID.ToByteArray(), subscription.GUID.ToByteArray() );
        }

        [Test, ExpectedException( typeof( InsufficientPermissionsException ) )]
        public void Should_Throw_InssurficientPermissionsException_On_Get_Subscription()
        {
            SubscriptionModule.Get( AnonCallContext, SubscriptionInfo.GUID );
        }

        [Test]
        public void Should_Create_Subscription()
        {
            var subscription = SubscriptionModule.Create( AdminCallContext, "some name" );

            Assert.AreEqual( "some name", subscription.Name );
        }

        [Test, ExpectedException( typeof( InsufficientPermissionsException ) )]
        public void Should_Throw_InssurficientPermissionsException_On_Create_Subscription()
        {
            SubscriptionModule.Create( AnonCallContext, "some name" );
        }

        [Test]
        public void Should_Delete_Subscription()
        {
            var result = SubscriptionModule.Delete( AdminCallContext, SubscriptionInfo.GUID );

            Assert.AreEqual( 1, result.Value );
        }

        [Test, ExpectedException( typeof(InsufficientPermissionsException) )]
        public void Should_Throw_InsufficientPermissionsExcention_When_Trying_To_Delete_Subscription()
        {
            SubscriptionModule.Delete( AnonCallContext, SubscriptionInfo.GUID );
        }

        [Test]
        public void Should_Update_Subscription()
        {
            var result = SubscriptionModule.Update( AdminCallContext, SubscriptionInfo.GUID, "new subscription name" );

            Assert.AreEqual(1, result.Value );
        }

        [Test, ExpectedException( typeof(InsufficientPermissionsException) )]
        public void Should_Throw_InsufficientPermissionsExcention_When_Trying_To_Update_Subscription()
        {
            SubscriptionModule.Update( AnonCallContext, SubscriptionInfo.GUID, "new subscription name" );
        }
    }
}
