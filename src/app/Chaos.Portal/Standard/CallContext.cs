using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using CHAOS;
using CHAOS.Extensions;
using CHAOS.Index;
using CHAOS.Portal.Exception;
using Chaos.Portal.Cache;
using Chaos.Portal.Data.Dto;
using Chaos.Portal.Request;
using Chaos.Portal.Response;
using Chaos.Portal.Response.Specification;

namespace Chaos.Portal.Standard
{
    public class CallContext : ICallContext
    {
        #region Fields

        private ISession                       _session;
        private IUserInfo                      _user;
        private IEnumerable<ISubscriptionInfo> _subscriptions;
        private IEnumerable<IGroup>            _group;
		
	    private const string SESSIONGUID_PARAMETER_NAME = "sessionGUID";

        private static readonly Guid _anonymousUserGuid;
        private static readonly IDictionary<ReturnFormat, IResponseSpecification> responseSpecifications = new Dictionary<ReturnFormat, IResponseSpecification>(); 

        #endregion
        #region Properties

        public IPortalApplication Application { get; set; }
        public IPortalRequest     Request { get; set; }
        public IPortalResponse    Response { get; set; }
        public ICache             Cache { get; set; }
        public IIndexManager      IndexManager { get; set; }

        /// <summary>
        /// Returns the current user, the user is cached and will not be updated during the callContexts life.
        /// </summary>
        public IUserInfo User 
        { 
            get
            {
                if( _user == null )
                {
                    _user = Application.PortalRepository.GetUserInfo(null, Session.GUID.ToGuid(), null).FirstOrDefault();
                        
                    if( _user == null )
                        throw new SessionDoesNotExistException( "Session has expired" );

                    Cache.Put( string.Format( "[UserInfo:sid={0}]", Session.GUID ), _user, new TimeSpan(0, 1, 0) );
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
					throw new SessionDoesNotExistException( "SessionGUID is invalid or has expired" );

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
                return _subscriptions ?? (_subscriptions = Application.PortalRepository.SubscriptionGet(null, User.GUID.ToGuid()));
            }
        }

        /// <summary>
        /// returns the groups the current user is part of, the Groups are cached, changes in the database will not be pickup up during the callContext life.
        /// </summary>
        public IEnumerable<IGroup> Groups
        {
            get { return _group ?? (_group = Application.PortalRepository.GroupGet(null, null, User.GUID.ToGuid())); }
        }

		/// <summary>
		/// True if current user is anonymous
		/// </summary>
        public bool IsAnonymousUser
        {
            get { return GetSessionFromDatabase() == null || AnonymousUserGuid.ToString() == Session.UserGUID.ToString(); }
        }

		/// <summary>
		/// Get the UUID of the anonymous user
		/// </summary>
        public Guid AnonymousUserGuid { get { return _anonymousUserGuid;  } }

        #endregion
        #region Constructors

        static CallContext()
        {
            responseSpecifications.Add(ReturnFormat.XML, new XmlResponse());
            responseSpecifications.Add(ReturnFormat.JSON, new JsonResponse());
            responseSpecifications.Add(ReturnFormat.JSONP, new JsonResponse());

            _anonymousUserGuid = new UUID( ConfigurationManager.AppSettings["AnonymousUserGUID"] ).ToGuid();
        }

        public CallContext(IPortalApplication application, IPortalRequest request, IPortalResponse response)
        {
            Application  = application;
            Request      = request;
            Response     = response.WithResponseSpecification(responseSpecifications[Request.ReturnFormat]);
            Cache        = application.Cache;
            IndexManager = application.IndexManager;

            Response.Header.ReturnFormat = Request.ReturnFormat;
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
                if( !Request.Parameters.ContainsKey( SESSIONGUID_PARAMETER_NAME ) )
                    return null;

                _session = Application.PortalRepository.SessionGet(new UUID(Request.Parameters[SESSIONGUID_PARAMETER_NAME]).ToGuid(), null).FirstOrDefault();

				if( _session == null )
					return null;
            }

			return _session;
		}

        #endregion
    }
}
