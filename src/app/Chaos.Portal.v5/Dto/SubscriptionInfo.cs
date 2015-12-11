namespace Chaos.Portal.v5.Dto
{
    using System;

    using CHAOS.Serialization;

    using Chaos.Portal.Core.Data.Model;

    public class SubscriptionInfo : AResult
    {
        #region Properties

        [Serialize("GUID")]
        public Guid GUID { get; set; }

        [Serialize]
        public string Name { get; set; }

        [Serialize]
        public DateTime DateCreated { get; set; }

        public Guid UserGUID { get; set; }
        public SubscriptionPermission Permission { get; set; }

        #endregion
        #region Constructors

        public SubscriptionInfo()
        {

        }

        public SubscriptionInfo(Core.Data.Model.SubscriptionInfo subscriptionInfo)
        {
            GUID        = subscriptionInfo.Guid;
            Name        = subscriptionInfo.Name;
            DateCreated = subscriptionInfo.DateCreated;
            UserGUID    = subscriptionInfo.UserGuid;
            Permission  = subscriptionInfo.Permission;
        }

        #endregion
    }
}