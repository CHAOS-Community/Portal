using System.Collections.Generic;

namespace CHAOS.Portal.DTO.Standard
{
    public class PagedResult<TResultType> : IPagedResult<TResultType>
    {
        #region Properties

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

        public IEnumerable<TResultType> Results { get; set; }

        #endregion
        #region Construction

        public PagedResult( uint foundCount, uint startIndex, IEnumerable<TResultType> results )
        {
            Results = results;
            FoundCount = foundCount;
            StartIndex = startIndex;
        }
        
        #endregion
    }
}
