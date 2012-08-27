using System;
using CHAOS.Portal.Core.Standard;

namespace CHAOS.Portal.Core.Module
{
    public class ModuleAttribute : PrettyNameAttribute
    {
        #region Properties

	    public string ModuleConfigName
	    {
		    get { return PrettyName; }
	    }

        #endregion
        #region Constructors

        public ModuleAttribute( string moduleConfigName ) :base( moduleConfigName )
        {

        }

        #endregion
    }
}
