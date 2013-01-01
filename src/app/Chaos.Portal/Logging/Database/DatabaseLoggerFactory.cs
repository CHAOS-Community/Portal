using Chaos.Portal.Data;

namespace Chaos.Portal.Logging.Database
{
    public class DatabaseLoggerFactory : ILogFactory
    {
        #region Fields

        private readonly IPortalRepository _portalRepository;

        #endregion
        #region Initialization

        public DatabaseLoggerFactory(IPortalRepository portalRepository)
        {
            _portalRepository = portalRepository;
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