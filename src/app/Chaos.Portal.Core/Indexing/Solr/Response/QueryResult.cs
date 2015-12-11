namespace Chaos.Portal.Core.Indexing.Solr.Response
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class QueryResult<TReturnType> : IQueryResult<TReturnType> where TReturnType : IIndexResult, new()
    {
        #region Properties

        public string Value { get; private set; }
        public uint FoundCount { get; set; }
        public uint StartIndex { get; set; }
        public IEnumerable<TReturnType> Results { get; set; }

        #endregion
        #region Constructors

        public QueryResult(string value, XElement element) : this(element.Element("result").Elements("doc").Select(doc => (TReturnType)new TReturnType().Init(doc)))
        {
            var result = element.Element("result");
            Value      = value;
            FoundCount = uint.Parse(result.Attribute("numFound").Value);
            StartIndex = uint.Parse(result.Attribute("start").Value);
        }

        private QueryResult(IEnumerable<TReturnType> results)
        {
            Results = results;
        }

        #endregion
    }
}