namespace Chaos.Portal.Core.Indexing.Solr
{
    public class Facet : IFacet
    {
        #region Properties

        public string DataType { get; set; }
        public string Value { get; set; }
        public uint Count { get; set; }

        #endregion
        #region Constructors

        public Facet(string dataType, string value, uint count)
        {
            this.DataType = dataType;
            this.Value = value;
            this.Count = count;
        }

        #endregion
    }
}