using System;
using Geckon;

namespace CHAOS.Portal.Data.DTO
{
	public class SubscriptionInfo : Subscription
	{
		#region Properties

		public UUID UserGUID { get; set; }
		public long? Permission { get; set; }

		#endregion
		#region Constructors

		public SubscriptionInfo() : base()
		{
			
		}

		public SubscriptionInfo( byte[] guid, byte[] userGUID, string name, long? permission, DateTime dateCreated ) : base( guid, name, dateCreated )
		{
			UserGUID    = new UUID( userGUID );
			Permission  = permission;
		}

		#endregion
	}
}
