namespace Chaos.Portal.v5.Dto
{
    using System;

    using CHAOS.Serialization;

    using Chaos.Portal.Core.Data.Model;

    public class UserInfo : AResult
    {
        [Serialize("GUID")]
        public Guid Guid { get; set; }

        [Serialize]
        public uint? SystemPermissions { get; set; }

        [Serialize]
        public string Email { get; set; }

        [Serialize]
        public DateTime? SessionDateCreated { get; set; }

        [Serialize]
        public DateTime? SessionDateModified { get; set; }

        public UserInfo()
        {
            
        }

        public UserInfo(Core.Data.Model.UserInfo userInfo)
        {
            Guid                = userInfo.Guid;
            SystemPermissions   = userInfo.SystemPermissions;
            Email               = userInfo.Email;
            SessionDateCreated  = userInfo.SessionDateCreated;
            SessionDateModified = userInfo.SessionDateModified;
        }
    }
}
