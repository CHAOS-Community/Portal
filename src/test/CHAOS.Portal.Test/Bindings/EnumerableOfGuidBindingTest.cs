namespace Chaos.Portal.Test.Bindings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chaos.Portal.Bindings.Standard;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class EnumerableOfGuidBindingTest
    {
        private delegate void EnumerableOfGuidAction(IEnumerable<Guid> guids);

        [Test]
        public void Bind_GivenTwoGuids_BindGuidsIntoAnIEnumerableOfGuid()
        {
            EnumerableOfGuidAction action = delegate(IEnumerable<Guid> guids) { };
            var callContext   = new Mock<ICallContext>();
            var binding       = new EnumerableOfGuidParameterBinding();
            var inputGuids    = "00000000-0000-0000-0000-000000000001,10000000-0000-0000-0000-000000000000";
            var parameterInfo = action.Method.GetParameters()[0];
            callContext.SetupGet(p => p.Request.Parameters).Returns(new Dictionary<string, string>() { { "guids", inputGuids } });

            var result = ((IEnumerable<Guid>) binding.Bind(callContext.Object, parameterInfo)).ToArray();

            Assert.AreEqual(new Guid("00000000-0000-0000-0000-000000000001"), result[0]);
            Assert.AreEqual(new Guid("10000000-0000-0000-0000-000000000000"), result[1]);
        } 
    }
}