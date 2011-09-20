using System.Linq;
using System.Xml.Linq;
using Geckon.Portal.Core;
using NUnit.Framework;

namespace Geckon.Portal.Extensions.Standard.Test
{
    [TestFixture]
    public class EmailPasswordTest : TestBase
    {
        [Test]
        public void Should_Create_Password_For_User()
        {
            EmailPasswordExtension extension = new EmailPasswordExtension( );
            extension.Init( new PortalContextMock(), Session.SessionID.ToString() );
            extension.CallContext.Parameters = new[] { new Parameter("sessionID", Session.SessionID.ToString()), new Parameter("userGUID", User.GUID.ToString()), new Parameter("password", "pbvu7000")};

            extension.CreatePassword( Session.SessionID.ToString(), User.GUID.ToString(), "pbvu7000" );
            
            Assert.IsNotNull( XDocument.Parse(extension.Result).Descendants( "GUID" ).FirstOrDefault() );
        }

        [Test]
        public void Should_Log_User_In()
        {
            EmailPasswordExtension extension = new EmailPasswordExtension();
            extension.Init( new PortalContextMock(), Session.SessionID.ToString() );
            extension.CallContext.Parameters = new[] { new Parameter("sessionID", AdminUser.SessionID.ToString()), new Parameter("email", AdminUser.Email), new Parameter("password", "pbvu7000")};

            extension.Login( AdminUser.SessionID.ToString(), AdminUser.Email, "pbvu7000" );

            Assert.IsNotNull(XDocument.Parse(extension.Result).Descendants("GUID").FirstOrDefault());
            Assert.AreEqual(1, XDocument.Parse( extension.Result ).Descendants("Result").Count());
        }
    }
}
