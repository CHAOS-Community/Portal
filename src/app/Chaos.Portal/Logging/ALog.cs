using System.Diagnostics;
using System.Text;
using CHAOS;

namespace Chaos.Portal.Logging
{
	public abstract class ALog : ILog
	{
		#region Fields

		private const string LOG_FORMAT_STRING       = "{0,19} [{1}] {2}\n";
		private const string EXCEPTION_FORMAT_STRING = "\tMessage: {0}\n\tStacktrace: {1}\n";

		#endregion
		#region Properties

		public LogLevel LogLevel { get; set; }
		public string Name { get; set; }
		public UUID SessionGUID { get; set; }

		protected StringBuilder LogBuilder { get; set; }
        protected Stopwatch Stopwatch { get; set; }

		#endregion
		#region Constructors

        protected ALog( string name, UUID sessionGUID, Stopwatch stopwatch, LogLevel logLevel = LogLevel.Debug )
		{
			Name        = name;
			SessionGUID = sessionGUID;
			LogBuilder  = new StringBuilder(  );
            Stopwatch   = stopwatch ?? new Stopwatch();

            WithLoglevel( logLevel );
		}

        public ILog WithLoglevel( LogLevel logLevel )
        {
            LogLevel = logLevel;

            return this;
        }

		#endregion
		#region Business Logic

		protected void Log( LogLevel logLevel, string message, System.Exception e )
		{
			if( logLevel < LogLevel )
				return;

			LogBuilder.AppendFormat( LOG_FORMAT_STRING, System.DateTime.Now.ToString( "o" ), logLevel.ToString().ToUpper(), message );

			for (; e != null; e = e.InnerException)
				LogBuilder.AppendFormat( EXCEPTION_FORMAT_STRING, e.Message, e.InnerException );
		}

		public void Debug( string message, System.Exception e = null )
		{
			Log( LogLevel.Debug, message, e );
		}

		public void Info(string message, System.Exception e = null )
		{
			Log( LogLevel.Info, message, e );
		}

		public void Warn(string message, System.Exception e = null )
		{
			Log( LogLevel.Warn, message, e );
		}

		public void Error(string message, System.Exception e = null )
		{
			Log( LogLevel.Error, message, e );
		}

		public void Fatal(string message, System.Exception e = null )
		{
			Log( LogLevel.Fatal, message, e );
		}

		public abstract void Commit( );

	    #endregion
	}
}
