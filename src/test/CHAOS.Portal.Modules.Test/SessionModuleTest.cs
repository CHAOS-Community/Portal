using System.Configuration;
using CHAOS.Portal.Core.Test;
using NUnit.Framework;

namespace CHAOS.Portal.Modules.Test
{
    [TestFixture]
    public class SessionModuleTest : TestBase
    {
        #region Properties

        private SessionPortalModule SessionPortalModule { get; set; }

        #endregion

        [SetUp]
        public void SetUp()
        {
            SessionPortalModule = new SessionPortalModule();
            SessionPortalModule.Initialize( ConfigurationManager.ConnectionStrings["PortalEntities"].ConnectionString );
        }

        [Test]
        public void Should_Create_Session()
        {
            var session = SessionPortalModule.Create( AnonCallContext, 4 );

            Assert.AreEqual( AnonCallContext.User.GUID.ToByteArray(), session.UserGUID.ToByteArray() );
        }

        [Test]
        public void Should_Get_A_Session()
        {
            var session = SessionPortalModule.Get( AnonCallContext );

            Assert.AreEqual( AnonCallContext.User.GUID.ToByteArray(), session.UserGUID.ToByteArray() );
        }

        [Test]
        public void Should_Update_A_Session()
        {
            var session = SessionPortalModule.Update( AdminCallContext );

            Assert.IsNotNull( session.DateModified );
        }

        [Test]
        public void Should_Delete_A_Session()
        {
            var result = SessionPortalModule.Delete( AdminCallContext );

            Assert.AreEqual( 1, result.Value );
        }
    }
}
