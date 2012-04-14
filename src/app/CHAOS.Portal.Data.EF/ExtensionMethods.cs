using System.Collections.Generic;
using System.Linq;

namespace CHAOS.Portal.Data.EF
{
	public static class ExtensionMethods
    {
        #region User

        public static IEnumerable<DTO.Standard.User> ToDTO( this IEnumerable<User> list )
        {
            return list.Select( ToDTO );
        }

	    public static DTO.Standard.User ToDTO( User user )
		{
			return new DTO.Standard.User( user.GUID.ToByteArray(), user.Email, user.DateCreated );
		}

        #endregion
        #region UserInfo

        public static IEnumerable<DTO.Standard.UserInfo> ToDTO( this IEnumerable<UserInfo> list )
		{ 
            return list.Select( ToDTO );
		}

        public static DTO.Standard.UserInfo ToDTO( UserInfo user )
		{
			return new DTO.Standard.UserInfo( user.GUID, user.SessionGUID, user.SystemPermission, user.Email, user.SessionDateCreated, user.DateModified );
		}

        #endregion
        #region Group

        public static IEnumerable<DTO.Standard.Group> ToDTO( this IEnumerable<Group> list )
		{ 
            return list.Select( ToDTO );
		}

        public static DTO.Standard.Group ToDTO( Group group )
		{
			return new DTO.Standard.Group( group.GUID, group.SystemPermission, group.Name, group.DateCreated );
		}

        #endregion
        #region Session

        public static IEnumerable<DTO.Standard.Session> ToDTO( this IEnumerable<Session> list )
		{ 
            return list.Select( ToDTO );
		}

        public static DTO.Standard.Session ToDTO( Session session )
		{
			return new DTO.Standard.Session( session.GUID, session.UserGUID, session.DateCreated, session.DateModified );
		}

        #endregion
        #region Module

        public static IEnumerable<DTO.Standard.Module> ToDTO( this IEnumerable<Module> list )
		{ 
            return list.Select( ToDTO );
		}

        public static DTO.Standard.Module ToDTO( Module module )
		{
			return new DTO.Standard.Module( module.ID, module.Name, module.Path, module.Configuration, module.DateCreated );
		}

        #endregion
		#region Extension

		public static IEnumerable<DTO.Standard.Extension> ToDTO( this IEnumerable<Extension> list )
		{
		    return list.Select( ToDTO );
		}

	    public static DTO.Standard.Extension ToDTO( this Extension dto )
		{
			return new DTO.Standard.Extension( dto.ID, dto.Map, dto.Path, dto.DateCreated );
		}

		#endregion
		#region SubscriptionInfo

        public static IEnumerable<DTO.Standard.SubscriptionInfo> ToDTO( this IEnumerable<SubscriptionInfo> list )
		{ 
            return list.Select( ToDTO );
		}

        public static DTO.Standard.SubscriptionInfo ToDTO( SubscriptionInfo subscriptionInfo )
		{
			return new DTO.Standard.SubscriptionInfo( subscriptionInfo.GUID, subscriptionInfo.UserGUID, subscriptionInfo.Name, subscriptionInfo.Permission, subscriptionInfo.DateCreated );
		}

		#endregion
		#region ClientSettings

        public static IEnumerable<DTO.Standard.ClientSettings> ToDTO( this IEnumerable<ClientSettings> list )
		{ 
            return list.Select( ToDTO );
		}

        public static DTO.Standard.ClientSettings ToDTO( ClientSettings clientSettings )
		{
			return new DTO.Standard.ClientSettings( clientSettings.GUID.ToByteArray(), clientSettings.Name, clientSettings.Settings, clientSettings.DateCreated );
		}

		#endregion
        #region UserSettings
        
        public static IEnumerable<DTO.Standard.UserSettings> ToDTO( this IEnumerable<UserSettings> list )
		{ 
            return list.Select( ToDTO );
		}

        public static DTO.Standard.UserSettings ToDTO( UserSettings userSettings )
		{
			return new DTO.Standard.UserSettings(userSettings.ClientSettingsGUID.ToByteArray(), userSettings.UserGUID.ToByteArray(), userSettings.Settings, userSettings.DateCreated );
		}

        #endregion
	}
}
