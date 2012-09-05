using CHAOS.Portal.Core.Standard;

namespace CHAOS.Portal.Core.Extension
{
    public class ExtensionAttribute : PrettyNameAttribute
    {
        #region Properties

	    public string ExtensionName
	    {
		    get { return PrettyName; }
			set { PrettyName = value; }
	    }

        #endregion
        #region Constructors

        public ExtensionAttribute( string extensionName ) : base( extensionName )
        {
            ExtensionName = extensionName;
        }

        #endregion
    }
}
