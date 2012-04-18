using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using CHAOS.Portal.Core.Extension;
using CHAOS.Portal.Core.Request;
using CHAOS.Portal.Core.Standard;
using CHAOS.Portal.DTO.Standard;
using CHAOS.Portal.Exception;
using Geckon.Index;
using Geckon.Index.Solr;
using Geckon.Serialization;

namespace CHAOS.Portal.Core.HttpModule
{
    public class PortalHttpModule : PortalApplication, IHttpModule
    {
        #region Construction

        public PortalHttpModule() : base( new Cache.Membase.Membase(), (IIndexManager) new SolrCoreManager<UUIDResult>() )
        {
            
        }


        #endregion
        #region IHttpModule Members

        public void Dispose()
        {
            //clean-up code here.
        }

        public void Init( HttpApplication context )
        {
            context.BeginRequest += ContextBeginRequest;
            
            // TODO: Add extension and module loading logic
            LoadedExtensions.Add( "Portal", new PortalExtensionLoader() );
            LoadedExtensions.Add( "Session", new DefaultExtentionLoader( "C:\\Users\\JesperFyhr\\Desktop\\Portal\\src\\app\\CHAOS.Portal.Web\\Extensions\\CHAOS.Portal.Extensions.dll", "CHAOS.Portal.Extensions.Session.SessionExtension" ) );
        }

        #endregion
        #region Business Logic

        private void ContextBeginRequest( object sender, EventArgs e )
        {
            HttpApplication application = (HttpApplication) sender;

            if( IsOnIgnoreList( application.Request.Url.AbsolutePath ) )
                return; // TODO: 404

            ICallContext callContext = CreateCallContext( application.Request );
            
            ProcessRequest( callContext );

            application.Response.ContentEncoding = System.Text.Encoding.Unicode;
            application.Response.ContentType     = GetContentType( callContext );

            using( System.IO.Stream inputStream  = callContext.GetResponseStream() )
            using( System.IO.Stream outputStream = application.Response.OutputStream )
            {
                inputStream.CopyTo( outputStream );
            }

            application.Response.End();
        }

        /// <summary>
        /// Get the http response content type based on the return format requested 
        /// </summary>
        /// <param name="callContext"></param>
        /// <returns></returns>
        private string GetContentType( ICallContext callContext )
        {
            // TODO: Should validate when request is received, not after it's done processing
            switch( callContext.ReturnFormat )
            {
                case ReturnFormat.GXML:
                    return "text/xml";
                case ReturnFormat.JSON:
                    return "application/json";
                case ReturnFormat.JSONP:
                    return "application/javascript";
                default:
                    throw new NotImplementedException( "Unknown return format" ); 
            }
        }

        /// <summary>
        /// Determine if the requested resource should be ignored
        /// </summary>
        /// <param name="absolutePath"></param>
        /// <returns></returns>
        private bool IsOnIgnoreList( string absolutePath )
        {
            if( absolutePath.EndsWith( "favicon.ico" ) )
                return true;

            // TODO: other resources that should be ignored

            return false;
        }

        /// <summary>
        /// Creates a CallContext based on the HttpRequest
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected ICallContext CreateCallContext( HttpRequest request )
        {
            string[] split = request.Url.AbsolutePath.Substring( request.ApplicationPath.Length ).Split('/');

            string extension = split[split.Length - 2];
            string action    = split[split.Length - 1];

            switch( request.HttpMethod )
            {
                case "DELETE":
                case "PUT":
                case "POST":
                    return new CallContext( this, new PortalRequest( extension, action, ConvertToIDictionary( request.Form ) ), new PortalResponse() );
                case "GET":
                    return new CallContext( this, new PortalRequest( extension, action, ConvertToIDictionary( request.QueryString ) ), new PortalResponse() );
                default:
                    throw new UnhandledException( "Unknown Http Method" );
            }
        }

        /// <summary>
        /// Converts a NameValueCollection to a IDictionary
        /// </summary>
        /// <param name="nameValueCollection"></param>
        /// <returns></returns>
        private IDictionary<string, string> ConvertToIDictionary( NameValueCollection nameValueCollection )
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>();
           
            // TODO: Put routing logic into seperate classes
            for( int i = 0; i < nameValueCollection.Keys.Count; i++ )
            {
                parameters.Add( nameValueCollection.Keys[i], nameValueCollection[i]);
            }

            return parameters;
        }

        #endregion
    }

    public class PortalExtensionLoader : IExtensionLoader
    {
        public IExtension CreateInstance()
        {
            return new PortalExtension();
        }
    }

    public class PortalExtension : IExtension
    {
        public void Test( ICallContext callContext )
        {
            callContext.PortalResponse.PortalResult.GetModule("Portal.Core").AddResult( new SimpleResult(callContext.PortalRequest.Extension + ":" + callContext.PortalRequest.Action ) );
        }

        public void Post( ICallContext callContext, int ID )
        {
            callContext.PortalResponse.PortalResult.GetModule("Portal.Core").AddResult( new SimpleResult( ID.ToString() ) );
        }
    }

    public class SimpleResult : Result
    {
        [Serialize]
        public string Result { get; set; }

        public SimpleResult( string result )
        {
            Result = result;
        }
    }
}
