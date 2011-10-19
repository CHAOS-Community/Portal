using System;

namespace Geckon.Portal.Core
{
    public interface IParameter
    {
        string ParameterName { get; set; }
        object Value { get; set; }
        Type Type { get; set; }
    }
}
