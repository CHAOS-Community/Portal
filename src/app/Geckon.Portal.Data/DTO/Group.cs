using System;
using Geckon.Serialization.Xml;

namespace Geckon.Portal.Data.Dto
{
    [Document("Geckon.Portal.Data.Dto.Group")]
    public class Group : XmlSerialize
    {
        #region Properties

        [Element("ID")]
        public int ID { get; set; }

        [Element("GUID")]
        public string GUID { get; set; }

        [Element("Name")]
        public string Name { get; set; }

        [Element("DateCreadted")]
        public DateTime DateCreated { get; set; }

        #endregion
        #region Construction

        public static Group Create( Data.Group group )
        {
            Group newGroup = new Group();

            newGroup.ID          = group.ID;
            newGroup.GUID        = group.GUID.ToString();
            newGroup.Name        = group.Name;
            newGroup.DateCreated = group.DateCreadted;

            return newGroup;
        }

        #endregion
    }
}
