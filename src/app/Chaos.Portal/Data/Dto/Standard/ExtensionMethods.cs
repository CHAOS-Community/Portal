namespace Chaos.Portal.Data.Dto.Standard
{
    using System.Collections.Generic;
    using System.Linq;

	public static class ExtensionMethods
    {
        #region User

        public static IEnumerable<User> ToDto(this IEnumerable<EF.User> list)
        {
            return list.Select( ToDto );
        }

	    public static User ToDto( EF.User user )
		{
			return new User( user.GUID.ToByteArray(), user.Email, user.DateCreated );
		}

        #endregion
        #region UserInfo

        public static IEnumerable<IUserInfo> ToDto(this IEnumerable<EF.UserInfo> list)
		{ 
            return list.Select( ToDto );
		}

        public static IUserInfo ToDto( EF.UserInfo user )
		{
            return new UserInfo( user.GUID, user.SessionGUID, user.SystemPermission, user.Email, user.SessionDateCreated, user.DateModified );
		}

        #endregion
        #region Group

        public static IEnumerable<IGroup> ToDto( this IEnumerable<EF.Group> list )
		{ 
            return list.Select( ToDto );
		}

        public static IGroup ToDto( EF.Group group )
		{
            return new Group( group.GUID, group.SystemPermission, group.Name, group.DateCreated );
		}

        #endregion
        #region Session

        public static IEnumerable<ISession> ToDto( this IEnumerable<EF.Session> list )
		{
            return list.Select( ToDto );
		}

        public static ISession ToDto( this EF.Session session )
		{
            return new Session( session.GUID, session.UserGUID, session.DateCreated, session.DateModified );
		}

        #endregion
        #region Module

        public static IEnumerable<Module> ToDto( this IEnumerable<EF.Module> list )
        { 
            return list.Select( ToDto );
        }

        public static Module ToDto(EF.Module module )
        {
            return new Module( module.ID, module.Name, module.Configuration, module.DateCreated );
        }

        #endregion
		#region SubscriptionInfo

        public static IEnumerable<ISubscriptionInfo> ToDto( this IEnumerable<EF.SubscriptionInfo> list )
		{ 
            return list.Select( ToDto );
		}

        public static ISubscriptionInfo ToDto(EF.SubscriptionInfo subscriptionInfo )
		{
            return new SubscriptionInfo( subscriptionInfo.GUID, subscriptionInfo.UserGUID, subscriptionInfo.Name, subscriptionInfo.Permission, subscriptionInfo.DateCreated );
		}

		#endregion
		#region ClientSettings

        public static IEnumerable<IClientSettings> ToDto( this IEnumerable<EF.ClientSettings> list )
		{ 
            return list.Select( ToDto );
		}

        public static IClientSettings ToDto(EF.ClientSettings clientSettings )
		{
            return new ClientSettings( clientSettings.GUID.ToByteArray(), clientSettings.Name, clientSettings.Settings, clientSettings.DateCreated );
		}

		#endregion
        #region UserSettings
        
        public static IEnumerable<UserSettings> ToDto( this IEnumerable<EF.UserSettings> list )
		{ 
            return list.Select( ToDto );
		}

        public static UserSettings ToDto(EF.UserSettings userSettings )
		{
            return new UserSettings( userSettings.ClientSettingsGUID.ToByteArray(), userSettings.UserGUID.ToByteArray(), userSettings.Settings, userSettings.DateCreated );
		}

        #endregion
	}
}
