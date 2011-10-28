using System;
using System.Collections.Generic;
using System.Linq;

namespace Geckon.Portal.Data.Result.Standard
{
    public class PagedResult : IPagedResult
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

        public IEnumerable<IResult> Results { get; set; }

        #endregion
        #region Construction

        public PagedResult( int foundCount, int startIndex, IEnumerable<IResult> results )
        {
            Results    = results;
            FoundCount = foundCount;
            StartIndex = startIndex;
        }
        
        #endregion
    }
}
