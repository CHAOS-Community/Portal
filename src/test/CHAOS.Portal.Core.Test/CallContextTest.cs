using CHAOS.Portal.Test;
using NUnit.Framework;

namespace CHAOS.Portal.Core.Test
{
	[TestFixture]
	public class CallContextTest : TestBase
	{
		[Test]
		public void Should_Detegfrmine_If_User_Is_Anonymous()
		{
			Assert.IsTrue( AnonCallContext.IsAnonymousUser );
			Assert.IsTrue( AnonCallContext2.IsAnonymousUser );
			Assert.IsFalse( AdminCallContext.IsAnonymousUser );
		}
	}
}
