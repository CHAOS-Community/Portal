namespace Chaos.Portal.Core.Data.Model
{
    using System.Collections.Generic;

    using CHAOS.Serialization;

    using Chaos.Portal.Core.Response;

    [Serialize]
    public class QueryResult : IPortalResult
    {
        #region Initialization

        public QueryResult()
        {
        }

        #endregion
        #region Properties
        
        [Serialize]
        public IList<IResultGroup<IResult>> Groups { get; set; }

        [Serialize]
        public IList<FieldFacet> FieldFacets { get; set; }

        #endregion
    }
}