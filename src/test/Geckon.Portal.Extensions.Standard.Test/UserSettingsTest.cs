using System.Linq;
using System.Xml.Linq;
using Geckon.Portal.Core;
using Geckon.Portal.Core.Standard.Extension;
using NUnit.Framework;

namespace Geckon.Portal.Extensions.Standard.Test
{
    [TestFixture]
    public class UserSettingsTest : TestBase
    {
        [Test]
        public void Should_Get_UserSettings()
        {
            UserSettingsExtension extension = new UserSettingsExtension();

            extension.Init( new PortalContextMock() );
            extension.Get( AdminCallContext, ClientSettings.GUID.ToString() );

            Assert.IsNotNull( XDocument.Parse( extension.Result ).Descendants("Settings").FirstOrDefault() );
        }

        [Test]
        public void Should_Create_UserSettings()
        {
            UserSettingsExtension extension = new UserSettingsExtension();

            extension.Init(new PortalContextMock() );
            extension.Create( AnonCallContext, ClientSettings.GUID.ToString(), "<xml />");

            Assert.IsNotNull( XDocument.Parse( extension.Result ).Descendants("Settings").FirstOrDefault() );
        }

        [Test]
        public void Should_Delete_UserSettings()
        {
            UserSettingsExtension extension = new UserSettingsExtension();

            extension.Init( new PortalContextMock() );
            extension.Delete( AdminCallContext, ClientSettings.GUID.ToString() );

            Assert.IsNotNull(XDocument.Parse(extension.Result).Descendants("Value").FirstOrDefault());
        }

        [Test]
        public void Should_Update_UserSettings()
        {
            UserSettingsExtension extension = new UserSettingsExtension();

            extension.Init( new PortalContextMock() );
            extension.Update( AdminCallContext, ClientSettings.GUID.ToString(), "<xmllll />" );

            Assert.IsNotNull( XDocument.Parse( extension.Result ).Descendants("Value").FirstOrDefault() );
        }
    }
}
