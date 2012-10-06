using System.Collections.Generic;
using CHAOS.Serialization;

namespace CHAOS.Portal.DTO
{
    [Serialize("ModuleResult")]
    public interface IModuleResult
    {
        string Fullname { get; }
        long Duration { get; }
        uint Count { get; }
        uint? PageIndex { get; set; }
        uint? TotalPages
        {
            get;
            set;
        }
        uint? TotalCount
        {
            get;
            set;
        }
        IList<IResult> Results { get; set; }
        void AddResult( IEnumerable<IResult> results );
        void AddResult( IResult result );
    }
}
