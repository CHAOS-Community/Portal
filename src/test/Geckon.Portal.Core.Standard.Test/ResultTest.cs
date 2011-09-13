using System.Collections.Generic;
using Geckon.Portal.Data.Result.Standard;
using Geckon.Serialization;
using Geckon.Serialization.Standard.String;
using NUnit.Framework;

namespace Geckon.Portal.Core.Standard.Test
{
    [TestFixture]
    public class ResultTest
    {
        [Test]
        public void Should_Add_Content_To_Result()
        {
            PortalResult portalResult = new PortalResult();

            portalResult.GetModule( "Geckon.Portal.Extension.TestModule").AddResult( new ContentResultTestDummy() );

            Assert.Greater( new Serialization.Standard.XML.XMLSerializer( new StringSerializer() ).Serialize( portalResult, false ).ToString().Length, 50 );
        }

        [Test]
        public void Should_Add_Content_Range_To_Result()
        {
            PortalResult portalResult = new PortalResult();
            IList<ContentResultTestDummy> range = new List<ContentResultTestDummy>();

            range.Add( new ContentResultTestDummy() );
            range.Add( new ContentResultTestDummy() );
            range.Add( new ContentResultTestDummy() );
            range.Add( new ContentResultTestDummy() );
            range.Add( new ContentResultTestDummy() );
            
            portalResult.GetModule( "Geckon.Portal.Extension.TestModule" ).AddResult( range );

            Assert.Greater( new Serialization.Standard.XML.XMLSerializer( new StringSerializer() ).Serialize( portalResult, false ).ToString().Length, 50 );
        }
    }

    public class ContentResultTestDummy : Result
    {
        [Serialize("SomeValue")]
        public int SomeValue
        {
            get { return 4; }
        }
    }
}
