using System;

namespace CHAOS.Portal.Core.Standard
{
    public class PrettyNameAttribute : Attribute
    {
        #region Properties

        public string PrettyName { get; set; }

        #endregion
        #region Constructors

        public PrettyNameAttribute( string prettyName )
        {
			PrettyName = prettyName;
        }

        #endregion
    }
}
