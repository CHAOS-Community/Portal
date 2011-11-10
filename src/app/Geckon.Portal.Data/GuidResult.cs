using System;
using System.Collections.Generic;
using System.Linq;
using Geckon.Index;

namespace Geckon.Portal.Data
{
    public class GuidResult : Result.Standard.Result, IIndexResult
    {
        #region Properties

        [Serialization.Serialize("Guid")]
        public Guid Guid { get; set; }

        #endregion
        #region Construction

        public GuidResult()
        {

        }

        public GuidResult( string guid )
        {
            Guid = Guid.Parse( guid );
        }

        #endregion

        public IIndexResult Init( System.Xml.Linq.XElement element )
        {
            Guid = Guid.Parse( element.Elements("str").Where( node => node.Attribute("name").Value == "GUID" ).First().Value );

            return this;
        }
    }
}
