using System.Linq;
using System.Xml.Linq;
using Geckon.Portal.Core.Standard.Extension;
using NUnit.Framework;

namespace Geckon.Portal.Extensions.Standard.Test
{
    [TestFixture]
    public class UserTest : TestBase
    {
        [Test]
        public void Should_Update_User()
        {
            UserExtension extension = new UserExtension( new PortalContextMock() );
            extension.Init( new Result() );

            XDocument xdoc = XDocument.Parse( extension.Update( Session.SessionID.ToString(), "new", null, null, null ).Content );

            Assert.AreEqual( "new", xdoc.Descendants( "Firstname" ).First().Value );
        }

        [Test]
        public void Should_Create_User()
        {
            UserExtension extension = new UserExtension(new PortalContextMock());
            extension.Init(new Result());

            XDocument xdoc = XDocument.Parse(extension.Create(Session.SessionID.ToString(), "new", null, null, "email").Content);

            Assert.AreEqual("new", xdoc.Descendants("Firstname").First().Value);
        }

        [Test]
        public void Should_Get_User()
        {
            UserExtension extension = new UserExtension(new PortalContextMock());
            extension.Init(new Result());

            XDocument xdoc = XDocument.Parse(extension.Get(Session.SessionID.ToString()).Content);

            Assert.AreEqual("Anonymous", xdoc.Descendants("Firstname").First().Value);
        }

        [Test]
        public void Should_Delete_User()
        {
            UserExtension extension = new UserExtension(new PortalContextMock());
            extension.Init(new Result());

            XDocument xdoc = XDocument.Parse( extension.Delete( Session.SessionID.ToString(), User.GUID.ToString() ).Content);

            Assert.AreEqual("1", xdoc.Descendants("Value").First().Value);
        }
    }
}
