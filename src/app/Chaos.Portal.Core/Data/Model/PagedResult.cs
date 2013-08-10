namespace Chaos.Portal.Core.Data.Model
{
    using System.Collections.Generic;

    public class PagedResult<TResultType> : IPagedResult<TResultType> where TResultType: IResult
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
            Results    = results;
            FoundCount = foundCount;
            StartIndex = startIndex;
        }
        
        #endregion
    }
}
