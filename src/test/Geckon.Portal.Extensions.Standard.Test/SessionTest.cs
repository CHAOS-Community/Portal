using System.Linq;
using System.Xml.Linq;
using Geckon.Portal.Core;
using NUnit.Framework;

namespace Geckon.Portal.Extensions.Standard.Test
{
    [TestFixture]
    public class SessionTest : TestBase
    {
        [Test]
        public void Should_Create_A_New_Session()
        {
            SessionExtension sessionExtension = new SessionExtension(  );
            sessionExtension.Init( new PortalContextMock(), Session.SessionID.ToString() );
            sessionExtension.CallContext.Parameters = new[] { new Parameter( "clientSettingID", 1 ), new Parameter( "protocolVersion",3 ) };

            sessionExtension.Create( 3 );

            Assert.IsNotNull( XDocument.Parse( sessionExtension.GetContentResult().Content ).Descendants("SessionID").FirstOrDefault() );
        }

        [Test]
        public void Should_Get_A_Session()
        {
            SessionExtension sessionExtension = new SessionExtension( );
            sessionExtension.Init( new PortalContextMock(),Session.SessionID.ToString() );
            sessionExtension.CallContext.Parameters = new[] { new Parameter("sessionID", Session.SessionID.ToString()) };

            sessionExtension.Get( Session.SessionID.ToString() );

            Assert.IsNotNull( XDocument.Parse( sessionExtension.GetContentResult().Content ).Descendants("SessionID").FirstOrDefault() );
        }

        [Test]
        public void Should_Update_A_Session()
        {
            SessionExtension sessionExtension = new SessionExtension();
            sessionExtension.Init( new PortalContextMock(), Session.SessionID.ToString() );
            sessionExtension.CallContext.Parameters = new[] { new Parameter("sessionID", Session.SessionID.ToString()) };

            sessionExtension.Update( Session.SessionID.ToString() );

            Assert.IsNotNull( XDocument.Parse( sessionExtension.GetContentResult().Content ).Descendants("SessionID").FirstOrDefault() );
            Assert.AreNotEqual( Session.DateModified.ToString(), XDocument.Parse( sessionExtension.GetContentResult().Content ).Descendants("DateModified").FirstOrDefault().Value );
        }

        [Test]
        public void Should_Delete_A_Session()
        {
            SessionExtension sessionExtension = new SessionExtension();
            sessionExtension.Init( new PortalContextMock(), Session.SessionID.ToString() );
            sessionExtension.CallContext.Parameters = new[] { new Parameter("sessionID", Session.SessionID.ToString()) };

            sessionExtension.Delete( Session.SessionID.ToString() );

            Assert.IsNotNull( XDocument.Parse( sessionExtension.GetContentResult().Content ).Descendants("Geckon.Portal.Data.ScalarResult").FirstOrDefault() );
        }
    }
}
