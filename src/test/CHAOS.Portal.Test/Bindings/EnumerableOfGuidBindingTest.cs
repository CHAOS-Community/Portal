namespace Chaos.Portal.Test.Bindings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chaos.Portal.Bindings.Standard;

    using NUnit.Framework;

    [TestFixture]
    public class EnumerableOfGuidBindingTest
    {
        private delegate void EnumerableOfGuidAction(IEnumerable<Guid> guids);

        [Test]
        public void Bind_GivenTwoGuids_BindGuidsIntoAnIEnumerableOfGuid()
        {
            EnumerableOfGuidAction action = delegate(IEnumerable<Guid> guids) { };
            var binding       = new EnumerableOfGuidParameterBinding();
            var inputGuids    = "00000000-0000-0000-0000-000000000001,10000000-0000-0000-0000-000000000000";
            var parameters = new Dictionary<string, string>() { { "guids", inputGuids } };
            var parameterInfo = action.Method.GetParameters()[0];

            var result = ((IEnumerable<Guid>)binding.Bind(parameters, parameterInfo)).ToArray();

            Assert.AreEqual(new Guid("00000000-0000-0000-0000-000000000001"), result[0]);
            Assert.AreEqual(new Guid("10000000-0000-0000-0000-000000000000"), result[1]);
        } 
    }
}