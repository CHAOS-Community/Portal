namespace Chaos.Portal.Core.Response.Dto.v2
{
    using System.Collections.Generic;

    using CHAOS.Serialization;

    using Chaos.Portal.Core.Data.Model;

    [Serialize("ResultGroup")]
    public class ResultGroup<TResult> : PagedResult<TResult> where TResult : IResult
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