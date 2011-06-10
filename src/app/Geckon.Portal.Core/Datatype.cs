using System;

namespace Geckon.Portal.Core
{
    public class Datatype : Attribute
    {
        #region Properties

        public string EventType { get; set; }
        public string Event { get; set; }

        #endregion
        #region Construction

        public Datatype( string extensionName, string _Event )
        {
            EventType = extensionName;
            Event     = _Event;
        }

        public Datatype( string extensionName )
        {
            EventType = extensionName;
        }

        #endregion
    }
}
