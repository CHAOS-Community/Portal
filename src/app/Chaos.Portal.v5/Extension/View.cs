namespace Chaos.Portal.v5.Extension
{
    using System.Collections.Generic;
    using System.Linq;

    using CHAOS.Serialization;

    using Chaos.Portal.Core;
    using Chaos.Portal.Core.Data.Model;
    using Chaos.Portal.Core.Extension;
    using Chaos.Portal.Core.Indexing;

    public class View : AExtension
    {
        #region Initialization

        public View(IPortalApplication portalApplication): base(portalApplication)
        {
        }

        #endregion
        public IPagedResult<IResult> Get(string view, IQuery query )
        {
            return ViewManager.GetView(view).Query(query);
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
