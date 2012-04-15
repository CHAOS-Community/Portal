using System;
using System.Collections.Generic;
using CHAOS.Portal.Core.Request.Bindings;
using CHAOS.Portal.Core.Standard;
using Geckon;

namespace CHAOS.Portal.Core.Request
{
    public class ParameterBindings
    {
        #region Properties

        public IDictionary<Type, IParameterBinding> Bindings { get; set; }

        #endregion
        #region Construction

        public ParameterBindings()
        {
            Bindings = new Dictionary<Type, IParameterBinding>();

            // Add default bindings
            Bindings.Add( typeof(string), new StringParameterBinding() );
            Bindings.Add( typeof(ICallContext), new CallContextParameterBinding() );
            Bindings.Add( typeof(CallContext), new CallContextParameterBinding() );
            Bindings.Add( typeof(long), new ConvertableParameterBinding<long>() );
            Bindings.Add( typeof(int), new ConvertableParameterBinding<int>() );
            Bindings.Add( typeof(short), new ConvertableParameterBinding<short>() );
            Bindings.Add( typeof(ulong), new ConvertableParameterBinding<ulong>() );
            Bindings.Add( typeof(uint), new ConvertableParameterBinding<uint>() );
            Bindings.Add( typeof(ushort), new ConvertableParameterBinding<ushort>() );
            Bindings.Add( typeof(double), new ConvertableParameterBinding<double>() );
            Bindings.Add( typeof(float), new ConvertableParameterBinding<float>() );
            Bindings.Add( typeof(DateTime), new ConvertableParameterBinding<DateTime>() );
            Bindings.Add( typeof(UUID), new UUIDParameterBinding() );
        }

        #endregion
        #region Business Logic

        #endregion
    }
}
