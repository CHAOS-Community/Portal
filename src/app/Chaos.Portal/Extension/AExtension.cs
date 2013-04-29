namespace Chaos.Portal.Extension
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Reflection;

    using Chaos.Portal.Cache;
    using Chaos.Portal.Core.Data;
    using Chaos.Portal.Core.Data.Model;
    using Chaos.Portal.Core.Exceptions;
    using Chaos.Portal.Indexing.View;
    using Chaos.Portal.Logging;
    using Chaos.Portal.Request;
    using Chaos.Portal.Response;

    public abstract class AExtension : IExtension
    {
        #region Fields

        private Core.Data.Model.Session _session;
        private UserInfo _user;
        private IEnumerable<SubscriptionInfo> _subscriptions;
        private IEnumerable<Core.Data.Model.Group> _group;

        private const string SessionguidParameterName = "sessionGUID";

        private static readonly Guid _anonymousUserGuid;

        public IPortalRequest Request { get; private set; }

        #endregion
        #region Initialization

        static AExtension()
        {
            _anonymousUserGuid = new Guid(ConfigurationManager.AppSettings["AnonymousUserGUID"]);
        }

        protected AExtension()
        {
            MethodInfos = new Dictionary<string, MethodInfo>();
        }

        public abstract IExtension WithConfiguration(string configuration);

        public IExtension WithPortalApplication(IPortalApplication portalApplication)
        {
            PortalApplication = portalApplication;
            
            return this;
        }

        public IExtension WithPortalResponse(IPortalResponse response)
        {
            Response = response;

            return this;
        }

        public IExtension WithPortalRequest(IPortalRequest request)
        {
            Request = request;

            return this;
        }

        #endregion
        #region Properties

        protected ILog Log { get; private set; }
        protected ICache Cache { get; set; }
        protected IViewManager ViewManager { get; set; }
        protected IPortalResponse Response { get; set; }
        protected IPortalRepository PortalRepository { get { return PortalApplication.PortalRepository; } }
        protected IDictionary<string, MethodInfo> MethodInfos { get; set; }

        public IPortalApplication PortalApplication { get; set; }

        /// <summary>
        /// Returns the current user, the user is cached and will not be updated during the callContexts life.
        /// </summary>
        public UserInfo User
        {
            get
            {
                if (_user == null)
                {
                    _user = PortalRepository.UserInfoGet(null, Session.Guid, null).FirstOrDefault();

                    if (_user == null) throw new SessionDoesNotExistException("No user associted with session");
                }

                return _user;
            }
        }

        /// <summary>
        /// Get the current session
        /// </summary>
        public Core.Data.Model.Session Session
        {
            get
            {
                if (GetSessionFromDatabase() == null) throw new SessionDoesNotExistException("SessionGUID is invalid or has expired");

                return _session;
            }
        }

        /// <summary>
        /// Get subscriptions associated with the current user
        /// </summary>
        public IEnumerable<Core.Data.Model.SubscriptionInfo> Subscriptions
        {
            get
            {
                return _subscriptions ?? (_subscriptions = PortalRepository.SubscriptionGet(null, User.Guid));
            }
        }

        /// <summary>
        /// returns the groups the current user is part of, the Groups are cached, changes in the database will not be pickup up during the callContext life.
        /// </summary>
        public IEnumerable<Core.Data.Model.Group> Groups
        {
            get { return _group ?? (_group = PortalRepository.GroupGet(null, null, User.Guid).ToList()); }
        }

        /// <summary>
        /// Get the UUID of the anonymous user
        /// </summary>
        public Guid AnonymousUserGuid { get { return _anonymousUserGuid; } }

        /// <summary>
        /// True if current user is anonymous
        /// </summary>
        public bool IsAnonymousUser
        {
            get { return GetSessionFromDatabase() == null || AnonymousUserGuid.ToString() == Session.UserGuid.ToString(); }
        }

        /// <summary>
        /// Gets the current session from the database
        /// </summary>
        /// <returns>The current session from the database, or null if sessionGUID is not specified</returns>
        public Core.Data.Model.Session GetSessionFromDatabase()
        {
            if (_session == null)
            {
                if (Request == null) return null;
                if (Request.Parameters == null) return null;
                if (!Request.Parameters.ContainsKey(SessionguidParameterName)) return null;

                _session = PortalRepository.SessionGet(new Guid(Request.Parameters[SessionguidParameterName]), null).FirstOrDefault();

                if (_session == null) return null;
            }

            return _session;
        }

        #endregion
        #region Business Logic

        /// <summary>
        /// Calls an action on the extension with the parameters based on the settings specified in the inputParameters
        /// </summary>
        public virtual IPortalResponse CallAction(IPortalRequest request)
        {
            Request = request;

            if(!MethodInfos.ContainsKey(Request.Action))
                MethodInfos.Add(Request.Action, GetType().GetMethod(Request.Action));

            var method     = MethodInfos[Request.Action];
            var parameters = BindParameters(Request.Parameters, method.GetParameters());
            
            try
            {
                var result = method.Invoke(this, parameters);

                Response.WriteToResponse(result);
            }
            catch (TargetInvocationException e)
            {
                Response.Error.SetException(e.InnerException);
                
                PortalApplication.Log.Fatal("ProcessRequest() - Unhandeled exception occured during", e.InnerException);
            }

            return Response;
        }

        private object[] BindParameters(IDictionary<string, string> inputParameters, ICollection<ParameterInfo> parameters)
        {
            var boundParameters = new object[parameters.Count];

            foreach (var parameterInfo in parameters)
            {
                if (!PortalApplication.Bindings.ContainsKey(parameterInfo.ParameterType))
                    throw new ParameterBindingMissingException(string.Format("There is no binding for the type:{0}", parameterInfo.ParameterType.FullName));

                boundParameters[parameterInfo.Position] = PortalApplication.Bindings[parameterInfo.ParameterType].Bind(inputParameters, parameterInfo);
            }

            return boundParameters;
        }

        #endregion
    }
}
