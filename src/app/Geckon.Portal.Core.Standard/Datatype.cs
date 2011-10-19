using System;

namespace Geckon.Portal.Core.Standard
{
    public class Datatype : Attribute, IDatatype
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

        #endregion
        #region Override

        public override string ToString()
        {
            return String.Format( "{0}:{1}", EventType, Event );
        }

        #endregion
    }
}
