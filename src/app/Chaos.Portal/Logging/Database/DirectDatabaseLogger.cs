using System;

namespace Chaos.Portal.Logging.Database
{
    public class DirectDatabaseLogger : ILog
    {
        #region Fields

        private readonly string Name = "PortalApplication";

        #endregion
        #region Properties

        private LogLevel LogLevel { get; set; }

        #endregion
        #region Initialization

        public ILog WithLoglevel( LogLevel logLevel )
        {
            LogLevel = logLevel;

            return this;
        }

        #endregion

        public void Debug(string message, Exception e = null)
        {
            var log = new DatabaseLogger(Name, null, LogLevel);

            log.Debug(message, e);

            log.Commit();
        }

        public void Info(string message, Exception e = null)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message, Exception e = null)
        {
            throw new NotImplementedException();
        }

        public void Error(string message, Exception e = null)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string message, Exception e = null)
        {
            throw new NotImplementedException();
        }

        public void Commit(uint duration)
        {
            throw new NotImplementedException();
        }


        public void Commit()
        {
            throw new NotImplementedException();
        }
    }
}