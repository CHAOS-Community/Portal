using System.Text;

namespace CHAOS.Portal.Core.Logging
{
	public abstract class ALog : ILog
	{
		#region Fields

		#endregion
		#region Properties

		public LogLevel LogLevel { get; set; }
		public string Name { get; set; }
		public uint Duration { get; set; }
		public UUID SessionGUID { get; set; }

		protected StringBuilder LogBuilder { get; set; }

		private const string LOG_FORMAT_STRING       = "{0,19} [{1,5}] {2}\n";
		private const string EXCEPTION_FORMAT_STRING = "\tMessage: {0}\nStacktrace: {1}\n";

		#endregion
		#region Constructors

		protected ALog( string name, UUID sessionGUID, LogLevel logLevel = LogLevel.Debug )
		{
			Name        = name;
			LogLevel    = logLevel;
			SessionGUID = sessionGUID;
			LogBuilder  = new StringBuilder(  );
		}

		#endregion
		#region Business Logic

		protected void Log( LogLevel logLevel, string message, System.Exception e )
		{
			if( logLevel < LogLevel )
				return;

			LogBuilder.AppendFormat( LOG_FORMAT_STRING, System.DateTime.Now.ToString( "yyyy-MM-dd'T'hh:mm:ss" ), logLevel.ToString().ToUpper(), message );

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

		public abstract void Commit( uint duration );

		#endregion
	}
}
