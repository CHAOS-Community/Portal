namespace Chaos.Portal.Core.Indexing.Solr.Request
{
    using System.Text;

    public class SolrGroup : IQueryGroupSettings
    {
        public string Field { get; set; }

        public uint Limit { get; set; }
        public uint Offset { get; set; }

        public bool IsGroupingEnabled 
        { 
            get
            {
                return !string.IsNullOrEmpty(Field) && Limit != 0;
            } 
        }

        public override string ToString()
        {
            var query = new StringBuilder();

            query.AppendFormat("&group={0}", IsGroupingEnabled.ToString().ToLower());
            query.AppendFormat("&group.limit={0}", Limit);
            query.AppendFormat("&group.field={0}", Field);
            query.AppendFormat("&group.offset={0}", Offset);
            
            return query.ToString();
        }
    }
}