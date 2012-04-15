using System.Collections.Generic;
using CHAOS.Portal.Core.Request;
using CHAOS.Portal.Core.Standard;
using CHAOS.Portal.Exception;
using NUnit.Framework;

namespace CHAOS.Portal.Core.Test
{
    [TestFixture]
    public class PortalApplicationTest : TestBase
    {
        [Test]
        public void Should_Process_PortalResult()
        {
            ICallContext callContext = new CallContext( PortalApplication, new PortalRequest( "MockExtension", "Test", new Dictionary<string, string>() ), new PortalResponse(  ) );

            PortalApplication.LoadedExtensions.Add( "MockExtension", new MockExtensionLoader() );
            PortalApplication.ProcessRequest( callContext );
            
            Assert.AreEqual( "Test", ( callContext.PortalResponse.PortalResult.GetModule( "CHAOS.Portal.Core.Test" ).Results[0] as StringResult ).Result );
        }

        [Test,ExpectedException(typeof(ExtensionMissingException))]
        public void Should_Throw_ExtensionMissingException_On_Process_PortalResult_If_Extension_Isnt_Loaded()
        {
            PortalApplication.ProcessRequest( new CallContext( PortalApplication, new PortalRequest( "MockExtension", "Test", new Dictionary<string, string>() ), new PortalResponse(  ) ) );
        }
    }
}
