using System;
using System.IO;
using System.Reflection;
using CHAOS;

namespace Chaos.Portal.Bindings.Standard
{
    using System.Collections.Generic;

    public class UUIDParameterBinding : IParameterBinding
    {
        public object Bind(IDictionary<string, string> parameters, ParameterInfo parameterInfo)
        {
            if (parameters.ContainsKey(parameterInfo.Name) && !string.IsNullOrEmpty(parameters[parameterInfo.Name]))
                return new UUID(StringToByteArray(parameters[parameterInfo.Name]));

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
