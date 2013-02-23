namespace Chaos.Portal.Extension
{
    using System.Collections.Generic;

    using Chaos.Portal.Data.Dto;
    using Chaos.Portal.Indexing.Solr;

    public class View : AExtension
    {
        #region Initialization

        public override IExtension WithConfiguration( string configuration ) { return this; }

        #endregion
        #region Get

        public IEnumerable<IResult> Get( ICallContext callContext, string view, IQuery query )
        {
            return callContext.ViewManager.Query(view, query);
        }

        #endregion
    }
}
