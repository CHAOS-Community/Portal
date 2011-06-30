using System.Web.Mvc;
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

            Assert.AreEqual( "", result.Content );
        }
    }
}
