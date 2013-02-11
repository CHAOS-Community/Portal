namespace Chaos.Portal.Test.Bindings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chaos.Portal.Bindings.Standard;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class GuidParameterBindingTest
    {
        private delegate void GuidAction(Guid guid);

        [Test]
        public void Bind_GivenTwoGuids_BindGuidsIntoAnIEnumerableOfGuid()
        {
            GuidAction action = delegate(Guid guid) { };
            var callContext   = new Mock<ICallContext>();
            var binding       = new GuidParameterBinding();
            var inputGuids    = "10000000-0000-0000-0000-000000000000";
            var parameterInfo = action.Method.GetParameters()[0];
            callContext.SetupGet(p => p.Request.Parameters).Returns(new Dictionary<string, string>() { { "guid", inputGuids } });

            var result = binding.Bind(callContext.Object, parameterInfo);

            Assert.AreEqual(new Guid("10000000-0000-0000-0000-000000000000"), result);
        } 
    }
}