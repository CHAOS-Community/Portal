namespace Chaos.Portal.Test.Bindings
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Bindings.Standard;

    using NUnit.Framework;

    [TestFixture]
  public class EnumerableOfStringBindingTest
    {
      private delegate void EnumerableAction(IEnumerable<string> guids);

        [Test]
        public void Bind_GivenTwoGuids_BindGuidsIntoAnIEnumerableOfGuid()
        {
          EnumerableAction action = delegate(IEnumerable<string> inputs) { };
            var binding       = new EnumerableOfStringParameterBinding();
            var inputGuids    = "hi,hey";
            var parameters = new Dictionary<string, string>() { { "inputs", inputGuids } };
            var parameterInfo = action.Method.GetParameters()[0];

            var result = ((IEnumerable<string>)binding.Bind(parameters, parameterInfo)).ToArray();

            Assert.That(result[0], Is.EqualTo("hi"));
            Assert.That(result[1], Is.EqualTo("hey"));
        } 
    }
}