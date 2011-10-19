using System;

namespace Geckon.Portal.Core
{
    public interface IDatatype
    {
        string EventType { get; set; }
        string Event { get; set; }
    }
}
