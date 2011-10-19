using System;

namespace Geckon.Portal.Core.Standard
{
    public class Parameter : IParameter
    {
        #region Properties

        public string ParameterName { get; set; }
        public object Value { get; set; }
        public Type   Type { get; set; }

        #endregion
        #region Construction

        public Parameter( string parameterName, object value )
        {
            ParameterName = parameterName;
            Value         = value;
            Type          = value == null ? null : value.GetType();
        }

        public Parameter( string parameterName, Type type )
        {
            ParameterName = parameterName;
            Type          = type;
        }

        #endregion
    }
}
