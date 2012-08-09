using System.Text;
using CHAOS.Portal.Data.EF;

namespace CHAOS.Portal.Core.Logging.Database
{
	public class DatabaseLogger : ALog
	{
		#region Fields

		#endregion
		#region Properties

		#endregion
		#region Constructors

		public DatabaseLogger( string name, UUID sessionGUID, LogLevel logLevel = LogLevel.Debug ) : base(name, sessionGUID, logLevel)
		{
			
		}

		#endregion
		#region Business Logic

		public override void Commit( uint duration )
		{
			using( var db = new PortalEntities() )
			{
				db.Log_Create( Name, LogLevel.ToString().ToUpper(), SessionGUID.ToByteArray(), (int?) duration, LogBuilder.ToString() );
			}
		}

		#endregion
	}
}
