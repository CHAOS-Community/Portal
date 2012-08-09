namespace CHAOS.Portal.Core.Logging
{
	public enum LogLevel : uint
	{
		Debug	= 1 << 1,
		Info	= 1 << 2,
		Warn	= 1 << 3,
		Error	= 1 << 4,
		Fatal	= 1 << 5
	}
}