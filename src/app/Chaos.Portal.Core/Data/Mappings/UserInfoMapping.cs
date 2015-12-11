namespace Chaos.Portal.Core.Data.Mappings
{
    using System.Collections.Generic;
    using System.Data;

    using CHAOS.Data;

    using Chaos.Portal.Core.Data.Model;

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
                        DateCreated         = reader.GetDateTimeNullable("DateCreated"),
                        SessionDateCreated  = reader.GetDateTimeNullable("SessionDateCreated"),
                        SessionDateModified = reader.GetDateTimeNullable("DateModified"),
                    };
            }
        }

        #endregion
    }
}