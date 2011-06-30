using System;
using System.Collections.Generic;
using Geckon.Portal.Core.Extension;
using Geckon.Serialization.Xml;
using System.Text;

namespace Geckon.Portal.Core.Standard.Extension
{
    public class Result : IResult
    {
        #region Fields

        private IDictionary<string, IList<XmlSerialize>> _Content = new Dictionary<string, IList<XmlSerialize>>();

        #endregion
        #region Properties

        public string Content
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                foreach( KeyValuePair<string, IList<XmlSerialize>> keyValuePair in _Content )
                {
                    sb.AppendFormat( "<{0}>", keyValuePair.Key );

                    foreach( XmlSerialize xmlSerialize in keyValuePair.Value )
                    {
                        sb.Append( xmlSerialize.ToXML().OuterXml );
                    }

                    sb.AppendFormat( "</{0}>", keyValuePair.Key);
                }

                return sb.ToString();
            }
        }

        #endregion
        #region Business Logic

        public void Add( string moduleName, XmlSerialize obj )
        {
            if( _Content.ContainsKey( moduleName ) )
                _Content[ moduleName ].Add( obj );
            else
            {
                _Content.Add( moduleName, new List<XmlSerialize>() );
                _Content[ moduleName ].Add( obj );
            }
        }

        public void Add( string moduleName, IEnumerable<XmlSerialize> objs )
        {
            foreach( XmlSerialize xmlSerialize in objs )
            {
                Add( moduleName, xmlSerialize );
            }
        }

        #endregion
    }
}
