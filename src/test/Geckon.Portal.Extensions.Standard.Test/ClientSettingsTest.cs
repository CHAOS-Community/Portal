using System.Linq;
using System.Xml.Linq;
using Geckon.Portal.Core;
using NUnit.Framework;

namespace Geckon.Portal.Extensions.Standard.Test
{
    [TestFixture]
    public class ClientSettingsTest : TestBase
    {
        [Test]
        public void Should_Get_ClientSettings()
        {
            ClientSettingsExtension extension = new ClientSettingsExtension();
            extension.Init(new PortalContextMock(), AdminSession.SessionID.ToString());
            extension.CallContext.Parameters = new Parameter[0];

            extension.Get(AdminUser.SessionID.ToString(), ClientSettings.GUID.ToString() );

            Assert.AreEqual(ClientSettings.GUID.ToString(), XDocument.Parse(extension.Result).Descendants("GUID").First().Value);
        }
    }
}
