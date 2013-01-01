﻿namespace Chaos.Portal.Logging
{
	public interface ILog
	{
		void Debug( string message, System.Exception e = null );
		void Info( string message, System.Exception e = null );
		void Warn( string message, System.Exception e = null );
		void Error( string message, System.Exception e = null );
		void Fatal( string message, System.Exception e = null );

		void Commit( );

	    ILog WithLoglevel(LogLevel logLevel);
	}
}
