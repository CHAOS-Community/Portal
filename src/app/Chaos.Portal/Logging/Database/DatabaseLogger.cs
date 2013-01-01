using System.Diagnostics;
using System.Linq;
using CHAOS;
using Chaos.Portal.Data.EF;

namespace Chaos.Portal.Logging.Database
{
	public class DatabaseLogger : ALog
	{
		#region Fields

		#endregion
		#region Properties

		#endregion
		#region Constructors

		public DatabaseLogger( string name, UUID sessionGUID, Stopwatch stopwatch, LogLevel logLevel = LogLevel.Debug ) : base(name, sessionGUID, stopwatch, logLevel)
		{

		}

		#endregion
		#region Business Logic

		public override void Commit( )
		{
			if( LogBuilder.Length == 0 )
				return;

			var t =
			new System.Threading.Thread( () =>
				{
					using( var db = new PortalEntities() )
					{
						db.Log_Create(Name, 
                                      LogLevel.ToString().ToUpper(), SessionGUID == null ? null : SessionGUID.ToByteArray(),
						              Stopwatch.ElapsedMilliseconds, 
                                      LogBuilder.ToString()).First();
					}
				});

			t.Start();
		}

		#endregion
	}
}
