using System;
using Geckon.Serialization.Xml;

namespace Geckon.Portal.Data.Dto
{
    [Document("Geckon.Portal.Data.Dto.Subscription")]
    public class Subscription : XmlSerialize
    {
        #region Properties

        public int ID { get; set; }

        [Element("GUID")]
        public Guid GUID { get; set; }

        [Element("Name")]
        public string Name { get; set; }

        [Element("DateCreated")]
        public DateTime DateCreated { get; set; }

        #endregion
        #region Construction

        public static Subscription Create( Data.Subscription subscription )
        {
            Subscription newSubscription = new Subscription();

            newSubscription.ID          = subscription.ID;
            newSubscription.GUID        = subscription.GUID;
            newSubscription.Name        = subscription.Name;
            newSubscription.DateCreated = subscription.DateCreated;

            return newSubscription;
        }

        #endregion
    }
}
