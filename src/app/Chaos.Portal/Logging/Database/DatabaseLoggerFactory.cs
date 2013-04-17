using Chaos.Portal.Core.Data;

namespace Chaos.Portal.Logging.Database
{
    public class DatabaseLoggerFactory : ILogFactory
    {
        #region Fields

        private readonly IPortalRepository _portalRepository;
        
        private LogLevel _logLevel;

        #endregion
        #region Initialization

        public DatabaseLoggerFactory(IPortalRepository portalRepository)
        {
            _portalRepository = portalRepository;
        }

        public ILogFactory WithLogLevel(LogLevel logLevel)
        {
            _logLevel = logLevel;

            return this;
        }

        #endregion
        #region Business Logic

        public ILog Create()
        {
            return new DatabaseLogger(_portalRepository);
        }


        #endregion
    }
}