namespace Chaos.Portal.Core.Data.Model
{
    using System;

    using CHAOS.Serialization;
    using CHAOS.Serialization.XML;

    public class Module : AResult
	{
		#region Properties

		[Serialize("ID")]
		public long ID { get; set; }

		[Serialize("Name")]
		public string Name { get; set; }

		[SerializeXML(false, true)]
		[Serialize("Configuration")]
		public string Configuration { get; set; }

		[Serialize("DateCreated")]
		public DateTime DateCreated { get; set; }

		#endregion
		#region Construction

		public Module() : this(long.MinValue,null,null,DateTime.MinValue)
		{
			
		}

		public Module( long id, string name, string configuration, DateTime dateCreated )
		{
			ID            = id;
			Name          = name;
			Configuration = configuration;
			DateCreated   = dateCreated;
		}

		#endregion
	}
}
