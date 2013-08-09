namespace Chaos.Portal.Core.Response.Dto
{
    using System.Collections.Generic;

    using CHAOS.Serialization;

    using Chaos.Portal.Core.Data.Model;

    [Serialize("GroupedResult")]
    public class GroupedResult<TResult> : IResult where TResult : IResult
    {
        #region Properties

        [Serialize("Groups")]
        public IList<ResultGroup<TResult>> Groups { get; set; }

        #endregion
        #region Initialization

        public GroupedResult()
        {
            Groups = new List<ResultGroup<TResult>>();
        }

        #endregion

        #region Implementation of IResult

        public string Fullname { get { return "Chaos.Portal.Core.Response.Dto.GroupedResult"; } }

        #endregion
    }
}