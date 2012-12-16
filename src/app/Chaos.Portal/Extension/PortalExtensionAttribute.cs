using System;

namespace Chaos.Portal.Extension
{
    public class PortalExtensionAttribute : Attribute
    {
        #region Properties

	    public string Name { get; set; }
        public string ConfigurationName { get; set; }

        #endregion
        #region Constructors

        public PortalExtensionAttribute( string name = null, string configurationName = null )
        {
            Name              = name;
            ConfigurationName = configurationName;
        }

        #endregion
    }
}
