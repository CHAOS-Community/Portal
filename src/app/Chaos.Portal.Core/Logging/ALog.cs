namespace Chaos.Portal.Core.Logging
{
    using System;
    using System.Diagnostics;
    using System.Text;

    public abstract class ALog : ILog
	{
		#region Fields

		private const string LOG_FORMAT_STRING       = "{0,19} [{1}] {2}\n";
        private const string EXCEPTION_FORMAT_STRING = "\tType: {0}\n\tMessage: {1}\n\tStacktrace: {2}\n";

		#endregion
		#region Properties

		public LogLevel LogLevel { get; set; }
		public string Name { get; set; }
		public Guid? SessionGuid { get; set; }

		protected StringBuilder LogBuilder { get; set; }
        protected Stopwatch Stopwatch { get; set; }

		#endregion
		#region Constructors

        protected ALog( )
		{
			LogBuilder  = new StringBuilder(  );
            Stopwatch   = new Stopwatch();
		}

        public ILog WithLoglevel( LogLevel logLevel )
        {
            LogLevel = logLevel;

            return this;
        }

        public ILog WithName(string name)
        {
            Name = name;

            return this;
        }

        public ILog WithStopwatch(Stopwatch stopwatch)
        {
            Stopwatch = stopwatch;

            return this;
        }

        public ILog WithSessionGuid(Guid sessionGuid)
        {
            SessionGuid = sessionGuid;

            return this;
        }

	    #endregion
		#region Business Logic

		protected void Log( LogLevel logLevel, string message, Exception e )
		{
			if( logLevel < LogLevel )
				return;

			LogBuilder.AppendFormat( LOG_FORMAT_STRING, DateTime.Now.ToString( "o" ), logLevel.ToString().ToUpper(), message );

			for (; e != null; e = e.InnerException)
				LogBuilder.AppendFormat( EXCEPTION_FORMAT_STRING, e.GetType().Name, e.Message, e.StackTrace );
		}

		public void Debug( string message, Exception e = null )
		{
			Log( LogLevel.Debug, message, e );
		}

		public void Info(string message, Exception e = null )
		{
			Log( LogLevel.Info, message, e );
		}

		public void Warn(string message, Exception e = null )
		{
			Log( LogLevel.Warn, message, e );
		}

		public void Error(string message, Exception e = null )
		{
			Log( LogLevel.Error, message, e );
		}

		public void Fatal(string message, Exception e = null )
		{
			Log( LogLevel.Fatal, message, e );
		}

		public abstract void Commit( );

	    #endregion
	}
}
