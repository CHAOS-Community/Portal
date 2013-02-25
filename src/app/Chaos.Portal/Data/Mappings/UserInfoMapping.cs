namespace Chaos.Portal.Data.Mappings
{
    using System.Collections.Generic;
    using System.Data;

    using CHAOS.Data;

    using Chaos.Portal.Data.Dto.Standard;

    public class UserInfoMapping : IReaderMapping<UserInfo>
    {
        #region Implementation of IReaderMapping<out UserInfo>

        public IEnumerable<UserInfo> Map(IDataReader reader)
        {
            while(reader.Read())
            {
                yield return new UserInfo
                    {
                        Guid                = reader.GetGuid("Guid"),
                        SessionGuid         = reader.GetGuidNullable("SessionGUID"),
                        SystemPermissions   = reader.GetUint32Nullable("SystemPermission"),
                        Email               = reader.GetString("Email"),
                        SessionDateCreated  = reader.GetDateTimeNullable("SessionDateCreated"),
                        SessionDateModified = reader.GetDateTimeNullable("DateModified"),
                    };
            }
        }

        #endregion
    }
}