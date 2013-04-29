namespace Chaos.Portal.Test.Bindings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Chaos.Portal.Bindings.Standard;
    using Chaos.Portal.Core.Exceptions;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class GuidParameterBindingTest
    {
        private delegate void GuidAction(Guid guid);
        private delegate void NullableGuidAction( Guid? guid );

        [Test]
        public void Bind_GivenAGuidWithDataInTheSegnificantPart_BindGuidsIntoAnIEnumerableOfGuid()
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

        [Test, ExpectedException( typeof( ParameterBindingMissingException ) )]
        public void Bind_GivenEmptyGuid_ReturnNull()
        {
            GuidAction action = delegate(Guid guid) { };
            var callContext   = new Mock<ICallContext>();
            var binding       = new GuidParameterBinding();
            var inputGuids    = "00000000-0000-0000-0000-000000000000";
            var parameterInfo = action.Method.GetParameters()[0];
            callContext.SetupGet(p => p.Request.Parameters).Returns(new Dictionary<string, string>() { { "guid", inputGuids } });

            binding.Bind(callContext.Object, parameterInfo);
        }

        [Test, ExpectedException( typeof( ParameterBindingMissingException ) )]
        public void Bind_GivenEmptyInput_ReturnNull()
        {
            GuidAction action = delegate( Guid guid ) { };
            var callContext   = new Mock<ICallContext>();
            var binding       = new GuidParameterBinding();
            var parameterInfo = action.Method.GetParameters()[0];
            callContext.SetupGet( p => p.Request.Parameters ).Returns( new Dictionary<string, string>() { { "guid", "" } } );

            binding.Bind( callContext.Object, parameterInfo );
        }

        [Test, ExpectedException( typeof( ParameterBindingMissingException ) )]
        public void Bind_WhereParameterIsNotInCollection_ReturnNull()
        {
            GuidAction action = delegate( Guid guid ) { };
            var callContext   = new Mock<ICallContext>();
            var binding       = new GuidParameterBinding();
            var parameterInfo = action.Method.GetParameters()[0];
            callContext.SetupGet( p => p.Request.Parameters ).Returns( new Dictionary<string, string>() );

            binding.Bind( callContext.Object, parameterInfo );
        }

        [Test]
        public void Bind_GivenEmptyGuidParameterIsNullable_ReturnNull()
        {
            NullableGuidAction action = delegate( Guid? guid ) { };
            var callContext   = new Mock<ICallContext>();
            var binding       = new GuidParameterBinding();
            var inputGuids    = "00000000-0000-0000-0000-000000000000";
            var parameterInfo = action.Method.GetParameters()[0];
            callContext.SetupGet( p => p.Request.Parameters ).Returns( new Dictionary<string, string>() { { "guid", inputGuids } } );

            var result = binding.Bind( callContext.Object, parameterInfo );

            Assert.IsNull( result );
        } 
    }
}