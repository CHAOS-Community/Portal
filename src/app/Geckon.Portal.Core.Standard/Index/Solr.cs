using System;
using System.Collections.Generic;
using System.Linq;
using Geckon.Portal.Core.Index;
using System.Text;
using System.Net;
using System.Xml.Linq;

namespace Geckon.Portal.Core.Standard
{
    public class Solr : IIndex
    {
        #region Properties

        public IList<SolrCoreConnection> Cores { get; set; }

        #endregion
        #region Construction

        public Solr()
        {
            Cores = new List<SolrCoreConnection>();
        }

        #endregion
        #region Business Logic

        public void Set( IEnumerable<IIndexable> items )
        {
            foreach( IIndexable item in items )
            {
                // TODO: Can be improved by just sending one Add call to solr
                Set( item );
            }
        }

        public void Set( IIndexable item )
        {
            foreach( SolrCoreConnection connection in Cores )
            {
                SendRequest( connection, "POST", "<add>" + ConvertToSolrDocument( item ) + "</add>" );
                SendRequest( connection, "POST", "<commit/>" );
            }
        }

        public IEnumerable<Data.Result.IResult> Get( IQuery query )
        {
            throw new NotImplementedException();
        }

        public void AddCore( SolrCoreConnection connection )
        {
            Cores.Add( connection );
        }


        public void RemoveAll()
        {
            // TODO: Probably a good idea to add some sort of permissions on this call
            string deleteAll = "<delete><query>*:*</query></delete>";
        }

        private void SendRequest( SolrCoreConnection core, string method, string data )
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create( string.Format( "{0}/{1}", core.URL, "update" )  );

            request.Method      = method;
            request.ContentType = "text/xml";

            using( System.IO.Stream stream = request.GetRequestStream() )
            {
                byte[] buffer = System.Text.Encoding.Unicode.GetBytes( "<?xml version=\"1.0\" encoding=\"utf-16\"?>" + data ); 

                stream.Write( buffer, 0, buffer.Length );

                stream.Flush();
                stream.Close();
            }

            WebResponse response = request.GetResponse();

            using( System.IO.StreamReader stream = new System.IO.StreamReader( response.GetResponseStream() ) )
            {
                string s = stream.ReadToEnd();
            }
        }

        /// <summary>
        /// Convert an Indexable item to a solr document
        /// </summary>
        /// <param name="item">the object to index</param>
        /// <returns></returns>
        public static string ConvertToSolrDocument( IIndexable item )
        {
            // TODO: Look into using Geckon XML serializer to construct solr documents
            StringBuilder sb = new StringBuilder();

            sb.Append( "<doc>" );

            foreach( KeyValuePair<string,string> field in item.GetIndexableFields() )
            {
                sb.Append( string.Format( "<field name=\"{0}\">{1}</field>",field.Key, field.Value ) );
            }

            sb.Append( "</doc>" );

            return sb.ToString();
        }

        #endregion
    }
}
