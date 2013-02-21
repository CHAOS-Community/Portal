﻿namespace Chaos.Portal.Indexing.Solr
{
    public class SolrQuery : IQuery
    {
        private string _facet;

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
                    _facet = value.Replace(":", "=");
                    _facet = _facet.Replace("field", "facet.field");
                    _facet = _facet.Replace("AND", "&");
                    _facet = _facet.Replace("+", "");
                    _facet = _facet.Replace(" ", "");
                }

            }
        }

        public uint PageIndex { get; set; }
        public uint PageSize { get; set; }

        public string SolrQueryString
        {
            get
            {
                return string.Format("q={0}&sort={1}&start={2}&rows={3}&facet={4}&{5}", Query, Sort ?? "", PageIndex * PageSize, PageSize, (!string.IsNullOrEmpty(Facet)).ToString().ToLower(), Facet);
            }
        }

        #endregion
        #region Construction

        public SolrQuery(string query, string facet, string sort, uint pageIndex, uint pageSize)
        {
            Init(query, facet, sort, pageIndex, pageSize);
        }

        public SolrQuery()
        {
        }

        #endregion
        #region Business Logic

        public void Init(string query, string facet, string sort, uint pageIndex, uint pageSize)
        {
            WithQuery(query);
            Sort = sort;
            WithPageIndex(pageIndex);
            WithPageSize(pageSize);
            Facet = facet;
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
        
        #endregion
    }
}
