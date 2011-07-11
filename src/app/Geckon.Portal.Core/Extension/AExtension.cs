using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using System.Xml;
using Geckon.Portal.Core.Module;
using Geckon.Portal.Data;
using Geckon.Serialization.Xml;

namespace Geckon.Portal.Core.Extension
{
    public abstract class AExtension : Controller, IExtension
    {
        #region Fields

        private IPortalContext _PortalContext;
        private IDictionary<Type, IChecked<IModule>> _AssociatedModules;

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

        #endregion
        #region Constructors

        public AExtension()
        {
            _AssociatedModules = new Dictionary<Type, IChecked<IModule>>();
        }

        public AExtension( IPortalContext context ) : this()
        {
            _PortalContext     = context;    
        }

        public void Init( IResult result )
        {
            ResultBuilder = result;
        }

        #endregion
        #region Business Logic

        #region Data

        protected PortalDataContext GetNewPortalDataContext()
        {
            return new PortalDataContext( ConfigurationManager.ConnectionStrings["Portal"].ConnectionString );
        }

        /// <summary>
        /// Initializes the list of registered modules that wants calls from the current extension call
        /// </summary>
        /// <param name="requestContext"></param>
        protected override void Execute( System.Web.Routing.RequestContext requestContext )
        {
            base.Execute(requestContext);

            foreach( IModule module in PortalContext.GetModules( requestContext.RouteData.Values["Controller"].ToString(),
                                                                 requestContext.RouteData.Values["Action"].ToString() ) )
            {
                _AssociatedModules.Add( module.GetType(), new Checked<IModule>( module ) );
            }
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

        protected Data.Dto.UserInfo GetUserInfo( string sessionId )
        {
            // TODO: Consider how to best handle Caching
            Data.Dto.UserInfo user = PortalContext.Cache.Get<Data.Dto.UserInfo>( "user-" + sessionId ); 
            
            if( user != null )
                return user;

            using( PortalDataContext db = GetNewPortalDataContext() )
            {
                UserInfo dbUser = db.UserInfo_Get( null, Guid.Parse( sessionId ) ).First();

                user = Data.Dto.UserInfo.Create( dbUser );

                PortalContext.Cache.Put( "user-" + sessionId, user.ToXML().OuterXml, new TimeSpan( 0, 20, 0 ) );

                return user;
            }
        }

        #endregion
        #region Module

        protected void CallModules( IMethodQuery methodQuery )
        {
            foreach( IModule associatedModule in _AssociatedModules.Values.Where( module => !module.IsChecked ) )
            {
                ResultBuilder.Add( associatedModule.GetType().FullName, associatedModule.InvokeMethod( methodQuery ) );
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
