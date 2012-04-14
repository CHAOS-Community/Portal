using System;
using System.Web;
using CHAOS.Portal.Core.Request;

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
        }

        #endregion
        #region Business Logic

        private void ContextBeginRequest( object sender, EventArgs e )
        {
            HttpApplication application = (HttpApplication) sender;

            ProcessRequest( CreatePortalRequest( application.Request ) );

            application.Response.End();
        }

        protected IPortalRequest CreatePortalRequest( HttpRequest request )
        {
            Parameter[] parameters = new Parameter[ request.QueryString.Count ];

            string[] split = request.Url.AbsolutePath.Substring( request.ApplicationPath.Length ).Split( '/' );

            for( int i = 0; i < request.QueryString.Keys.Count; i++ )
            {
                parameters[i] = new Parameter( request.QueryString.Keys[i], request.QueryString[i] );
            }

            return new PortalRequest( split[0], split[1], parameters );
        }

        #endregion
    }
}
