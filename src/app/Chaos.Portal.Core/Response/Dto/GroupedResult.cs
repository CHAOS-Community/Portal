namespace Chaos.Portal.Core.Response.Dto
{
    using System.Collections.Generic;

    using CHAOS.Serialization;

    using Chaos.Portal.Core.Data.Model;

    public class GroupedResult<TResult> : IPortalResult where TResult : IResult
    {
        #region Properties

        [Serialize]
        public IList<ResultGroup<TResult>> Groups { get; set; }

        #endregion
        #region Initialization

        public GroupedResult()
        {
            Groups = new List<ResultGroup<TResult>>();
        }

        #endregion
    }
}