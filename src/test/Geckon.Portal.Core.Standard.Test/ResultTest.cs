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

            Assert.AreEqual("<Geckon.Portal.Extension.TestModule Count=\"1\"><Geckon.Portal.Core.Standard.Test.ContentResultTestDummy><SomeValue>4</SomeValue></Geckon.Portal.Core.Standard.Test.ContentResultTestDummy></Geckon.Portal.Extension.TestModule>", result.Content);
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

            Assert.AreEqual("<Geckon.Portal.Extension.TestModule Count=\"5\"><Geckon.Portal.Core.Standard.Test.ContentResultTestDummy><SomeValue>4</SomeValue></Geckon.Portal.Core.Standard.Test.ContentResultTestDummy><Geckon.Portal.Core.Standard.Test.ContentResultTestDummy><SomeValue>4</SomeValue></Geckon.Portal.Core.Standard.Test.ContentResultTestDummy><Geckon.Portal.Core.Standard.Test.ContentResultTestDummy><SomeValue>4</SomeValue></Geckon.Portal.Core.Standard.Test.ContentResultTestDummy><Geckon.Portal.Core.Standard.Test.ContentResultTestDummy><SomeValue>4</SomeValue></Geckon.Portal.Core.Standard.Test.ContentResultTestDummy><Geckon.Portal.Core.Standard.Test.ContentResultTestDummy><SomeValue>4</SomeValue></Geckon.Portal.Core.Standard.Test.ContentResultTestDummy></Geckon.Portal.Extension.TestModule>", result.Content);
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
