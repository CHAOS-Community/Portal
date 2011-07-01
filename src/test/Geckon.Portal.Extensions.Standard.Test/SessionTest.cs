using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using Geckon.Portal.Core.Standard.Extension;
using NUnit.Framework;

namespace Geckon.Portal.Extensions.Standard.Test
{
    [TestFixture]
    public class SessionTest
    {
        [Test]
        public void Should_Create_A_New_Session()
        {
            SessionExtension sessionExtension = new SessionExtension( new PortalContextMock() );
            sessionExtension.Init( new Result() );
            
            ContentResult result = sessionExtension.Create( 1, 3 );
            XDocument xml = XDocument.Parse( result.Content );
            
            Assert.IsNotNull( xml.Descendants("SessionID").FirstOrDefault() );
        }
    }
}
