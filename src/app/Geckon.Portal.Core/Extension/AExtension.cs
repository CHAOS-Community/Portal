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

        #endregion
        #region Constructors

        public AExtension()
        {
            
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
            filterContext.Result = ConvertToContentResult( string.Format( "<Error><Exception>{0}</Exception><Message><![CDATA[{1}]]></Message><StackTrace><![CDATA[{2}]]></StackTrace></Error>" , 
                                                           filterContext.Exception.GetType().FullName, 
                                                           filterContext.Exception.Message,
                                                           filterContext.Exception.StackTrace ) 
                                                         );

            // TODO: Not all clients support status codes, this should be dependant on ClientSettings
            filterContext.HttpContext.Response.StatusCode = 500;
        }

        #endregion
        #region Result formatting

        //protected ContentResult ConvertToContentResult( )
        //{
        //    return ConvertToContentResult( Result );
        //}

        protected ContentResult ConvertToContentResult( string content )
        {
            ContentResult result = new ContentResult();

            result.Content         = string.Format("<PortalResult Duration=\"{0:F1}\">{1}</PortalResult>",
                                     DateTime.Now.Subtract( HttpContext.Timestamp ).TotalMilliseconds,
                                     content);
            result.ContentType     = "text/xml";
            result.ContentEncoding = Encoding.UTF8;
            
            return result;
        }

        protected ContentResult ConvertToContentResult( IEnumerable<XmlSerialize> contents )
        {
            return ConvertToContentResult( ConvertToString( contents ) );
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

        protected ContentResult ConvertToContentResult( XmlDocument content )
        {
            return ConvertToContentResult( content.OuterXml );
        }

        protected ContentResult ConvertToContentResult( XmlSerialize content )
        {
            return ConvertToContentResult( content.ToXML().OuterXml );
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

        protected IEnumerable<XmlSerialize> CallModules( IMethodQuery methodQuery)
        {
            return PortalContext.CallModules( this, methodQuery );
        }

        protected T CallModule<T>( IMethodQuery methodQuery ) where T : XmlSerialize
        {
            return PortalContext.CallModule<T>( this, methodQuery );
        }

        protected T GetModule<T>() where T : IModule
        {
            return PortalContext.GetModule<T>();
        }

        #endregion

        #endregion
    }
}
