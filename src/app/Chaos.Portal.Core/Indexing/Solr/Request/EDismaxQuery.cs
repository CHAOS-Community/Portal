using System.Collections.Generic;
using System.Linq;
using CHAOS.Parsing.XML.Schema.Data.SimpleTypeContents;

namespace Chaos.Portal.Core.Indexing.Solr.Request
{
	public class EDismaxQuery : IQuery
	{
		private string _facet;
		private string _query;

		public string Query
		{
			get { return _query; }
			set { _query = value.Trim(); }
		}

		public string Sort { get; set; }
		public string Filter { get; set; }
		public uint PageIndex { get; set; }
		public uint PageSize { get; set; }
		public string QueryFields { get; set; }
		public IList<string> BoostQuery { get; set; }

		public EDismaxQuery()
		{
			Query = "";
			Sort = "";
			Filter = "";
			QueryFields = "";
			BoostQuery = new List<string>();
		}

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

		public string SolrQueryString
		{
			get
			{
				var boostQuery = BoostQuery.Aggregate("", (current, bq) => current + ("&bq=" + bq));

				return string.Format("fl=Id,score&q={0}&sort={1}&start={2}&rows={3}&fq={4}&qf={5}{6}&defType=edismax&facet={7}{8}{9}",
				                     string.IsNullOrEmpty(Query) ? "*:*" : Query, 
														 Sort ?? "", 
														 PageIndex*PageSize, 
														 PageSize, 
														 Filter,
														 QueryFields,
														 boostQuery,
				                     (!string.IsNullOrEmpty(Facet)).ToString().ToLower(), 
														 Facet,
				                     Group != null ? Group.ToString() : "");
			}
		}

		public IQueryGroupSettings Group { get; set; }

		public void Init(string query, string facet, string sort, string filter, uint pageIndex, uint pageSize)
		{
			Query = query;
			Sort = sort;
			PageIndex = pageIndex;
			PageSize = pageSize;
			Facet = facet;
			Filter = filter;
		}

		public override string ToString()
		{
			return SolrQueryString;
		}
	}
}