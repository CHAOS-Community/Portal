namespace Chaos.Portal.Data.Dto
{
    using System;

    using CHAOS.Serialization;

    public class SubscriptionInfo : AResult
	{
		#region Properties

		public Guid UserGuid { get; set; }
		public SubscriptionPermission Permission { get; set; }

	    [Serialize]
	    public Guid     Guid { get; set; }

        [Serialize]
        public string   Name { get; set; }

        [Serialize]
        public DateTime DateCreated { get; set; }

        #endregion
	}
}
