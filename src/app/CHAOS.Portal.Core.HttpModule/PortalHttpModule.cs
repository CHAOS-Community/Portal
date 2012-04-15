using System;
using System.Collections.Generic;
using System.Web;
using CHAOS.Portal.Core.Extension;
using CHAOS.Portal.Core.Request;
using CHAOS.Portal.Core.Standard;
using CHAOS.Portal.DTO.Standard;
using Geckon.Serialization;

namespace CHAOS.Portal.Core.HttpModule
{
    public class PortalHttpModule : PortalApplication, IHttpModule
    {
        #region IHttpModule Members

        public void Dispose()
        {
            //clean-up code here.
        }

        public void Init( HttpApplication context )
        {
            context.BeginRequest += ContextBeginRequest;

            LoadedExtensions.Add( "Portal", new PortalExtensionLoader() );
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

        private string GetContentType( ICallContext callContext )
        {
            switch( callContext.ReturnFormat )
            {
                case ReturnFormat.GXML:
                    return "text/xml";
                case ReturnFormat.JSON:
                    return "application/json";
                case ReturnFormat.JSONP:
                    return "application/javascript";
                default:
                    throw new NotImplementedException( "Unknown return format" ); // TODO: Should validate when request is received, not after it's done processing
            }
        }

        private bool IsOnIgnoreList( string absolutePath )
        {
            if( absolutePath.EndsWith( "favicon.ico" ) )
                return true;

            // TODO: other resources that should be ignored

            return false;
        }

        protected ICallContext CreateCallContext( HttpRequest request )
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            string[] split = request.Url.AbsolutePath.Substring( request.ApplicationPath.Length ).Split( '/' );

            for( int i = 0; i < request.QueryString.Keys.Count; i++ )
            {
                parameters.Add( request.QueryString.Keys[i], request.QueryString[i] );
            }

            return new CallContext( this, new PortalRequest( split[split.Length-2], split[split.Length-1], parameters ), new PortalResponse(  ) );
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
