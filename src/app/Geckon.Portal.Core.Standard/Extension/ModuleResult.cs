using System.Collections.Generic;
using Geckon.Portal.Core.Extension;
using Geckon.Serialization.Xml;

namespace Geckon.Portal.Core.Standard.Extension
{
    public class ModuleResult : IModuleResult
    {
        #region Fields

        private IList<KeyValuePair<string, object>> _Attributes;
        private IList<XmlSerialize>                   _Elements;

        #endregion
        #region Properties

        public IEnumerable<KeyValuePair<string, object>> Attributes
        {
            get { return _Attributes; }
        }
        
        public IEnumerable<XmlSerialize> Elements
        {
            get { return _Elements; }
        }

        #endregion
        #region Construction

        public ModuleResult()
        {
            _Attributes = new List<KeyValuePair<string, object>>();
            _Elements   = new List<XmlSerialize>();
        }

        #endregion
        #region Business Logic

        public void AddAttribute( KeyValuePair<string, object> nameValue )
        {
            _Attributes.Add( nameValue );
        }

        public void AddElement(XmlSerialize value)
        {
            _Elements.Add( value );
        }

        public void AddAttribute( params KeyValuePair<string, object>[] attributes )
        {
            foreach( KeyValuePair<string, object> nameValue in attributes )
            {
                AddAttribute( nameValue );
            }
        }

        #endregion
    }
}
