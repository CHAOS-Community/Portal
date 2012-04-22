using System;
using CHAOS.Extensions;

namespace CHAOS.Portal.DTO.Standard
{
	public class SubscriptionInfo : Subscription
	{
		#region Properties

		public UUID UserGUID { get; set; }
		public long? Permission { get; set; }

		#endregion
		#region Constructors

		public SubscriptionInfo()
		{
			
		}

		public SubscriptionInfo( Guid guid, Guid? userGUID, string name, long? permission, DateTime dateCreated ) : base( guid, name, dateCreated )
		{
			UserGUID    = userGUID.HasValue ? userGUID.Value.ToUUID() : null;
			Permission  = permission;
		}

		#endregion
	}
}
