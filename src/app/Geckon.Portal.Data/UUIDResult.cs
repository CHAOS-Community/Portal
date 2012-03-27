using System;
using System.Collections.Generic;
using System.Linq;
using Geckon.Index;

namespace Geckon.Portal.Data
{
    public class UUIDResult : Result.Standard.Result, IIndexResult
    {
        #region Properties

        [Serialization.Serialize("Guid")]
        public UUID Guid { get; set; }

        #endregion
        #region Construction

        public UUIDResult()
        {

        }

		public UUIDResult(string guid)
        {
            Guid = new UUID( guid );
        }

        #endregion

        public IIndexResult Init( System.Xml.Linq.XElement element )
        {
            Guid = new UUID( element.Elements("str").Where( node => node.Attribute("name").Value == "GUID" ).First().Value );

            return this;
        }
    }
}
