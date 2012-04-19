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
    }
}
