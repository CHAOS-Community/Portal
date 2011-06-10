using System.Collections.Generic;
using Geckon.Portal.Solr.DTO;
namespace Geckon.Portal.Data.DTO
{
    public class SolrSchemaFieldChange
    {
        #region Constructor

        public SolrSchemaFieldChange(List<MetadataSchemaSolrAnnotation> fieldsAdded, List<MetadataSchemaSolrAnnotation> fieldsRemoved)
        {
            FieldsAdded = fieldsAdded;
            FieldsRemoved = fieldsRemoved;
        }

        #endregion
        #region Properties

        public List<MetadataSchemaSolrAnnotation> FieldsAdded { get; private set; }

        public List<MetadataSchemaSolrAnnotation> FieldsRemoved { get; private set; }

        #endregion
    }
}