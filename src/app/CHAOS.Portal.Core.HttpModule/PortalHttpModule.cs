using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Web;
using CHAOS.Extensions;
using CHAOS.Index;
using CHAOS.Index.Solr;
using CHAOS.Portal.Core.Module;
using CHAOS.Portal.Core.Request;
using CHAOS.Portal.Core.Standard;
using CHAOS.Portal.DTO.Standard;
using CHAOS.Portal.Data.EF;
using CHAOS.Portal.Exception;
using CHAOS.Portal.Core.Extension;

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
            // REVIEW: Look into moving the loading process out of the http module
            LoadExtensions();
            LoadModules();

            context.BeginRequest += ContextBeginRequest;
        }

        private void LoadModules()
        {
            using( var db = new PortalEntities() )
            {
                // Load modules
                foreach( string file in System.IO.Directory.GetFiles( string.Format( "{0}\\Modules", ServiceDirectory ), "*.dll" ) )
                {
                    Assembly assembly = Assembly.LoadFile( file );

                    // Get the types and identify the IModules
                    foreach( Type type in assembly.GetTypes() )
                    {
                        if( !type.Implements<IModule>() )
                            continue;
                    
                        var attribute = type.GetCustomAttribute<ModuleAttribute>( true );
                        var module    = (IModule) assembly.CreateInstance( type.FullName );

                        // If an attribute is present on the class, load config from database
                        if( !attribute.IsNull() )
                        {
                            var moduleConfig = db.Module_Get( null, attribute.ModuleConfigName ).FirstOrDefault();

                            if( moduleConfig == null )
                                throw new ModuleConfigurationMissingException( string.Format( "The module requires a configuration, but none was found with the name: {0}", attribute.ModuleConfigName ) );

                            module.Initialize( moduleConfig.Configuration );
                        }

                        // Index modules by the Extensions they subscribe to
                        foreach( MethodInfo method in module.GetType().GetMethods() )
                        {
                            foreach( Datatype datatypeAttribute in method.GetCustomAttributes( typeof( Datatype ), true ) )
                            {
                                if( !LoadedModules.ContainsKey( datatypeAttribute.ExtensionName ) )
                                    LoadedModules.Add( datatypeAttribute.ExtensionName, new Collection<IModule>() );

                                if( !LoadedModules[ datatypeAttribute.ExtensionName ].Contains( module ) )
                                    LoadedModules[ datatypeAttribute.ExtensionName ].Add( module );
                            }
                        }
                    }
                }
            }
        }

        private void LoadExtensions()
        {
            foreach( string file in System.IO.Directory.GetFiles( string.Format( "{0}\\Extensions", ServiceDirectory ), "*.dll" ) )
            {
                Assembly assembly = Assembly.LoadFile( file );

                foreach( Type type in assembly.GetTypes() )
                {
                    if( !type.Implements<IExtension>() )
                        continue;
                    
                    var attribute = type.GetCustomAttribute<ExtensionAttribute>( true );

                    LoadedExtensions.Add( attribute.IsNull() ? type.Name : attribute.ExtensionName,
                                          (IExtension) assembly.CreateInstance( type.FullName ) );
                }
            }
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
                case ReturnFormat.XML:
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
}
