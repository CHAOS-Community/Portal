namespace Chaos.Portal.Core.Indexing.View
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Cache;
	using Exceptions;

	/// <summary>
	/// The view manager.
	/// </summary>
	public class ViewManager : IViewManager
	{
		#region Fields

		/// <summary>
		/// The _loaded views.
		/// </summary>
		private readonly IDictionary<string, IView> _loadedViews;

		private readonly ICache Cache;

		#endregion

		#region Initialization

		/// <summary>
		/// Initializes a new instance of the <see cref="ViewManager"/> class.
		/// </summary>
		/// <param name="dictionary">The dictionary.</param>
		public ViewManager(IDictionary<string, IView> dictionary, ICache cache)
		{
			_loadedViews = dictionary;
			Cache = cache;
		}

		#endregion

		#region Properties

		public IEnumerable<IView> LoadedViews
		{
			get { return _loadedViews.Select(item => item.Value); }
		}

		#endregion

		#region Business Logic

		/// <summary>
		/// The index.
		/// </summary>
		/// <param name="obj">The object.</param>
		/// <returns>The IndexReport of the index process</returns>
		public void Index(object obj)
		{
			Index(new[] {obj});
		}

		/// <summary>
		/// The index.
		/// </summary>
		/// <param name="objs">The objects.</param>
		public void Index(IEnumerable<object> objs)
		{
			const int pageSize = 1000;
			
			for (var pageIndex = 0; true; pageIndex++)
			{
				var page = objs.Skip((pageIndex) * pageSize).Take(pageSize).Take(pageSize).ToList();

				foreach (var view in _loadedViews.Values)
					view.Index(page);

				if(page.Count < pageSize)
					break;
			}
		}

		public IView GetView(string viewName)
		{
			if (!_loadedViews.ContainsKey(viewName))
				throw new ViewNotLoadedException(string.Format("No key with name: '{0}' has been loaded", viewName));

			return _loadedViews[viewName];
		}

		/// <summary>
		/// Sends a delete all query to all Views, essentially cleaning the entire index
		/// </summary>
		public void Delete()
		{
			foreach (var view in LoadedViews)
			{
				view.Delete();
			}
		}

		/// <summary>
		/// Sends a delete query to all Views
		/// </summary>
		/// <param name="query"></param>
		public void Delete(string query)
		{
			foreach (var view in LoadedViews)
			{
				view.Delete(query);
			}
		}

		public void AddView(IView view, bool force = false)
		{
			if (view == null) throw new NullReferenceException("Cannot load a null view");
			if (string.IsNullOrEmpty(view.Name)) throw new ArgumentException("View.Name cannot be null");
			if (_loadedViews.ContainsKey(view.Name))
			{
				if (!force) throw new DuplicateViewException("Key already added: " + view.Name);

				_loadedViews[view.Name] = view;
			}
			else
				_loadedViews.Add(view.Name, view);
		}

		#endregion
	}
}