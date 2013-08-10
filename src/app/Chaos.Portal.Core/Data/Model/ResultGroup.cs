namespace Chaos.Portal.Core.Data.Model
{
    using System.Collections.Generic;

    using CHAOS.Serialization;

    public interface IResultGroup<out TResult>where TResult : IResult
    {
        IEnumerable<TResult> Results { get; }

        string Value { get; }
        uint FoundCount{get;}
        uint StartIndex{get;}
    }

    [Serialize("ResultGroup")]
    public class ResultGroup<TResult> : PagedResult<TResult>, IResultGroup<TResult>
        where TResult : IResult
    {
        #region Properties

        [Serialize]
        public string Value { get; set; }

        #endregion
        #region Initialization

        public ResultGroup(uint foundCount, uint startIndex, IEnumerable<TResult> results) : base(foundCount, startIndex, results)
        {
        }

        public ResultGroup(uint foundCount, uint startIndex) : base(foundCount, startIndex, new List<TResult>())
        {
        }

        #endregion

        #region Implementation of IResult

        public string Fullname { get { return "Chaos.Portal.Core.Response.Dto.ResultGroup"; } }

        #endregion
    }
}