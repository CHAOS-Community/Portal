namespace Chaos.Portal.Test.Bindings
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    using Chaos.Portal.Bindings.Standard;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class XDocumentBindingTest
    {
        private delegate void XDocumentAction(XDocument xml);

        [Test]
        public void Bind_GivenValidXml_ReturnAXDocumentContainingTheXml()
        {
            XDocumentAction action = delegate(XDocument xml) { };
            var callContext        = new Mock<ICallContext>();
            var binding            = new XDocumentBinding();
            var xmlInput           = "<xml>sample</xml>";
            var parameterInfo      = action.Method.GetParameters().First();
            callContext.SetupGet(p => p.Request.Parameters).Returns(new Dictionary<string, string>() { { "xml", xmlInput } });

            var result = binding.Bind(callContext.Object, parameterInfo);

            Assert.AreEqual(xmlInput, result.ToString());
        } 
    }
}