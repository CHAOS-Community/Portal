using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Geckon;
using Geckon.Index;
using Geckon.Serialization;

namespace CHAOS.Portal.DTO.Standard
{
    public class UUIDResult : Result, IIndexResult
    {
        #region Properties

        [Serialize("Guid")]
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

        public IIndexResult Init( XElement element )
        {
            Guid = new UUID( element.Elements("str").Where( node => node.Attribute("name").Value == "GUID" ).First().Value );

            return this;
        }
    }
}
