namespace Chaos.Portal.v5.Response.Dto
{
    using System.Collections.Generic;

    using CHAOS.Serialization;

    using Chaos.Portal.Core.Data.Model;

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
        void AddResult(IEnumerable<IResult> results);
        void AddResult(IResult result);
    }
}