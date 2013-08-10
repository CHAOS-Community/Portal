namespace Chaos.Portal.Core.Data.Model
{
    using System.Collections.Generic;

    using CHAOS.Serialization;

    public interface IGroupedResult<out TResult> where TResult : IResult
    {
        IEnumerable<IResultGroup<TResult>> Groups { get; }
    }

    [Serialize("GroupedResult")]
    public class GroupedResult<TResult> : IGroupedResult<TResult>where TResult : IResult
    {
        #region Properties

        [Serialize("Groups")]
        public IEnumerable<IResultGroup<TResult>> Groups { get; set; }

        #endregion
        #region Initialization

        public GroupedResult() : this(new List<IResultGroup<TResult>>())
        {            
        }
        
        public GroupedResult(IEnumerable<IResultGroup<TResult>> groups)
        {
            Groups = groups;
        }

        #endregion

        #region Implementation of IResult

        public string Fullname { get { return "Chaos.Portal.Core.Response.Dto.GroupedResult"; } }

        #endregion
    }
}