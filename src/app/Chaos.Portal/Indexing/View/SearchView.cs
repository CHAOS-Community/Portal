namespace Chaos.Portal.Indexing.View
{
    using System;
    using System.Collections.Generic;

    using CHAOS.Serialization;

    public class SearchView : IViewData
    {
        #region Properties

        public string Fullname
        {
            get
            {
                return "Chaos.Portal.Data.Dto.SearchView";
            }
        }

        [Serialize]
        public string Id { get; set; }

        [Serialize]
        public string Type { get; set; }

        [Serialize]
        public string Title { get; set; }

        [Serialize]
        public string Abstract { get; set; }

        [Serialize]
        public string Thumbnail { get; set; }
        
        [Serialize]
        public DateTime DateCreated { get; set; }

        [Serialize]
        public List<Option> Options { get; set; }

        public KeyValuePair<string, string> UniqueIdentifier { get { return new KeyValuePair<string, string>("Id", Id); } }

        #endregion
        #region Implementation of IIndexable

        public IEnumerable<KeyValuePair<string, string>> GetIndexableFields()
        {
            yield return UniqueIdentifier;
            yield return new KeyValuePair<string, string>("Title", Title);
            yield return new KeyValuePair<string, string>("Type", Type);
            yield return new KeyValuePair<string, string>("Abstract", Abstract);
            yield return new KeyValuePair<string, string>("DateCreated", DateCreated.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'"));
            // todo: refactor datetime tostring, create a helpermethod to convert a DateTime into the indexing format
        }

        #endregion
    }

    public class Option
    {
        [Serialize]
        public string Key { get; set; }
        
        [Serialize]
        public string Value { get; set; }

        public Option(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}