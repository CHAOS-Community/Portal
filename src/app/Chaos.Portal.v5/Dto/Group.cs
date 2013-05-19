namespace Chaos.Portal.v5.Dto
{
    using System;

    using CHAOS.Serialization;

    using Chaos.Portal.Core.Data.Model;

    public class Group : AResult
    {
        #region Properties

        [Serialize("GUID")]
        public Guid GUID { get; set; }

        [Serialize]
        public long? SystemPermission { get; set; }

        [Serialize]
        public string Name { get; set; }

        [Serialize]
        public DateTime DateCreated { get; set; }

        #endregion
        #region Construction

        public Group()
        {

        }

        public Group(Core.Data.Model.Group group)
        {
            GUID             = group.Guid;
            Name             = @group.Name;
            SystemPermission = group.SystemPermission;
            DateCreated      = group.DateCreated;
        }

        #endregion
    }
}