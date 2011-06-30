using System;
using System.Collections.Generic;
using Geckon.Portal.Core.Extension;
using Geckon.Serialization.Xml;

namespace Geckon.Portal.Core.Standard.Extension
{
    public class Result : IResult
    {
        #region Fields

        private IList<XmlSerialize> _Content = new List<XmlSerialize>();

        #endregion
        #region Properties

        public IEnumerable<XmlSerialize> Content
        {
            get { return _Content; }
        }

        #endregion
        #region Business Logic

        public void Add( XmlSerialize obj )
        {
            _Content.Add( obj );
        }

        public void Add( IEnumerable<XmlSerialize> objs )
        {
            foreach( XmlSerialize xmlSerialize in objs )
            {
                _Content.Add( xmlSerialize );
            }
        }

        #endregion
    }
}
