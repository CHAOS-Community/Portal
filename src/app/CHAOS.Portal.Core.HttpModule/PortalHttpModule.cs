using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml.Linq;
using CHAOS.Extensions;
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
    public class PortalHttpModule : IHttpModule
    {
        #region Properties

        protected PortalApplication PortalApplication { get; set; }

        #endregion
        #region IHttpModule Members

        public void Dispose()
        {
            //clean-up code here.
        }

        public void Init( HttpApplication context )
        {
            // REVIEW: Look into moving the loading process out of the http module
            if( context.Application["PortalApplication"] == null )
            {
                lock( context.Application )
                {
                    if( context.Application["PortalApplication"] == null )
                    {
                        var application = new PortalApplication( new Cache.Membase.Membase(), new SolrCoreManager<UUIDResult>() );

                        context.Application["PortalApplication"] = application;

                        LoadExtensions( application );
                        LoadModules( application );
                    }
                }
            }

            PortalApplication = (PortalApplication) context.Application["PortalApplication"];

            context.BeginRequest += ContextBeginRequest;
        }

        private void LoadModules( PortalApplication application )
        {
            using( var db = new PortalEntities() )
            {
                // Load modules
                foreach( string file in System.IO.Directory.GetFiles( string.Format( "{0}\\Modules", application.ServiceDirectory ), "*.dll" ) )
                {
                    Assembly assembly = Assembly.LoadFile( file );

                    // Get the types and identify the IModules
                    foreach( var type in assembly.GetTypes() )
                    {
                        if( type.IsAbstract )
                            continue;

                        if( !type.Implements<IModule>() )
                            continue;
                    
                        var attribute = type.GetCustomAttribute<ModuleAttribute>( true );
                        var module    = (IModule) assembly.CreateInstance( type.FullName );

                        // If an attribute is present on the class, load config from database
                        if( attribute != null )
                        {
                            var moduleConfig = db.Module_Get( null, attribute.ModuleConfigName ).FirstOrDefault();

                            if( moduleConfig == null )
                                throw new ModuleConfigurationMissingException( string.Format( "The module requires a configuration, but none was found with the name: {0}", attribute.ModuleConfigName ) );

                            module.Initialize( moduleConfig.Configuration );

                            var indexSettings = db.IndexSettings_Get( (int?) moduleConfig.ID ).FirstOrDefault();
                    
                            if( indexSettings != null )
                            {
                                foreach( var url in XElement.Parse(indexSettings.Settings).Elements("Core").Select( core => core.Attribute( "url" ).Value ) )
	                            {
                                    application.IndexManager.AddIndex( module.GetType().FullName, new SolrCoreConnection( url ) );
	                            }    
                            }
                        }

                        // Index modules by the Extensions they subscribe to
                        foreach( var method in module.GetType().GetMethods() )
                        {
                            foreach( Datatype datatypeAttribute in method.GetCustomAttributes( typeof( Datatype ), true ) )
                            {
                                if( !application.LoadedModules.ContainsKey( datatypeAttribute.ExtensionName ) )
                                    application.LoadedModules.Add( datatypeAttribute.ExtensionName, new Collection<IModule>() );

                                if( !application.LoadedModules[ datatypeAttribute.ExtensionName ].Contains( module ) )
                                    application.LoadedModules[ datatypeAttribute.ExtensionName ].Add( module );
                            }
                        }
                    }
                }
            }
        }

        private void LoadExtensions( PortalApplication application )
        {
            foreach( string file in System.IO.Directory.GetFiles( string.Format( "{0}\\Extensions", application.ServiceDirectory ), "*.dll" ) )
            {
                var assembly = Assembly.LoadFile( file );

                foreach( var type in assembly.GetTypes() )
                {
                    if( !type.Implements<IExtension>() )
                        continue;
                    
                    var attribute = type.GetCustomAttribute<ExtensionAttribute>( true );

                    application.LoadedExtensions.Add( attribute == null ? type.Name : attribute.ExtensionName,
                                                     (IExtension) assembly.CreateInstance( type.FullName ) );
                }
            }
        }

        #endregion
        #region Business Logic

        private void ContextBeginRequest( object sender, EventArgs e )
        {
            using( var application = (HttpApplication) sender )
            {
                if( IsOnIgnoreList( application.Request.Url.AbsolutePath ) )
                    return; // TODO: 404

                var callContext = CreateCallContext( application.Request );

                PortalApplication.ProcessRequest(callContext);

                application.Response.ContentEncoding = System.Text.Encoding.Unicode;
                application.Response.ContentType     = GetContentType( callContext );
                application.Response.Charset         = "utf-16";

                using( var inputStream  = callContext.GetResponseStream() )
                using( var outputStream = application.Response.OutputStream )
                {
                    inputStream.CopyTo( outputStream );
                }

                application.Response.End();
            }
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
            var split     = request.Url.AbsolutePath.Substring( request.ApplicationPath.Length ).Split('/');
            var extension = split[ split.Length - 2 ];
            var action    = split[ split.Length - 1 ];

            switch( request.HttpMethod )
            {
                case "DELETE":
                case "PUT":
                case "POST":
                    return new CallContext( PortalApplication, new PortalRequest( extension, action, ConvertToIDictionary( request.Form ) ), new PortalResponse() );
                case "GET":
                    return new CallContext( PortalApplication, new PortalRequest( extension, action, ConvertToIDictionary( request.QueryString ) ), new PortalResponse() );
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
            var parameters = new Dictionary<string, string>();
           
            // TODO: Put routing logic into seperate classes
            for( var i = 0; i < nameValueCollection.Keys.Count; i++ )
            {
                parameters.Add( nameValueCollection.Keys[i], nameValueCollection[i]);
            }

            return parameters;
        }

        #endregion
    }
}
