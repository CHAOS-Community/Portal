using System.Collections.Generic;
using Geckon.Portal.Core.Extension;
using Geckon.Portal.Core.Standard.Extension;
using Geckon.Serialization.Xml;
using NUnit.Framework;

namespace Geckon.Portal.Core.Standard.Test
{
    [TestFixture]
    public class ResultTest
    {
        [Test]
        public void Should_Add_Content_To_Result()
        {
            IResult result = new Result();

            result.Add( "Geckon.Portal.Extension.TestModule", new ContentResultTestDummy() );

            Assert.Greater( result.Content.Length, 50 );
        }

        [Test]
        public void Should_Add_Content_Range_To_Result()
        {
            IResult result = new Result();
            IList<ContentResultTestDummy> range = new List<ContentResultTestDummy>();

            range.Add( new ContentResultTestDummy() );
            range.Add( new ContentResultTestDummy() );
            range.Add( new ContentResultTestDummy() );
            range.Add( new ContentResultTestDummy() );
            range.Add( new ContentResultTestDummy() );
            
            result.Add( "Geckon.Portal.Extension.TestModule", range );

            Assert.Greater(result.Content.Length, 50);
        }
    }

    public class ContentResultTestDummy : XmlSerialize
    {
        [Element]
        public int SomeValue
        {
            get { return 4; }
        }
    }
}
