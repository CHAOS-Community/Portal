using System.Linq;
using System.Xml.Linq;
using Geckon.Portal.Core;
using Geckon.Portal.Test;
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

            extension.Init( new PortalContextMock() );
            extension.Get( AdminCallContext, ClientSettings.GUID.ToString() );

            Assert.AreEqual(ClientSettings.GUID.ToString(), XDocument.Parse(extension.Result).Descendants("GUID").First().Value);
        }
    }
}
