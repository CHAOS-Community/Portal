using System.Configuration;
using CHAOS.Portal.Test;
using NUnit.Framework;

namespace CHAOS.Portal.Modules.Test
{
    [TestFixture]
    public class UserSettingsTest : TestBase
    {
        #region Constructors

        private UserSettingsModule UserSettingsModule { get; set; }

        [SetUp]
        public void SetUp()
        {
            base.SetUp();

            UserSettingsModule = new UserSettingsModule();
            UserSettingsModule.Initialize( string.Format( "<Settings ConnectionString=\"{0}\" />", ConfigurationManager.ConnectionStrings["PortalEntities"].ConnectionString.Replace("\"", "&quot;") ) );
        }

        #endregion

        [Test]
        public void Should_Get_UserSettings()
        {
            var userSettings = UserSettingsModule.Get( AdminCallContext, ClientSettings.GUID );

            Assert.IsNotNull( userSettings.Settings );
        }

        [Test]
        public void Should_Delete_UserSettings()
        {
            var result = UserSettingsModule.Delete( AdminCallContext, ClientSettings.GUID );

            Assert.AreEqual( 1, result.Value );
        }

        [Test]
        public void Should_Set_UserSettings()
        {
            var result = UserSettingsModule.Set( AdminCallContext, ClientSettings.GUID, "<xmllll />" );

            Assert.AreEqual( 1, result.Value );
        }
    }
}
