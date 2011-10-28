using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Geckon.Portal.Data.Result;
using Geckon.Portal.Data.Result.Standard;
using Geckon.Portal.Data;

namespace Geckon.Portal.Core.Standard
{
    public class SolrResponse
    {
        #region Properties

        public int Duration { get; set; }
        public int Status { get; set; }
        public IPagedResult Result{ get; set; }

        #endregion
        #region Construction

        public SolrResponse( string responseData )
        {
            XDocument xdoc = XDocument.Parse( responseData );

            Duration = int.Parse( xdoc.Descendants( "int" ).Where( node => node.Attributes( "name" ).Where( attribute => attribute.Value == "QTime" ) != null ).First().Value );
            Status   = int.Parse( xdoc.Descendants( "int" ).Where( node => node.Attributes( "name" ).Where( attribute => attribute.Value == "status" ) != null ).First().Value );

            if(  xdoc.Descendants( "result" ).FirstOrDefault() != null )
                Result = new PagedResult( int.Parse( xdoc.Root.Elements( "result" ).Attributes( "numFound" ).First().Value ),
                                          int.Parse( xdoc.Root.Elements( "result" ).Attributes( "start" ).First().Value ),
                                          xdoc.Root.Elements("result").First().Elements("doc").Select( doc => new GuidResult( doc.Elements("str").Where( element => element.Attribute("name").Value == "guid" ).First().Value ) ) );
        }

        #endregion
    }
}
