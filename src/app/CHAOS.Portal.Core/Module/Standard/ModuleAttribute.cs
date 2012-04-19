using System;

namespace CHAOS.Portal.Core.Module.Standard
{
    public class ModuleAttribute : Attribute
    {
        #region Properties

        public string ModuleConfigName { get; set; }

        #endregion
        #region Constructors

        public ModuleAttribute( string moduleConfigName )
        {
            ModuleConfigName = moduleConfigName;
        }

        #endregion
    }
}
