using System;

namespace Chaos.Portal.Logging
{
	public interface ILog
	{
		void Debug( string message, Exception e = null );
		void Info( string message, Exception e = null );
		void Warn( string message, Exception e = null );
		void Error( string message, Exception e = null );
		void Fatal( string message, Exception e = null );

		void Commit( );

	    ILog WithLoglevel(LogLevel logLevel);
        ILog WithName(string name);
        ILog WithStopwatch(System.Diagnostics.Stopwatch stopwatch);
        ILog WithSessionGuid(Guid sessionGuid);
	}
}
