namespace Chaos.Portal.Core.Data.Model
{
    using System.Collections.Generic;
    using System.Linq;

    using CHAOS.Serialization;

    public class PagedResult<TResultType> : IPagedResult<TResultType> where TResultType: IResult
    {
        #region Properties

        [Serialize]
        public uint Count
        {
            get
            {
                return (uint)Results.Count();
            }
        }

        [Serialize("TotalCount")]
        public uint FoundCount
        {
            get;
            set;
        }

        public uint StartIndex
        {
            get;
            set;
        }

        // TODO: refactor out the use of IEnumerable to an IList
        [Serialize]
        public IEnumerable<TResultType> Results { get; set; }

        #endregion
        #region Construction

        public PagedResult( uint foundCount, uint startIndex, IEnumerable<TResultType> results )
        {
            Results    = results;
            FoundCount = foundCount;
            StartIndex = startIndex;
        }
        
        #endregion
    }
}
