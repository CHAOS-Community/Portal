using System;
using System.Collections.Generic;
using Geckon.Data;
using Geckon.Portal.Core.Extension;
using Geckon.Serialization.Xml;

namespace Geckon.Portal.Core.Standard.Extension
{
    public class ModuleResult : IModuleResult
    {
        #region Fields

        private IList<NameValue>    _Attributes;
        private IList<XmlSerialize> _Elements;

        #endregion
        #region Properties

        public IEnumerable<NameValue> Attributes
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
            _Attributes = new List<NameValue>();
            _Elements   = new List<XmlSerialize>();
        }

        #endregion
        #region Business Logic

        public void AddAttribute( NameValue nameValue )
        {
            _Attributes.Add( nameValue );
        }

        public void AddElement(XmlSerialize value)
        {
            _Elements.Add( value );
        }

        public void AddAttribute( NameValue[] attributes )
        {
            foreach( NameValue nameValue in attributes )
            {
                AddAttribute( nameValue );
            }
        }

        #endregion
    }
}
