using System;
using Geckon.Portal.Data.Result.Standard;
using Geckon.Serialization;

namespace CHAOS.Portal.Data.DTO
{
	public class Extension : Result
	{
		#region Properties

		[Serialize("ID")]
		public long ID{ get; set; }
		
		[Serialize("Map")]
		public string Map{ get; set; }
		
		[Serialize("Path")]
		public string Path{ get; set; }

		[Serialize("DateCreated")]
		public DateTime DateCreated{ get; set; }

		#endregion
		#region Constructor

		public Extension( long id, string map, string path, DateTime dateCreated )
		{
			ID          = id;
			Map         = map;
			Path        = path;
			DateCreated = dateCreated;
		}

		public Extension()
		{
			
		}

		#endregion
	}
}
