using System;
using System.IO;
using System.Reflection;

namespace Chaos.Portal.Bindings.Standard
{
    public class GuidParameterBinding : IParameterBinding
    {
        public object Bind( ICallContext callContext, ParameterInfo parameterInfo )
        {
            if( callContext.Request.Parameters.ContainsKey( parameterInfo.Name ) && !string.IsNullOrEmpty( callContext.Request.Parameters[ parameterInfo.Name ] ) )
                return new Guid( StringToByteArray( callContext.Request.Parameters[ parameterInfo.Name ] ) );

            return null;
        }

        private static byte[] StringToByteArray(string hex)
        {
            hex = hex.Replace("-", "");

            var numberChars = hex.Length / 2;
            var bytes       = new byte[numberChars];

            using (var sr = new StringReader(hex))
            {
                for (var i = 0; i < numberChars; i++)
                    bytes[i] = Convert.ToByte(new string(new char[2] { (char)sr.Read(), (char)sr.Read() }), 16);
            }

            return bytes;
        }
    }
}
