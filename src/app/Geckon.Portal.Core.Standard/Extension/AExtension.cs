using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
using Geckon.Serialization.Standard.String;
using Geckon.Serialization.Standard.XML;

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

        #endregion
        #region Constructors

        public AExtension()
        {
            AssociatedModules = new Dictionary<Type, IChecked<IModule>>();
            Timestamp         = new Stopwatch();
        }

        public void Init( IPortalContext portalContext, string sessionID )
        {
            Timestamp.Start();

            PortalContext = portalContext;
            PortalResult  = new PortalResult( Timestamp ); 
            CallContext   = new CallContext( portalContext.Cache, portalContext.Solr, sessionID );
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

        protected override void OnException( ExceptionContext filterContext )
        {
            base.OnException( filterContext );

            if( filterContext.Exception is TargetInvocationException )
                filterContext.Exception = filterContext.Exception.InnerException; 
            
            filterContext.ExceptionHandled = true;
            filterContext.Result           = GetContentResult( new ExtensionError( filterContext.Exception, Timestamp ) );

            // TODO: Not all clients support status codes, this should be dependant on ClientSettings
            filterContext.HttpContext.Response.StatusCode = 200;
        }

        #endregion
        #region portalResult formatting

        public ContentResult GetContentResult()
        {
            CallModules( CallContext.Parameters.ToList() );

            return GetContentResult( PortalResult );
        }

        private static ContentResult GetContentResult( IPortalResult portalResult )
        {
            ContentResult result = new ContentResult();

            ISerializer<XDocument> serializer = new XMLSerializer( new StringSerializer( CultureInfo.InvariantCulture ) ); 
            
            result.Content         = serializer.Serialize( portalResult, false ).ToString( SaveOptions.DisableFormatting );
            result.ContentType     = "text/xml";
            result.ContentEncoding = Encoding.Unicode;

            return result;
        }

        #endregion
        #region Module

        protected void CallModules( params Parameter[] parameters )
        {
            CallModules( parameters.Select( (parameter)=>parameter ).ToList() );
        }

        private void CallModules(IList<Parameter> parameters)
        {
            foreach( IChecked<IModule> associatedModule in AssociatedModules.Values.Where( module => !module.IsChecked ) )
            {
                parameters.Add( new Parameter( "callContext", CallContext ) );

                PortalResult.GetModule( associatedModule.Value.GetType().FullName ).AddResult( associatedModule.Value.InvokeMethod( new MethodQuery( Controller,
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
