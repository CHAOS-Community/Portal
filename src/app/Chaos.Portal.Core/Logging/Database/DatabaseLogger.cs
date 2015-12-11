namespace Chaos.Portal.Core.Logging.Database
{
    using Chaos.Portal.Core.Data;

    public class DatabaseLogger : ALog
	{
		#region Fields

	    private readonly IPortalRepository _portalRepository;

		#endregion
		#region Properties

		#endregion
		#region Constructors

		public DatabaseLogger(IPortalRepository portalRepository)
		{
		    _portalRepository = portalRepository;
		}

		#endregion
		#region Business Logic

		public override void Commit( )
		{
			if( LogBuilder.Length == 0 )
				return;

			var t =
			new System.Threading.Thread( () => _portalRepository.LogCreate(Name,
			                                                               SessionGuid,
			                                                               LogLevel.ToString().ToUpper(),
			                                                               Stopwatch.ElapsedMilliseconds,
			                                                               LogBuilder.ToString()));

			t.Start();
		}

		#endregion
	}
}
