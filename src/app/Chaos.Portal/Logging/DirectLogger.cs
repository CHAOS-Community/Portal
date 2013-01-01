using System;
using System.Diagnostics;

namespace Chaos.Portal.Logging
{
    public class DirectLogger : ILog
    {
        #region Fields

        private readonly ILogFactory _loggerFactory;

        private string    _name;
        private Guid      _sessionGuid;
        private Stopwatch _stopwatch;
        private LogLevel  _logLevel;

        #endregion
        #region Initialization

        public DirectLogger(ILogFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public ILog WithLoglevel( LogLevel logLevel )
        {
            _logLevel = logLevel;

            return this;
        }

        public ILog WithName(string name)
        {
            _name = name;

            return this;
        }

        public ILog WithStopwatch(Stopwatch stopwatch)
        {
            _stopwatch = stopwatch;

            return this;
        }

        public ILog WithSessionGuid(Guid sessionGuid)
        {
            _sessionGuid = sessionGuid;

            return this;
        }

        #endregion
        #region Business Logic

        public void Debug(string message, Exception e = null)
        {
            var log = GetLogger();

            log.Debug(message, e);

            log.Commit();
        }

        public void Info(string message, Exception e = null)
        {
            var log = GetLogger();

            log.Info(message, e);

            log.Commit();
        }

        public void Warn(string message, Exception e = null)
        {
            var log = GetLogger();

            log.Warn(message, e);

            log.Commit();
        }

        public void Error(string message, Exception e = null)
        {
            var log = GetLogger();

            log.Error(message, e);

            log.Commit();
        }

        public void Fatal(string message, Exception e = null)
        {
            var log = GetLogger();

            log.Fatal(message, e);

            log.Commit();
        }

        public void Commit(uint duration)
        {
            throw new NotImplementedException();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        private ILog GetLogger()
        {
            return _loggerFactory.Create().WithLoglevel(_logLevel).WithName(_name).WithSessionGuid(_sessionGuid).WithStopwatch(_stopwatch);
        }

        #endregion
    }
}