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

            sessionExtension.Init( new PortalContextMock() );
            sessionExtension.Create( AnonCallContext, 3 );

            Assert.IsNotNull( XDocument.Parse( sessionExtension.Result ).Descendants("SessionID").FirstOrDefault() );
        }

        [Test]
        public void Should_Get_A_Session()
        {
            SessionExtension sessionExtension = new SessionExtension( );

            sessionExtension.Init( new PortalContextMock() );
            sessionExtension.Get( AnonCallContext );

            Assert.IsNotNull( XDocument.Parse( sessionExtension.Result ).Descendants("SessionID").FirstOrDefault() );
        }

        [Test]
        public void Should_Update_A_Session()
        {
            SessionExtension sessionExtension = new SessionExtension();

            sessionExtension.Init( new PortalContextMock() );
            sessionExtension.Update( AnonCallContext );

            Assert.IsNotNull( XDocument.Parse( sessionExtension.Result ).Descendants("SessionID").FirstOrDefault() );
            Assert.AreNotEqual( Session.DateModified.ToString(), XDocument.Parse( sessionExtension.Result ).Descendants("DateModified").FirstOrDefault().Value );
        }

        [Test]
        public void Should_Delete_A_Session()
        {
            SessionExtension sessionExtension = new SessionExtension();

            sessionExtension.Init( new PortalContextMock() );
            sessionExtension.Delete( AnonCallContext );

            Assert.IsNotNull( XDocument.Parse( sessionExtension.Result ).Descendants("Result").FirstOrDefault() );
        }
    }
}
