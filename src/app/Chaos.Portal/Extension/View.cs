namespace Chaos.Portal.Extension
{
    using System.Collections.Generic;
    using System.Linq;

    using CHAOS.Serialization;

    using Chaos.Portal.Core.Data.Model;
    using Chaos.Portal.Indexing.Solr;

    public class View : AExtension
    {
        #region Initialization

        public override IExtension WithConfiguration( string configuration ) { return this; }

        #endregion
        
        public IPagedResult<IResult> Get(string view, IQuery query )
        {
            return ViewManager.Query(view, query);
        }
        
        public IEnumerable<IResult> List()
        {
            return ViewManager.LoadedViews.Select(loadedView => new ViewListItem{Name = loadedView.Name});
        }
    }

    public class ViewListItem : IResult
    {
        #region Implementation of IResult

        public string Fullname { get { return "Chaos.Portal.Extension.ViewInfo"; } }

        [Serialize]
        public string Name { get; set; }

        #endregion
    }
}
