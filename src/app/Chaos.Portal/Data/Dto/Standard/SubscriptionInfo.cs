using System;
using CHAOS;
using CHAOS.Extensions;

namespace Chaos.Portal.Data.Dto.Standard
{
	public class SubscriptionInfo : Subscription, ISubscriptionInfo
	{
		#region Properties

		public UUID UserGUID { get; set; }
		public SubscriptionPermission Permission { get; set; }

		#endregion
		#region Constructors

		public SubscriptionInfo()
		{
			
		}

		public SubscriptionInfo( Guid guid, Guid? userGUID, string name, long? permission, DateTime dateCreated ) : base( guid, name, dateCreated )
		{
			UserGUID    = userGUID.HasValue ? userGUID.Value.ToUUID() : null;
			Permission  = (SubscriptionPermission) permission;
		}

		#endregion
	}
}
