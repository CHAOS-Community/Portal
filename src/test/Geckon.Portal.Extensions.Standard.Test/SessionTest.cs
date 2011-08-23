using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using Geckon.Portal.Core.Standard.Extension;
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
            sessionExtension.Init( new PortalContextMock(),new Result(), Session.SessionID.ToString() );
            
            ContentResult result = sessionExtension.Create( 1, 3 );

            Assert.IsNotNull( XDocument.Parse( result.Content ).Descendants("SessionID").FirstOrDefault() );
        }

        [Test]
        public void Should_Get_A_Session()
        {
            SessionExtension sessionExtension = new SessionExtension( );
            sessionExtension.Init( new PortalContextMock(),new Result(), Session.SessionID.ToString() );
            
            ContentResult create = sessionExtension.Create( 1, 3 );
            ContentResult result = sessionExtension.Get( XDocument.Parse( create.Content ).Descendants("SessionID").FirstOrDefault().Value );

            Assert.IsNotNull( XDocument.Parse( result.Content ).Descendants("SessionID").FirstOrDefault() );
        }

        [Test]
        public void Should_Update_A_Session()
        {
            SessionExtension sessionExtension = new SessionExtension();
            sessionExtension.Init( new PortalContextMock(),new Result(), Session.SessionID.ToString() );

            ContentResult result = sessionExtension.Update( Session.SessionID.ToString() );

            Assert.IsNotNull( XDocument.Parse( result.Content ).Descendants("SessionID").FirstOrDefault() );
            Assert.AreNotEqual( Session.DateModified.ToString(), XDocument.Parse( result.Content ).Descendants("DateModified").FirstOrDefault().Value );
        }

        [Test]
        public void Should_Delete_A_Session()
        {
            SessionExtension sessionExtension = new SessionExtension();
            sessionExtension.Init( new PortalContextMock(),new Result(), Session.SessionID.ToString() );

            ContentResult result = sessionExtension.Delete( Session.SessionID.ToString() );

            Assert.IsNotNull( XDocument.Parse( result.Content ).Descendants("Geckon.Portal.Data.ScalarResult").FirstOrDefault() );
        }
    }
}
