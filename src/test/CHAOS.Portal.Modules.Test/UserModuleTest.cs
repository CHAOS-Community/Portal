using System.Configuration;
using CHAOS.Portal.Test;
using NUnit.Framework;

namespace CHAOS.Portal.Modules.Test
{
    [TestFixture]
    public class UserModuleTest : TestBase
    {
        #region Constructors

        private UserModule UserModule { get; set; }

        [SetUp]
        public new void SetUp()
        {
            base.SetUp();

            UserModule = new UserModule();
            UserModule.Initialize( string.Format( "<Settings ConnectionString=\"{0}\" />", ConfigurationManager.ConnectionStrings["PortalEntities"].ConnectionString.Replace("\"", "&quot;") ) );
        }

        #endregion

        [Test]
        public void Should_Get_User()
        {
            var user = UserModule.Get( AnonCallContext );

            Assert.AreEqual( "anon@ymo.us", user.Email );
        }
    }
}
