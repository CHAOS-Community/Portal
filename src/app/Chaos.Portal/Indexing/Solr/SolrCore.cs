namespace Chaos.Portal.Indexing.Solr
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    using CHAOS.Net;

    public class SolrCore : IIndex
    {
        #region Fields

        private readonly IHttpConnection _httpConnection;

        #endregion
        #region Construction

        public SolrCore(IHttpConnection httpConnection, string core)
        {
            _httpConnection = httpConnection;
            Core = core;
        }

        #endregion
        #region Properties

        public string Core { get; protected set; }

        #endregion
        #region Business Logic

        public Response<GuidResult> Query(IQuery query)
        {
            using (var stream = _httpConnection.Get(Core + "/select", query.ToString()))
            {
                var response = new Response<GuidResult>(stream);

                return response;
            }
        }

        public void Index(IIndexable indexable)
        {
            Index(new[] { indexable });
        }

        public void Index(IEnumerable<IIndexable> indexables)
        {
            var post = new XElement("add", indexables.Select(ConvertToSolrDocument));

            using (_httpConnection.Post(Core + "/update", post))
            {
                
            }

            Commit(isSoftCommit: true);
        }

        public void Commit(bool isSoftCommit = false)
        {
            var format = string.Format("<commit softCommit=\"{0}\"/>", isSoftCommit.ToString().ToLower());

            using (_httpConnection.Post(Core + "/update", XElement.Parse(format)))
            {
                
            }
        }

        public void Optimize()
        {
            var optimizeString = "<optimize/>";

            using (_httpConnection.Post(Core + "/update", XElement.Parse(optimizeString)))
            {
                
            }
        }

        /// <summary>
        /// Delete all documents in core
        /// </summary>
        public void Delete()
        {
            var deleteString = "<delete><query>*:*</query></delete>";

            using (_httpConnection.Post(Core + "/update", XElement.Parse(deleteString)))
            {
                
            }
        }

        /// <summary>
        /// Convert an Indexable item to a solr document
        /// </summary>
        /// <param name="item">the object to index</param>
        /// <returns></returns>
        public static XElement ConvertToSolrDocument(IIndexable item)
        {
            var doc = new XElement("doc");

            foreach (var field in item.GetIndexableFields())
            {
                doc.Add(new XElement("field", new XAttribute("name", field.Key), field.Value.Replace("<", " ")));
            }

            return doc;
        }

        #endregion
    }
}