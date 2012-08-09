using CHAOS.Portal.Core.Logging;

namespace CHAOS.Portal.Core.Test
{
	public class MockLog : ALog
	{
		public string Result { get; set; }

		public MockLog( string name, UUID sessionGUID, LogLevel logLevel = LogLevel.Debug ) : base(name, sessionGUID, logLevel)
		{
		}

		public override void Commit( uint duration )
		{
			Result = LogBuilder.ToString();
		}
	}
}