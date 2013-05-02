namespace Chaos.Portal.Core.Response.Dto
{
    using System.Collections.Generic;

    using Chaos.Portal.Core.Data.Model;

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
            Results = results;
            FoundCount = foundCount;
            StartIndex = startIndex;
        }
        
        #endregion
    }
}
