using System;
using System.Collections.Generic;
using System.Linq;

namespace Geckon.Portal.Data
{
    public class GuidResult : Result.Standard.Result
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
    }
}
