namespace Chaos.Portal.Core.Indexing.Solr.Request
{
	public class SolrQuery : IQuery
	{
		private string _facet;

		//private string _groupField;

		#region Properties

		public string Query { get; set; }
		public string Sort { get; set; }

		public string Facet
		{
			get { return _facet; }
			set
			{
				if (string.IsNullOrEmpty(value))
					_facet = "";
				else
				{
					var rangeStart = value.IndexOf("(range ");

					if (rangeStart != -1)
					{
						var rangeEnd = value.IndexOf(")", rangeStart);
						var rangeCommand = value.Substring(rangeStart, rangeEnd - rangeStart);
						var rangeParameters = rangeCommand.Split(' ');

						_facet =
							string.Format(
								"&facet.range={0}&f.{0}.facet.range.start={1}&f.{0}.facet.range.end={2}&f.{0}.facet.range.gap=%2B{3}",
								rangeParameters[1], rangeParameters[2], rangeParameters[3], rangeParameters[4]);

						value = value.Remove(rangeStart, rangeEnd - rangeStart + 1);
					}

					value = value.Replace(":", "=");
					value = value.Replace("field", "&facet.field");
					value = value.Replace("AND", "&");
					value = value.Replace("+", "");
					value = value.Replace(" ", "");
					_facet += value;
				}
			}
		}

		public string Filter { get; set; }

		public uint PageIndex { get; set; }
		public uint PageSize { get; set; }

		public string SolrQueryString
		{
			get
			{
				return string.Format("fl=Id,score&q={0}&sort={1}&start={2}&rows={3}&fq={4}&facet={5}{6}{7}",
				                     string.IsNullOrEmpty(Query) ? "*:*" : Query, Sort ?? "", PageIndex*PageSize, PageSize, Filter,
				                     (!string.IsNullOrEmpty(Facet)).ToString().ToLower(), Facet,
				                     Group != null ? Group.ToString() : "");
			}
		}

		public IQueryGroupSettings Group { get; set; }

		#endregion

		#region Construction

		public SolrQuery(string query, string facet, string sort, string filter, uint pageIndex, uint pageSize) : this()
		{
			Init(query, facet, sort, filter, pageIndex, pageSize);
		}

		public SolrQuery()
		{
			Group = new SolrGroup();
		}

		#endregion

		#region Business Logic

		public void Init(string query, string facet, string sort, string filter, uint pageIndex, uint pageSize)
		{
			WithQuery(query);
			Sort = sort;
			WithPageIndex(pageIndex);
			WithPageSize(pageSize);
			Facet = facet;
			Filter = filter;
		}

		public SolrQuery WithQuery(string q)
		{
			Query = q;

			return this;
		}

		public SolrQuery WithPageIndex(uint pageIndex)
		{
			PageIndex = pageIndex;

			return this;
		}

		public SolrQuery WithPageSize(uint pageSize)
		{
			PageSize = pageSize;

			return this;
		}

		public override string ToString()
		{
			return SolrQueryString;
		}

		#endregion
	}
}