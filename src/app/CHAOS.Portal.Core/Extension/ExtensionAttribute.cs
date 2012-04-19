using System;

namespace CHAOS.Portal.Core.Extension
{
    public class ExtensionAttribute : Attribute
    {
        #region Properties

        public string ExtensionName { get; set; }

        #endregion
        #region Constructors

        public ExtensionAttribute( string extensionName )
        {
            ExtensionName = extensionName;
        }

        #endregion
    }
}
