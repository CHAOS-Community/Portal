using System;
using Geckon.Serialization;
using Geckon.Serialization.XML;

namespace CHAOS.Portal.DTO.Standard
{
	public class Module : Result
	{
		#region Properties

		[Serialize("ID")]
		public long ID { get; set; }

		[Serialize("Name")]
		public string Name { get; set; }

		[Serialize("Path")]
		public string Path { get; set; }

		[SerializeXML(false, true)]
		[Serialize("Configuration")]
		public string Configuration { get; set; }

		[Serialize("DateCreated")]
		public DateTime DateCreated { get; set; }

		#endregion
		#region Construction

		public Module()
		{
			
		}

		public Module( long id, string name, string path, string configuration, DateTime dateCreated )
		{
			ID            = id;
			Name          = name;
			Path          = path;
			Configuration = configuration;
			DateCreated   = dateCreated;
		}

		#endregion
	}
}
