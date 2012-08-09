using CHAOS.Portal.Core.Logging;
using CHAOS.Portal.Core.Logging.Database;
using NUnit.Framework;

namespace CHAOS.Portal.Core.Test
{
	[TestFixture]
	public class LogTest
	{
		[Test]
		public void Should_Log_DEBUG()
		{
			var log = new MockLog( "Test loger", new UUID(), LogLevel.Debug );

			log.Debug( "debug" );
			log.Info( "info" );
			log.Warn( "warn" );
			log.Error( "error" );
			log.Fatal( "fatal" );

			log.Commit( 0 );

			Assert.IsTrue( log.Result.Contains("debug") );
			Assert.IsTrue( log.Result.Contains("info") );
			Assert.IsTrue( log.Result.Contains("warn") );
			Assert.IsTrue( log.Result.Contains("error") );
			Assert.IsTrue( log.Result.Contains("fatal") );
		}

		[Test]
		public void Should_Log_INFO()
		{
			var log = new MockLog( "Test loger", new UUID(), LogLevel.Info );

			log.Debug( "debug" );
			log.Info( "info" );
			log.Warn( "warn" );
			log.Error( "error" );
			log.Fatal( "fatal" );

			log.Commit(0);

			Assert.IsFalse( log.Result.Contains("debug") );

			Assert.IsTrue( log.Result.Contains("info") );
			Assert.IsTrue( log.Result.Contains("warn") );
			Assert.IsTrue( log.Result.Contains("error") );
			Assert.IsTrue( log.Result.Contains("fatal") );
		}

		[Test]
		public void Should_Log_WARN()
		{
			var log = new MockLog( "Test loger", new UUID(), LogLevel.Warn );

			log.Debug( "debug" );
			log.Info( "info" );
			log.Warn( "warn" );
			log.Error( "error" );
			log.Fatal( "fatal" );

			log.Commit(0);

			Assert.IsFalse( log.Result.Contains("debug") );
			Assert.IsFalse( log.Result.Contains("info") );

			Assert.IsTrue( log.Result.Contains("warn") );
			Assert.IsTrue( log.Result.Contains("error") );
			Assert.IsTrue( log.Result.Contains("fatal") );
		}

		[Test]
		public void Should_Log_ERROR()
		{
			var log = new MockLog( "Test loger", new UUID(), LogLevel.Error );

			log.Debug( "debug" );
			log.Info( "info" );
			log.Warn( "warn" );
			log.Error( "error" );
			log.Fatal( "fatal" );

			log.Commit(0);

			Assert.IsFalse( log.Result.Contains("debug") );
			Assert.IsFalse( log.Result.Contains("info") );
			Assert.IsFalse( log.Result.Contains("warn") );

			Assert.IsTrue( log.Result.Contains("error") );
			Assert.IsTrue( log.Result.Contains("fatal") );
		}

		[Test]
		public void Should_Log_FATAL()
		{
			var log = new MockLog( "Test loger", new UUID(), LogLevel.Fatal );

			log.Debug( "debug" );
			log.Info( "info" );
			log.Warn( "warn" );
			log.Error( "error" );
			log.Fatal( "fatal" );

			log.Commit(0);

			Assert.IsFalse( log.Result.Contains("debug") );
			Assert.IsFalse( log.Result.Contains("info") );
			Assert.IsFalse( log.Result.Contains("warn") );
			Assert.IsFalse( log.Result.Contains("error") );

			Assert.IsTrue( log.Result.Contains("fatal") );
		}

		[Test]
		public void Should_Log_To_Database()
		{
			var log = new DatabaseLogger( "PortalTest", new UUID(), LogLevel.Debug );

			log.Debug( "debug" );
			log.Info( "info" );
			log.Warn( "warn" );
			log.Error( "error" );
			log.Fatal( "fatal" );

			log.Commit(0);
		}
	}
}
