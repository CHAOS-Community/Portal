using System.Collections.Generic;

namespace CHAOS.Portal.DTO
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
