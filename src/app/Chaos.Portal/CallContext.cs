using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CHAOS;
using CHAOS.Extensions;

using Chaos.Portal.Cache;
using Chaos.Portal.Data.Dto;
using Chaos.Portal.Exceptions;
using Chaos.Portal.Logging;
using Chaos.Portal.Request;
using Chaos.Portal.Response;
using Chaos.Portal.Response.Specification;

namespace Chaos.Portal
{
    using Chaos.Portal.Index;

    public class CallContext : ICallContext
    {
        #region Fields

        private ISession                       _session;
        private IUserInfo                      _user;
        private IEnumerable<ISubscriptionInfo> _subscriptions;
        private IEnumerable<IGroup>            _group;
		
	    private const string SessionguidParameterName = "sessionGUID";

        private static readonly Guid _anonymousUserGuid;
        private static readonly IDictionary<ReturnFormat, IResponseSpecification> ResponseSpecifications = new Dictionary<ReturnFormat, IResponseSpecification>(); 

        #endregion
        #region Properties

        public IPortalApplication Application { get; set; }
        public IPortalRequest     Request { get; set; }
        public IPortalResponse    Response { get; set; }
        public ICache             Cache { get; set; }
        public IViewManager       ViewManager { get; set; }
        public ILog               Log { get; private set; }

        /// <summary>
        /// Returns the current user, the user is cached and will not be updated during the callContexts life.
        /// </summary>
        public IUserInfo User 
        { 
            get
            {
                if( _user == null )
                {
                    _user = Application.PortalRepository.GetUserInfo(null, Session.Guid, null).FirstOrDefault();
                        
                    if( _user == null )
                        throw new SessionDoesNotExistException( "Session has expired" );

                    Cache.Put(string.Format( "[UserInfo:sid={0}]", Session.Guid ), _user, new TimeSpan(0, 1, 0) );
                }

                return _user;
            }
        }

		/// <summary>
		/// Get the current session
		/// </summary>
        public ISession Session
        {
            get
            {
				if( GetSessionFromDatabase() == null)
					throw new SessionDoesNotExistException( "SessionGuid is invalid or has expired" );

				return _session;
            }
        }

		/// <summary>
		/// Get subscriptions associated with the current user
		/// </summary>
        public IEnumerable<ISubscriptionInfo> Subscriptions
        {
            get 
            {
                return _subscriptions ?? (_subscriptions = Application.PortalRepository.SubscriptionGet(null, User.Guid));
            }
        }

        /// <summary>
        /// returns the groups the current user is part of, the Groups are cached, changes in the database will not be pickup up during the callContext life.
        /// </summary>
        public IEnumerable<IGroup> Groups
        {
            get { return _group ?? (_group = Application.PortalRepository.GroupGet(null, null, User.Guid)); }
        }

		/// <summary>
		/// True if current user is anonymous
		/// </summary>
        public bool IsAnonymousUser
        {
            get { return GetSessionFromDatabase() == null || AnonymousUserGuid.ToString() == Session.UserGuid.ToString(); }
        }

		/// <summary>
		/// Get the UUID of the anonymous user
		/// </summary>
        public Guid AnonymousUserGuid { get { return _anonymousUserGuid;  } }

        #endregion
        #region Constructors

        static CallContext()
        {
            ResponseSpecifications.Add(ReturnFormat.XML, new XmlResponse());
            ResponseSpecifications.Add(ReturnFormat.JSON, new JsonResponse());
            ResponseSpecifications.Add(ReturnFormat.JSONP, new JsonpResponse());
            ResponseSpecifications.Add(ReturnFormat.ATTACHMENT, new StreamResponse());

            _anonymousUserGuid = new UUID( ConfigurationManager.AppSettings["AnonymousUserGUID"] ).ToGuid();
        }

        public CallContext(IPortalApplication application, IPortalRequest request, IPortalResponse response, ILog log)
        {
            Application = application;
            Request     = request;
            Response    = response.WithResponseSpecification(ResponseSpecifications[request.ReturnFormat]);
            Cache       = application.Cache;
            ViewManager = application.ViewManager;
            Log         = log;
            
            response.Header.ReturnFormat = request.ReturnFormat;
            response.Header.Callback     = request.Parameters.ContainsKey("callback") ? request.Parameters["callback"] : null;
        }

        #endregion
        #region Business Logic

		/// <summary>
		/// Gets the current session from the database
		/// </summary>
		/// <returns>The current session from the database, or null if sessionGUID is not specified</returns>
		public ISession GetSessionFromDatabase()
		{
			if( _session == null )
            {
                if( !Request.Parameters.ContainsKey( SessionguidParameterName ) )
                    return null;

                _session = Application.PortalRepository.SessionGet(new UUID(Request.Parameters[SessionguidParameterName]).ToGuid(), null).FirstOrDefault();

				if( _session == null )
					return null;
            }

			return _session;
		}

        #endregion
    }
}
