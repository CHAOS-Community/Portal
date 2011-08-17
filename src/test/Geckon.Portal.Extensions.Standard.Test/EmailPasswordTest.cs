using System.Linq;
using System.Xml.Linq;
using Geckon.Portal.Core.Standard.Extension;
using NUnit.Framework;

namespace Geckon.Portal.Extensions.Standard.Test
{
    [TestFixture]
    public class EmailPasswordTest : TestBase
    {
        [Test]
        public void Should_Create_Password_For_User()
        {
            EmailPasswordExtension extension = new EmailPasswordExtension( new PortalContextMock() );
            extension.Init( new Result(), Session.SessionID.ToString() );

            XDocument xdoc = XDocument.Parse( extension.CreatePassword( Session.SessionID.ToString(), User.GUID.ToString(), "pbvu7000" ).Content );
            
            Assert.IsNotNull( xdoc.Descendants( "GUID" ).FirstOrDefault() );
        }

        [Test]
        public void Should_Log_User_In()
        {
            EmailPasswordExtension extension = new EmailPasswordExtension(new PortalContextMock());
            extension.Init( new Result(), Session.SessionID.ToString() );

            extension.CreatePassword(Session.SessionID.ToString(), User.GUID.ToString(), "pbvu7000");

            extension = new EmailPasswordExtension(new PortalContextMock());
            extension.Init( new Result(), Session.SessionID.ToString() );

            XDocument xdoc = XDocument.Parse(extension.LoginEmailPassword( Session.SessionID.ToString(), User.Email, "pbvu7000" ).Content );

            Assert.IsNotNull(xdoc.Descendants("GUID").FirstOrDefault());
            Assert.AreEqual(1, xdoc.Descendants("Geckon.Portal.Data.Dto.UserInfo").Count() );
        }
    }
}
