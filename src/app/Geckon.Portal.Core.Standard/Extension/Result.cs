using System;
using System.Collections.Generic;
using Geckon.Data;
using System.Linq;
using Geckon.Portal.Core.Extension;
using Geckon.Serialization.Xml;
using System.Text;

namespace Geckon.Portal.Core.Standard.Extension
{
    public class Result : IResult
    {
        #region Fields

        private IDictionary<string, IModuleResult> _Content = new Dictionary<string, IModuleResult>();

        #endregion
        #region Properties

        private DateTime Timestamp { get; set; }

        public string Content
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                foreach( KeyValuePair<string, IModuleResult> moduleResult in _Content )
                {
                    sb.AppendFormat( "<{0}{1} Count=\"{2}\">", moduleResult.Key, 
                                                               GetAttributeString( moduleResult.Value.Attributes ),
                                                               moduleResult.Value.Elements.Count() );

                    foreach( XmlSerialize xmlSerialize in moduleResult.Value.Elements )
                    {
                        sb.Append( xmlSerialize.ToXML().OuterXml );
                    }

                    sb.AppendFormat( "</{0}>", moduleResult.Key );
                }

                return string.Format( "<PortalResult Duration=\"{0:F1}\">{1}</PortalResult>",
                                      DateTime.Now.Subtract( Timestamp ).TotalMilliseconds,
                                      sb.ToString() );
            }
        }

        private string GetAttributeString( IEnumerable<KeyValuePair<string, object>> nameValues )
        {
            StringBuilder sb = new StringBuilder();

            foreach( KeyValuePair<string, object> nameValue in nameValues )
            {
                sb.Append( String.Format( " {0}=\"{1}\"", nameValue.Key, nameValue.Value ) );
            }

            return sb.ToString();
        }

        #endregion
        #region Construction

        public Result(  )
        {
            Timestamp = DateTime.Now;
        }
        
        #endregion
        #region Business Logic

        public void Add( string moduleName, XmlSerialize obj, params KeyValuePair<string, object>[] attributes )
        {
            if( _Content.ContainsKey( moduleName ) )
            {
                _Content[ moduleName ].AddElement( obj );
                _Content[ moduleName ].AddAttribute( attributes );                
            }
            else
            {
                _Content.Add( moduleName, new ModuleResult() );
                _Content[ moduleName ].AddElement( obj );
                _Content[ moduleName ].AddAttribute( attributes );  
            }
        }

        public void Add( string moduleName, IEnumerable<XmlSerialize> objs, params KeyValuePair<string, object>[] attributes )
        {
            foreach( XmlSerialize xmlSerialize in objs )
            {
                Add( moduleName, xmlSerialize, attributes );
            }
        }

        #endregion
    }
}
