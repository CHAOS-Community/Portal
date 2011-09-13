using System.Collections.Generic;

namespace Geckon.Portal.Data.Result
{
    public interface IModuleResult
    {
        string Fullname { get; }
        long Duration { get; }
        int Count { get; }
        IList<IResult> Results { get; set; }
        void AddResult( IEnumerable<IResult> results );
        void AddResult( IResult result );
    }
}
