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
            extension.Init(new PortalContextMock(), new Result(), AdminUser.SessionID.ToString());
            extension.CallContext.Parameters = new Parameter[0];

            extension.Get( AdminUser.SessionID.ToString(), ClientSettings.GUID.ToString() );

            Assert.IsNotNull( XDocument.Parse( extension.GetContentResult().Content ).Descendants("Settings").FirstOrDefault() );
        }

        [Test]
        public void Should_Create_UserSettings()
        {
            UserSettingsExtension extension = new UserSettingsExtension();
            extension.Init(new PortalContextMock(), new Result(), Session.SessionID.ToString());
            extension.CallContext.Parameters = new Parameter[0];

            extension.Create(Session.SessionID.ToString(), ClientSettings.GUID.ToString(), "<xml />");

            Assert.IsNotNull( XDocument.Parse( extension.GetContentResult().Content ).Descendants("Settings").FirstOrDefault() );
        }

        [Test]
        public void Should_Delete_UserSettings()
        {
            UserSettingsExtension extension = new UserSettingsExtension();
            extension.Init(new PortalContextMock(), new Result(), AdminUser.SessionID.ToString());
            extension.CallContext.Parameters = new Parameter[0];

            extension.Delete( AdminUser.SessionID.ToString(), ClientSettings.GUID.ToString() );

            Assert.IsNotNull(XDocument.Parse(extension.GetContentResult().Content).Descendants("Value").FirstOrDefault());
        }

        [Test]
        public void Should_Update_UserSettings()
        {
            UserSettingsExtension extension = new UserSettingsExtension();
            extension.Init(new PortalContextMock(), new Result(), AdminUser.SessionID.ToString());
            extension.CallContext.Parameters = new Parameter[0];

            extension.Update( AdminUser.SessionID.ToString(), ClientSettings.GUID.ToString(), "<xmllll />" );

            Assert.IsNotNull( XDocument.Parse( extension.GetContentResult().Content ).Descendants("Value").FirstOrDefault() );
        }
    }
}
