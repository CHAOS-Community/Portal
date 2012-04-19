using System;

namespace CHAOS.Portal.Core.Module
{
    public class Datatype : Attribute
    {
        #region Properties

        public string ExtensionName { get; set; }
        public string ActionName { get; set; }

        #endregion
        #region Construction

        public Datatype( string extensionName, string actionName )
        {
            ExtensionName = extensionName;
            ActionName    = actionName;
        }

        #endregion
        #region Override

        public override string ToString()
        {
            return String.Format( "{0}:{1}", ExtensionName, ActionName );
        }

        #endregion
    }
}
