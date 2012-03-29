using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Geckon.Portal.Core.Extension;
using Geckon.Portal.Core.Module;
using Geckon.Portal.Data.Result;
using Geckon.Portal.Data.Result.Standard;
using Geckon.Serialization;
using Geckon.Serialization.JSON;
using Geckon.Serialization.Standard;

using System.Diagnostics;

namespace Geckon.Portal.Core.Standard.Extension
{
    public abstract class AExtension : Controller, IExtension
    {
        #region Fields

        #endregion
        #region Properties

        public IPortalContext PortalContext { get; private set; }
        
        public PortalResult  PortalResult { get; set; }

        protected IDictionary<Type, IChecked<IModule>> AssociatedModules { get; set; }
        protected string Controller { get; set; }
        protected string Action { get; set; }

        private ReturnFormat ReturnFormat { get; set; }
        private bool UseHttpStatusCodes { get; set; }
        private ICallContext CallContext { get; set; }
        private Stopwatch TimeStamp { get; set; }
        private IEnumerable<Parameter> Parameters { get; set; }

        public string Result
        {
            get
            {
                switch( ReturnFormat )
                {
                    case ReturnFormat.GXML:
                        return SerializerFactory.Get<XDocument>().Serialize( PortalResult, false ).ToString( SaveOptions.DisableFormatting );
                    case ReturnFormat.JSON:
                        return SerializerFactory.Get<JSON>().Serialize( PortalResult, false ).Value;
                    case ReturnFormat.JSONP:
                        return SerializerFactory.Get<JSON>().Serialize( PortalResult, false ).GetAsJSONP( HttpContext.Request.QueryString[ "callback"] );
                    default:
                        throw new NotImplementedException( "Format is unknown" );
                }
            }
        }

        #endregion
        #region Constructors

        public AExtension()
        {
            TimeStamp = new Stopwatch();
            TimeStamp.Start();

            Parameters = new Parameter[0];
            PortalResult      = new PortalResult(TimeStamp);
            AssociatedModules = new Dictionary<Type, IChecked<IModule>>();
        }

        public void Init( IPortalContext portalContext )
        {
            Init( portalContext, "GXML", "true" );
        }

        public void Init( IPortalContext portalContext, string format, string useHttpStatusCodes )
        {
            PortalContext      = portalContext;
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
            log4net.LogManager.GetLogger("Portal").Debug( filterContext.HttpContext.Request.Url.OriginalString );

            Controller = filterContext.RouteData.Values["Controller"].ToString();
            Action     = filterContext.RouteData.Values["Action"].ToString();

            foreach( IModule module in PortalContext.GetModules( Controller, Action ) )
            {
                AssociatedModules.Add( module.GetType(), new Checked<IModule>( module ) );
            }

            if( filterContext.ActionParameters.ContainsKey( "callContext" ) )
                CallContext = (ICallContext) filterContext.ActionParameters["callContext"];
            else
                CallContext = new CallContext( APortalApplication.Cache,
                                               APortalApplication.IndexManager,
                                               filterContext.HttpContext.Request.QueryString["sessionID"] );
            
            Parameters = filterContext.ActionParameters.Select( ( parameter ) => new Parameter( parameter.Key, parameter.Value ) );

            base.OnActionExecuting(filterContext);
        }

        protected override void Execute(System.Web.Routing.RequestContext requestContext)
        {
            try
            {
                base.Execute( requestContext );
            }
            catch (System.Exception e)
            {
                OnException( e, requestContext.HttpContext.Response );
            }
        }
        /// <summary>
        /// This Method Call all modules subscriping to this Extension call, that haven't yet been checked
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted( ActionExecutedContext filterContext )
        {
            filterContext.Result = GetContentResult();
            
            base.OnActionExecuted( filterContext );
        }

        #region Exception Result

        protected override void OnException( ExceptionContext filterContext )
        {
            base.OnException( filterContext );

            OnException( filterContext.Exception, filterContext.HttpContext.Response );

            filterContext.ExceptionHandled                = true;

        }

        protected void OnException( System.Exception exception, HttpResponseBase response )
        {
            if( exception is TargetInvocationException )
                exception = exception.InnerException; 
            
            log4net.LogManager.GetLogger("Portal").Fatal( "Unhandled exception", exception );

            ContentResult result = GetContentResult(ReturnFormat, new ExtensionError( exception, TimeStamp ) );

            response.StatusCode      = GetErrorStatusCode();
            response.ContentType     = result.ContentType;
            response.ContentEncoding = result.ContentEncoding;

            response.Write(result.Content);
        }

        /// <summary>
        /// Returns either error code 500 or 200, depending on the UseHttpStatusCodes property
        /// </summary>
        /// <returns></returns>
        private int GetErrorStatusCode()
        {
            return UseHttpStatusCodes ? 500 : 200;
        }

        #endregion

        #endregion
        #region portalResult formatting

        protected ContentResult GetContentResult( )
        {
            CallModules( Parameters.ToList() );

            return GetContentResult( ReturnFormat, PortalResult );
        }

        protected ContentResult GetContentResult( ReturnFormat returnFormat, IPortalResult portalResult )
        {
            ContentResult result = new ContentResult();
            
            switch( returnFormat )
            {
                case ReturnFormat.GXML:
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

                    result.Content     = jsonpSerializer.Serialize( portalResult, false ).GetAsJSONP( HttpContext.Request.QueryString[ "callback"] );
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
                if( parameters.Where( parameter => parameter.ParameterName == "callContext" ).FirstOrDefault() == null )
                    parameters.Add( new Parameter( "callContext", CallContext ) );

                IModuleResult result = associatedModule.Value.InvokeMethod( new MethodQuery( Controller,
                                                                                             Action,
                                                                                             parameters ) );
                PortalResult.Modules.Add( result );
                
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
