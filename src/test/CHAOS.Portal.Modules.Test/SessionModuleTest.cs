using System.Configuration;
using CHAOS.Portal.Test;
using NUnit.Framework;

namespace CHAOS.Portal.Modules.Test
{
    [TestFixture]
    public class SessionModuleTest : TestBase
    {
        #region Constructors

        private SessionModule SessionModule { get; set; }

        [SetUp]
        public void SetUp()
        {
            base.SetUp();

            SessionModule = new SessionModule();
            SessionModule.Initialize( ConfigurationManager.ConnectionStrings["PortalEntities"].ConnectionString );
        }

        #endregion

        [Test]
        public void Should_Create_Session()
        {
            var session = SessionModule.Create( AnonCallContext, 4 );

            Assert.AreEqual( AnonCallContext.User.GUID.ToByteArray(), session.UserGUID.ToByteArray() );
        }

        [Test]
        public void Should_Get_A_Session()
        {
            var session = SessionModule.Get( AnonCallContext );

            Assert.AreEqual( AnonCallContext.User.GUID.ToByteArray(), session.UserGUID.ToByteArray() );
        }

        [Test]
        public void Should_Update_A_Session()
        {
            var session = SessionModule.Update( AdminCallContext );

            Assert.IsNotNull( session.DateModified );
        }

        [Test]
        public void Should_Delete_A_Session()
        {
            var result = SessionModule.Delete( AdminCallContext );

            Assert.AreEqual( 1, result.Value );
        }
    }
}
