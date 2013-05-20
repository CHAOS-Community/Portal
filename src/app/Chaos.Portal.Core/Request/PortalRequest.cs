namespace Chaos.Portal.Core.Request
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.Linq;

    using Chaos.Portal.Core.Data;
    using Chaos.Portal.Core.Data.Model;
    using Chaos.Portal.Core.Exceptions;

    public class PortalRequest : IPortalRequest
    {
        #region Fields

        private Core.Data.Model.Session _session;
        private UserInfo _user;
        private IEnumerable<SubscriptionInfo> _subscriptions;
        private IEnumerable<Core.Data.Model.Group> _group;

        private IPortalRepository _portalRepository;

        private const string SessionguidParameterName = "sessionGUID";

        private static readonly Guid _anonymousUserGuid;

        #endregion
        #region Constructors

        static PortalRequest()
        {
            _anonymousUserGuid = new Guid(ConfigurationManager.AppSettings["AnonymousUserGUID"]);
        }

        public PortalRequest(Protocol version, string extension, string action, IDictionary<string, string> parameters, IPortalRepository portalRepository, IEnumerable<FileStream> files)
        {
            Stopwatch = new Stopwatch();
            Stopwatch.Start();

            Version          = version;
            Extension        = extension;
            Action           = action;
            Parameters       = parameters;
			Files            = files;
            PortalRepository = portalRepository;
        }

		public PortalRequest(Protocol version, string extension, string action, IDictionary<string, string> parameters, IPortalRepository portalRepository) : this( version, extension, action, parameters, portalRepository, new List<FileStream>() )
        {
        }

        public PortalRequest() : this( Protocol.Latest, null, null, new Dictionary<string, string>(), null, new List<FileStream>() )
        {
            
        }

        #endregion
        #region Properties

        public Protocol                   Version          { get; set; }
        public string                     Extension        { get; protected set; }
        public string                     Action           { get; protected set; }
        public IDictionary<string,string> Parameters       { get; protected set; }
		public IEnumerable<FileStream>    Files            { get; protected set; }
        public Stopwatch                  Stopwatch        { get; private set; }
        public ReturnFormat               ReturnFormat
        { 
            get
            {
                return Parameters.ContainsKey("format") ? (ReturnFormat)Enum.Parse(typeof(ReturnFormat), Parameters["format"].ToUpper()) : ReturnFormat.XML;
            } 
        }

        public IPortalRepository PortalRepository
        {
            get
            {
                if (_portalRepository == null) throw new UnhandledException("Database connection hasn't been initialized");

                return _portalRepository;
            }
            set
            {
                _portalRepository = value;
            }
        }

        public UserInfo User
        {
            get
            {
                if (_user == null)
                {
                    _user = PortalRepository.UserInfoGet(null, Session.Guid, null, null).FirstOrDefault();

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
            get { return _group ?? (_group = PortalRepository.GroupGet(null, null, User.Guid, null).ToList()); }
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
                if (Parameters == null) return null;
                if (!Parameters.ContainsKey(SessionguidParameterName)) return null;

                _session = PortalRepository.SessionGet(new Guid(Parameters[SessionguidParameterName]), null).FirstOrDefault();

                if (_session == null) return null;
            }

            return _session;
        }

        #endregion
    }
}
