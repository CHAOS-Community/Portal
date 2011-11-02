using System;
using System.Collections.Generic;
using System.Linq;
using Geckon.Portal.Core.Index;
using System.Text;
using System.Net;
using System.Xml.Linq;
using Geckon.Portal.Core.Standard.Index;

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

        #region IIndex

        public void Set( IEnumerable<IIndexable> items )
        {
           Set( items, true );
        }

        public void Set( IIndexable item )
        {
            Set( item, true );
        }

        public IEnumerable<Data.Result.IResult> Get( IQuery query )
        {
            SolrResponse result = SendRequest( Cores[0], HttpMethod.GET, "select", ((SolrQuery)query).SolrQueryString );

            return result.Result.Results;
        }

        #endregion

        public void Set( IEnumerable<IIndexable> items, bool doCommit )
        {
            foreach( SolrCoreConnection connection in Cores )
            {
                SendRequest( connection, HttpMethod.POST, "update", new XElement( "add", items.Select( item => ConvertToSolrDocument( item ) ) ) );
            }

            if( doCommit )
                Commit();
        }

        public void Set( IIndexable item, bool doCommit )
        {
            foreach( SolrCoreConnection connection in Cores )
            {
                SendRequest( connection, HttpMethod.POST, "update", new XElement( "add", ConvertToSolrDocument( item ) ) );
            }

            if( doCommit )
                Commit();
        }

        public void Commit()
        { 
            foreach( SolrCoreConnection connection in Cores )
            {
                SendRequest( connection, HttpMethod.POST, "update", new XElement( "commit" ) );
            }
        }

        public void AddCore( SolrCoreConnection connection )
        {
            Cores.Add( connection );
        }

        public void RemoveAll( bool doCommit )
        {
            // TODO: Probably a good idea to add some sort of permissions on this call
            foreach( SolrCoreConnection connection in Cores )
            {
                SendRequest( connection, HttpMethod.POST, "update", XElement.Parse( "<delete><query>*:*</query></delete>" ) );
            }

            if( doCommit )
                Commit();
        }

        private static SolrResponse SendRequest( SolrCoreConnection core, HttpMethod method, string command, string data)
        {
            HttpWebRequest request = null;

            switch( method )
            {
               case HttpMethod.GET:
                    request = (HttpWebRequest) WebRequest.Create( string.Format( "{0}/{1}?{2}", core.URL, command, data )  );

                    request.Method      = "GET";
                    request.ContentType = "text/xml";

                    break;
               case HttpMethod.POST:
                    request = (HttpWebRequest) WebRequest.Create( string.Format( "{0}/{1}", core.URL, command )  );

                    request.Method      = "POST";
                    request.ContentType = "text/xml";

                    using( System.IO.Stream stream = request.GetRequestStream() )
                    {
                        byte[] buffer = Encoding.Unicode.GetBytes( data ); 

                        stream.Write( buffer, 0, buffer.Length );
                    }

                    break;
               case HttpMethod.PUT:
                    throw new NotImplementedException();
               case HttpMethod.DELETE:
                    throw new NotImplementedException();
            }

            using( System.IO.StreamReader stream = new System.IO.StreamReader( request.GetResponse().GetResponseStream() ) )
            {
                return new SolrResponse( stream.ReadToEnd() );
            }
        }

        private static SolrResponse SendRequest(SolrCoreConnection core, HttpMethod method, string command, XElement data)
        {
            return SendRequest( core, method, command, new XDeclaration("1.0", "UTF-16", null ) + data.ToString( SaveOptions.DisableFormatting ) );
        }

        /// <summary>
        /// Convert an Indexable item to a solr document
        /// </summary>
        /// <param name="item">the object to index</param>
        /// <returns></returns>
        public static XElement ConvertToSolrDocument(IIndexable item)
        {
            // TODO: Look into using Geckon XML serializer to construct solr documents
            XElement doc = new XElement( "doc" );

            foreach( KeyValuePair<string,string> field in item.GetIndexableFields() )
            {
                doc.Add( new XElement( "field", new XAttribute( "name", field.Key ), field.Value ) );
            }

            return doc;
        }

        #endregion
    }
}
