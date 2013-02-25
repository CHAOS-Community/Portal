namespace Chaos.Portal.Data.Dto.Standard
{
    using System;

	public class SubscriptionInfo : Subscription, ISubscriptionInfo
	{
		#region Properties

		public Guid UserGuid { get; set; }
		public SubscriptionPermission Permission { get; set; }

		#endregion
	}
}
