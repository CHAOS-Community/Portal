using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Xml;
using Geckon.Portal.Core.Extension;
using Geckon.Portal.Core.Module;
using Geckon.Portal.Data;
using Geckon.Serialization.Xml;

namespace Geckon.Portal.Core.Standard.Extension
{
    public abstract class AExtension : Controller, IExtension
    {
        #region Fields

        private IPortalContext _PortalContext;

        #endregion
        #region Properties

        public IPortalContext PortalContext
        {
            get
            {
                if( _PortalContext == null )
                    _PortalContext = ((APortalApplication)HttpContext.ApplicationInstance).PortalContext;

                return _PortalContext;
            }
        }

        protected IResult ResultBuilder { get; set; }

        protected IDictionary<Type, IChecked<IModule>> AssociatedModules { get; set; }
        protected string Controller { get; set; }
        protected string Action { get; set; }

        #endregion
        #region Constructors

        public AExtension()
        {
            AssociatedModules = new Dictionary<Type, IChecked<IModule>>();
        }

        public AExtension( IPortalContext context ) : this()
        {
            _PortalContext     = context;    
        }

        public void Init( IResult result )
        {
            ResultBuilder = result;
        }

        /// <summary>
        /// Initializes the list of registered modules that wants calls from the current extension call
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Controller = filterContext.RouteData.Values["Controller"].ToString();
            Action     = filterContext.RouteData.Values["Action"].ToString();

            foreach( IModule module in PortalContext.GetModules( Controller, Action ) )
            {
                AssociatedModules.Add( module.GetType(), new Checked<IModule>( module ) );
            }

            base.OnActionExecuting(filterContext);
        }

        #endregion
        #region Business Logic

        #region Data

        protected PortalDataContext GetNewPortalDataContext()
        {
            return new PortalDataContext( ConfigurationManager.ConnectionStrings["Portal"].ConnectionString );
        }

        #endregion
        #region Override

        protected override void OnException( ExceptionContext filterContext )
        {
            base.OnException( filterContext );

            if( filterContext.Exception is TargetInvocationException )
                filterContext.Exception = filterContext.Exception.InnerException; 
            
            filterContext.ExceptionHandled = true;
            filterContext.Result = GetContentResult( string.Format( "<Error><Exception>{0}</Exception><Message><![CDATA[{1}]]></Message><StackTrace><![CDATA[{2}]]></StackTrace></Error>" , 
                                                           filterContext.Exception.GetType().FullName, 
                                                           filterContext.Exception.Message,
                                                           filterContext.Exception.StackTrace ) 
                                                         );

            // TODO: Not all clients support status codes, this should be dependant on ClientSettings
            filterContext.HttpContext.Response.StatusCode = 500;
        }

        #endregion
        #region Result formatting

        protected ContentResult GetContentResult()
        {
            return GetContentResult( ResultBuilder.Content );
        }

        protected ContentResult GetContentResult( string content )
        {
            ContentResult result = new ContentResult();
            
            result.Content         = string.Format("<PortalResult Duration=\"{0:F1}\">{1}</PortalResult>",
                                     HttpContext == null ? 0 : DateTime.Now.Subtract(HttpContext.Timestamp).TotalMilliseconds,
                                     content);
            result.ContentType     = "text/xml";
            result.ContentEncoding = Encoding.UTF8;
            
            return result;
        }

        protected ContentResult GetContentResult( IEnumerable<XmlSerialize> contents )
        {
            return GetContentResult( ConvertToString( contents ) );
        }

        protected string ConvertToString( IEnumerable<XmlSerialize> contents )
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach( XmlSerialize xml in contents )
            {
                stringBuilder.Append(xml.ToXML().OuterXml);
            }

            return stringBuilder.ToString();
        }

        protected ContentResult GetContentResult( XmlDocument content )
        {
            return GetContentResult( content.OuterXml );
        }

        protected ContentResult GetContentResult( XmlSerialize content )
        {
            return GetContentResult( content.ToXML().OuterXml );
        }

        #endregion
        #region UserInfo

        protected Data.Dto.UserInfo GetUserInfo( string sessionID )
        {
            Data.Dto.UserInfo userInfo = PortalContext.Cache.Get<Data.Dto.UserInfo>( string.Format( "[UserInfo:sid={0}]", sessionID ) );

            if( userInfo == null )
            {
                using( PortalDataContext db = GetNewPortalDataContext() )
                {
                    userInfo = Data.Dto.UserInfo.Create( db.UserInfo_Get( null, Guid.Parse( sessionID ), null, null, null ).First() );

                    PortalContext.Cache.Put( string.Format( "[UserInfo:sid={0}]", sessionID ),
                                             userInfo.ToXML().OuterXml,
                                             new TimeSpan( 0, 1, 0 ) );
                }
            }

            return userInfo;
        }

        #endregion
        #region Module

        protected void CallModules( params Parameter[] parameters )
        {
            foreach( IChecked<IModule> associatedModule in AssociatedModules.Values.Where( module => !module.IsChecked ) )
            {
                ResultBuilder.Add( associatedModule.Value.GetType().FullName, 
                                   associatedModule.Value.InvokeMethod( new MethodQuery( Controller, 
                                                                                         Action, 
                                                                                         parameters ) ) );

                associatedModule.IsChecked = true;
            }
        }

        protected void CallModule<T>( string controller, string action, params Parameter[] parameters )
        {
            IChecked<IModule> associatedModule = AssociatedModules[ typeof( T ) ];

            ResultBuilder.Add( associatedModule.Value.GetType().FullName, 
                               associatedModule.Value.InvokeMethod( new MethodQuery( Controller, 
                                                                                     Action, 
                                                                                     parameters ) ) );

            associatedModule.IsChecked = true;
        }

        protected T GetModule<T>() where T : IModule
        {
            return PortalContext.GetModule<T>();
        }

        #endregion

        #endregion
    }
}
