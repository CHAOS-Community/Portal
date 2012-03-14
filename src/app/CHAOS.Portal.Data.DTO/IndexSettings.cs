using System;

namespace CHAOS.Portal.Data.DTO
{
	public class IndexSettings
	{
		#region Properties

		public long ID { get; set; }
		public long ModuleID { get; set; }
		public string Settings { get; set; }
		public DateTime DateCreated { get; set; }

		#endregion
		#region Constructor

		public IndexSettings()
		{
			
		}

		public IndexSettings( long id, long moduleID, string settings, DateTime dateCreated )
		{
			ID          = id;
			ModuleID    = moduleID;
			Settings    = settings;
			DateCreated = dateCreated;
		}

		#endregion
	}
}
