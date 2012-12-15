using System;
using Chaos.Portal.Standard;

namespace Chaos.Portal.Extension
{
    public class ExtensionAttribute : Attribute
    {
        #region Properties

	    public string Name { get; set; }
        public string ConfigurationName { get; set; }

        #endregion
        #region Constructors

        public ExtensionAttribute( string name = null, string configurationName = null )
        {
            Name              = name;
            ConfigurationName = configurationName;
        }

        #endregion
    }
}
