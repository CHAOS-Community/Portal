using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Xml.Linq;
using Geckon.Portal.Core.Extension;
using Geckon.Portal.Core.Module;
using Geckon.Portal.Data.Result;
using Geckon.Portal.Data.Result.Standard;
using Geckon.Serialization;
using Geckon.Serialization.JSON;
using Geckon.Serialization.Standard;

namespace Geckon.Portal.Core.Standard.Extension
{
    public abstract class AExtension : Controller, IExtension
    {
        #region Fields

        #endregion
        #region Properties

        public IPortalContext PortalContext { get; private set; }

        protected PortalResult PortalResult { get; set; }
        protected IDictionary<Type, IChecked<IModule>> AssociatedModules { get; set; }
        protected string Controller { get; set; }
        protected string Action { get; set; }
        public ICallContext CallContext { get; set; }
        private Stopwatch Timestamp { get; set; }
        private ReturnFormat ReturnFormat { get; set; }
        private bool UseHttpStatusCodes { get; set; }

        #endregion
        #region Constructors

        public AExtension()
        {
            AssociatedModules = new Dictionary<Type, IChecked<IModule>>();
            Timestamp         = new Stopwatch();
        }

        public void Init( IPortalContext portalContext, string sessionID )
        {
            // TODO: Find better name for XML + No Error code Format
            Init( portalContext, sessionID, "XML", "true" );
        }

        public void Init( IPortalContext portalContext, string sessionID, string format, string useHttpStatusCodes )
        {
            Timestamp.Start();

            PortalContext      = portalContext;
            PortalResult       = new PortalResult( Timestamp ); 
            CallContext        = new CallContext( portalContext.Cache, portalContext.Solr, sessionID );
            ReturnFormat       = ( ReturnFormat ) Enum.Parse( typeof( ReturnFormat ), format.ToUpper() );
            UseHttpStatusCodes = bool.Parse( useHttpStatusCodes );
        }

        #endregion
        #region Business Logic

        #region Override

        /// <summary>
        /// Initializes the list of registered modules that wants calls from the current extension call
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting( ActionExecutingContext filterContext )
        {
            Controller = filterContext.RouteData.Values["Controller"].ToString();
            Action     = filterContext.RouteData.Values["Action"].ToString();

            foreach( IModule module in PortalContext.GetModules( Controller, Action ) )
            {
                AssociatedModules.Add( module.GetType(), new Checked<IModule>( module ) );
            }

            // This Set the CallContext on the Method if specified
            if( filterContext.ActionParameters.ContainsKey( "callContext" ) )
                filterContext.ActionParameters["callContext"] = CallContext;

            CallContext.Parameters = filterContext.ActionParameters.Select( (parameter) => new Parameter( parameter.Key, parameter.Value ) );

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// This Method Call all modules subscriping to this Extension call, that haven't yet been checked
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Result = GetContentResult();

            base.OnActionExecuted(filterContext);
        }

        #region Exception Result

        protected override void OnException( ExceptionContext filterContext )
        {
            base.OnException( filterContext );

            if( filterContext.Exception is TargetInvocationException )
                filterContext.Exception = filterContext.Exception.InnerException; 
            
            filterContext.ExceptionHandled                = true;
            filterContext.Result                          = GetContentResult( ReturnFormat, new ExtensionError( filterContext.Exception, Timestamp ) );
            filterContext.HttpContext.Response.StatusCode = GetErrorStatusCode(  );
        }

        private int GetErrorStatusCode()
        {
            return UseHttpStatusCodes ? 500 : 200;
        }

        #endregion

        #endregion
        #region portalResult formatting

        public ContentResult GetContentResult()
        {
            CallModules( CallContext.Parameters.ToList() );

            return GetContentResult( ReturnFormat, PortalResult );
        }

        private ContentResult GetContentResult( ReturnFormat returnFormat, IPortalResult portalResult )
        {
            ContentResult result = new ContentResult();
            
            switch( returnFormat )
            {
                case ReturnFormat.XML:
                    ISerializer<XDocument> xmlSerializer = SerializerFactory.Get<XDocument>();

                    result.Content     = xmlSerializer.Serialize( portalResult, false ).ToString( SaveOptions.DisableFormatting );
                    result.ContentType = "text/xml";
                    break;
                case ReturnFormat.JSON:
                    ISerializer<JSON> jsonSerializer = SerializerFactory.Get<JSON>();

                    result.Content     = jsonSerializer.Serialize( portalResult, false ).Value;
                    result.ContentType = "application/json";
                    break;
                case ReturnFormat.JSONP:
                    ISerializer<JSON> jsonpSerializer = SerializerFactory.Get<JSON>();

                    result.Content     = jsonpSerializer.Serialize( portalResult, false ).GetAsJSONP( HttpContext.Request.QueryString[ "jsonp"] );
                    result.ContentType = "application/javascript";
                    break;
                default:
                    throw new NotImplementedException( "Format is unknown" );
            }
            
            result.ContentEncoding = Encoding.Unicode;

            return result;
        }

        #endregion
        #region Module

        protected void CallModules( params Parameter[] parameters )
        {
            CallModules( parameters.Select( (parameter)=>parameter ).ToList() );
        }

        private void CallModules( IList<Parameter> parameters )
        {
            foreach( IChecked<IModule> associatedModule in AssociatedModules.Values.Where( module => !module.IsChecked ) )
            {
                IModuleResult result = PortalResult.GetModule( associatedModule.Value.GetType().FullName );
                
                parameters.Add( new Parameter( "callContext", CallContext ) );

                result.AddResult( associatedModule.Value.InvokeMethod( new MethodQuery( Controller,
                                                                                        Action,
                                                                                        parameters ) ) );
                associatedModule.IsChecked = true;
            }
        }

        protected T GetModule<T>() where T : IModule
        {
            return PortalContext.GetModule<T>();
        }

        #endregion

        #endregion
    }
}
