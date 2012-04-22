using System.Reflection;
using CHAOS.Portal.Exception;
using CHAOS.Portal.Test;
using NUnit.Framework;

namespace CHAOS.Portal.Core.Test
{
    [TestFixture]
    public class ParameterBindingsTest : TestBase
    {
        [Test]
        public void Should_Should_Bind_String_Parameter()
        {
            AnonCallContext.PortalRequest.Parameters.Add( "test", "string value" );
            ParameterInfo parameterInfo = typeof(MockExtension).GetMethod("TestStringParameter").GetParameters()[1];

            Assert.AreEqual( "string value", PortalApplication.Bindings[typeof(string)].Bind( AnonCallContext, parameterInfo ) );
        }

        [Test]
        public void Should_Should_Bind_Null_String_Parameter()
        {
            AnonCallContext.PortalRequest.Parameters.Add( "test", null );
            ParameterInfo parameterInfo = typeof(MockExtension).GetMethod("TestStringParameter").GetParameters()[1];

            Assert.AreEqual( null, PortalApplication.Bindings[typeof(string)].Bind( AnonCallContext, parameterInfo) );
        }

        [Test]
        public void Should_Should_Bind_Int32_Parameter()
        {
            AnonCallContext.PortalRequest.Parameters.Add( "test", "1000" );
            ParameterInfo parameterInfo = typeof(MockExtension).GetMethod("TestInt32Parameter").GetParameters()[1];

            Assert.AreEqual( 1000, PortalApplication.Bindings[typeof(int)].Bind( AnonCallContext, parameterInfo) );
        }

        [Test, ExpectedException(typeof(ParameterBindingMissingException))]
        public void Should_Should_Throw_ParameterBindingMissingException_If_Int32_Parameter_Is_Missing()
        {
            AnonCallContext.PortalRequest.Parameters.Add( "test", null );
            ParameterInfo parameterInfo = typeof(MockExtension).GetMethod("TestInt32Parameter").GetParameters()[1];

            PortalApplication.Bindings[typeof(int)].Bind( AnonCallContext, parameterInfo );
        }

        [Test]
        public void Should_Should_Bind_Null_Int32_Parameter()
        {
            AnonCallContext.PortalRequest.Parameters.Add( "test", null );
            ParameterInfo parameterInfo = typeof(MockExtension).GetMethod("TestNullableInt32Parameter").GetParameters()[1];

            Assert.AreEqual( null, PortalApplication.Bindings[typeof(int)].Bind( AnonCallContext, parameterInfo ) );
        }

        [Test]
        public void Should_Should_Bind_UInt32_Parameter()
        {
            AnonCallContext.PortalRequest.Parameters.Add( "test", "1000" );
            ParameterInfo parameterInfo = typeof(MockExtension).GetMethod("TestUInt32Parameter").GetParameters()[1];

            Assert.AreEqual( 1000, PortalApplication.Bindings[typeof(uint)].Bind( AnonCallContext, parameterInfo) );
        }

        [Test]
        public void Should_Should_Bind_Null_UInt32_Parameter()
        {
            AnonCallContext.PortalRequest.Parameters.Add( "test", null );
            ParameterInfo parameterInfo = typeof(MockExtension).GetMethod("TestNullableUInt32Parameter").GetParameters()[1];

            Assert.AreEqual( null, PortalApplication.Bindings[typeof(uint)].Bind( AnonCallContext, parameterInfo ) );
        }

        [Test]
        public void Should_Should_Bind_UUID_Parameter()
        {
            AnonCallContext.PortalRequest.Parameters.Add( "test", "21ec2020-3aea-1069-a2dd-08002b30309d" );
            ParameterInfo parameterInfo = typeof(MockExtension).GetMethod("TestUUIDParameter").GetParameters()[1];

            Assert.AreEqual( "21ec2020-3aea-1069-a2dd-08002b30309d", PortalApplication.Bindings[typeof(UUID)].Bind( AnonCallContext, parameterInfo).ToString() );
        }

        [Test]
        public void Should_Should_Bind_Null_UUID_Parameter()
        {
            AnonCallContext.PortalRequest.Parameters.Add( "test", null );
            ParameterInfo parameterInfo = typeof(MockExtension).GetMethod("TestNullableUUIDParameter").GetParameters()[1];

            Assert.AreEqual( null, PortalApplication.Bindings[typeof(UUID)].Bind( AnonCallContext, parameterInfo ) );
        }
    }
}
