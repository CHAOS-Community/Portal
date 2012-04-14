using System;
using System.Collections.Generic;
using CHAOS.Portal.Core.Request.Bindings;

namespace CHAOS.Portal.Core.Request
{
    public class ParameterBindings
    {
        #region Properties

        public IDictionary<Type, IRequestParameterBinding> Bindings { get; set; }

        #endregion
        #region Construction

        public ParameterBindings()
        {
            Bindings = new Dictionary<Type, IRequestParameterBinding>();

            // Add default bindings
            Bindings.Add( typeof(string), new StringParameterBinding() );
        }

        #endregion
        #region Business Logic

        #endregion
    }
}
