using System.Collections.Generic;

namespace CHAOS.Portal.DTO.Standard
{
    public class PagedResult<TResultType> : IPagedResult<TResultType>
    {
        #region Properties

        public int FoundCount
        {
            get;
            set;
        }

        public int StartIndex
        {
            get;
            set;
        }

        public IEnumerable<TResultType> Results { get; set; }

        #endregion
        #region Construction

        public PagedResult( int foundCount, int startIndex, IEnumerable<TResultType> results )
        {
            Results = results;
            FoundCount = foundCount;
            StartIndex = startIndex;
        }
        
        #endregion
    }
}
