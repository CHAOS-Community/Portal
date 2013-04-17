using System.Collections.Generic;
using CHAOS.Serialization;

namespace Chaos.Portal.Response
{
    using Chaos.Portal.Core.Data.Model;

    [Serialize("PortalResult")]
    public class PortalResult : IPortalResult
    {
        #region Fields

        private uint _totalCount;

        #endregion
        #region Properties

        [Serialize]
        public uint Count { get { return (uint) Results.Count; } }
        [Serialize]
        public uint TotalCount
        {
            get
            {
                return _totalCount == 0 ? (uint) Results.Count : _totalCount;
            } 
            set { _totalCount = value; }
        }
        [Serialize]
        public IList<IResult> Results { get; set; }

        #endregion
        #region Construction

        public PortalResult()
        {
            Results = new List<IResult>();
        }

        #endregion
        #region Business Logic



        #endregion
    }
}
