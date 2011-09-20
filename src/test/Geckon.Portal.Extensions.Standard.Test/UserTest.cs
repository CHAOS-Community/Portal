using System.Linq;
using System.Xml.Linq;
using Geckon.Portal.Core;
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
            UserExtension extension = new UserExtension(  );
            extension.Init( new PortalContextMock(), Session.SessionID.ToString() );
            extension.CallContext.Parameters = new Parameter[0];

            extension.Update( Session.SessionID.ToString(), "new", null, null, null  );

            Assert.AreEqual( "new", XDocument.Parse(extension.Result).Descendants( "Firstname" ).First().Value );
        }

        [Test]
        public void Should_Create_User()
        {
            UserExtension extension = new UserExtension();
            extension.Init( new PortalContextMock(), Session.SessionID.ToString() );
            extension.CallContext.Parameters = new Parameter[0];

            extension.Create(Session.SessionID.ToString(), "new", null, null, "email");

            Assert.AreEqual("new", XDocument.Parse(extension.Result).Descendants("Firstname").First().Value);
        }

        [Test]
        public void Should_Get_User()
        {
            UserExtension extension = new UserExtension();
            extension.Init( new PortalContextMock(), Session.SessionID.ToString() );
            extension.CallContext.Parameters = new Parameter[0];

            extension.Get(Session.SessionID.ToString());

            Assert.AreEqual("Anonymous", XDocument.Parse(extension.Result).Descendants("Firstname").First().Value);
        }

        [Test]
        public void Should_Delete_User()
        {
            UserExtension extension = new UserExtension();
            extension.Init( new PortalContextMock(), Session.SessionID.ToString() );
            extension.CallContext.Parameters = new Parameter[0];

            extension.Delete( Session.SessionID.ToString(), User.GUID.ToString() );

            Assert.AreEqual("1", XDocument.Parse(extension.Result).Descendants("Value").First().Value);
        }
    }
}
