using System.Collections.Generic;
using CHAOS.Portal.DTO;

namespace CHAOS.Portal.Core
{
    public interface IModuleResult
    {
        string Fullname { get; }
        long Duration { get; }
        int Count { get; }
        int? PageIndex { get; set; }
        int? TotalPages
        {
            get;
            set;
        }
        int? TotalCount
        {
            get;
            set;
        }
        IList<IResult> Results { get; set; }
        void AddResult( IEnumerable<IResult> results );
        void AddResult( IResult result );
    }
}
