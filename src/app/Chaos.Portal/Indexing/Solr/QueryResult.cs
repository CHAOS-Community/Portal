namespace Chaos.Portal.Indexing.Solr
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public class QueryResult<TReturnType> where TReturnType : IIndexResult, new()
    {
        #region Properties

        public uint FoundCount { get; set; }
        public uint StartIndex { get; set; }
        public IEnumerable<TReturnType> Results { get; set; }

        #endregion
        #region Constructors

        public QueryResult() : this(new List<TReturnType>())
        {

        }

        public QueryResult(XElement element) : this(element.Elements("doc").Select(doc => (TReturnType)new TReturnType().Init(doc)))
        {
            this.FoundCount = uint.Parse(element.Attribute("numFound").Value);
            this.StartIndex = uint.Parse(element.Attribute("start").Value);
        }

        private QueryResult(IEnumerable<TReturnType> results)
        {
            this.Results = results;
        }

        #endregion
    }
}
