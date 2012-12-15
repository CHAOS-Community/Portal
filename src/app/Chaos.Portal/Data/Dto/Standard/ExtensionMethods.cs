using System.Collections.Generic;
using System.Linq;

namespace Chaos.Portal.Data.Dto.Standard
{
	public static class ExtensionMethods
    {
        #region User

        public static IEnumerable<User> ToDto( this IEnumerable<CHAOS.Portal.Data.EF.User> list )
        {
            return list.Select( ToDto );
        }

	    public static User ToDto( CHAOS.Portal.Data.EF.User user )
		{
			return new User( user.GUID.ToByteArray(), user.Email, user.DateCreated );
		}

        #endregion
        #region UserInfo

        public static IEnumerable<IUserInfo> ToDto( this IEnumerable<CHAOS.Portal.Data.EF.UserInfo> list )
		{ 
            return list.Select( ToDto );
		}

        public static IUserInfo ToDto( CHAOS.Portal.Data.EF.UserInfo user )
		{
			return new UserInfo( user.GUID, user.SessionGUID, user.SystemPermission, user.Email, user.SessionDateCreated, user.DateModified );
		}

        #endregion
        #region Group

        public static IEnumerable<IGroup> ToDto( this IEnumerable<CHAOS.Portal.Data.EF.Group> list )
		{ 
            return list.Select( ToDto );
		}

        public static IGroup ToDto( CHAOS.Portal.Data.EF.Group group )
		{
			return new Group( group.GUID, group.SystemPermission, group.Name, group.DateCreated );
		}

        #endregion
        #region Session

        public static IEnumerable<ISession> ToDto( this IEnumerable<CHAOS.Portal.Data.EF.Session> list )
		{
            return list.Select( ToDto );
		}

        public static ISession ToDto( this CHAOS.Portal.Data.EF.Session session )
		{
			return new Session( session.GUID, session.UserGUID, session.DateCreated, session.DateModified );
		}

        #endregion
        #region Module

        public static IEnumerable<Module> ToDto( this IEnumerable<CHAOS.Portal.Data.EF.Module> list )
        { 
            return list.Select( ToDto );
        }

        public static Module ToDto( CHAOS.Portal.Data.EF.Module module )
        {
            return new Module( module.ID, module.Name, module.Configuration, module.DateCreated );
        }

        #endregion
		#region SubscriptionInfo

        public static IEnumerable<ISubscriptionInfo> ToDto( this IEnumerable<CHAOS.Portal.Data.EF.SubscriptionInfo> list )
		{ 
            return list.Select( ToDto );
		}

        public static ISubscriptionInfo ToDto( CHAOS.Portal.Data.EF.SubscriptionInfo subscriptionInfo )
		{
			return new SubscriptionInfo( subscriptionInfo.GUID, subscriptionInfo.UserGUID, subscriptionInfo.Name, subscriptionInfo.Permission, subscriptionInfo.DateCreated );
		}

		#endregion
		#region ClientSettings

        public static IEnumerable<IClientSettings> ToDto( this IEnumerable<CHAOS.Portal.Data.EF.ClientSettings> list )
		{ 
            return list.Select( ToDto );
		}

        public static IClientSettings ToDto( CHAOS.Portal.Data.EF.ClientSettings clientSettings )
		{
			return new ClientSettings( clientSettings.GUID.ToByteArray(), clientSettings.Name, clientSettings.Settings, clientSettings.DateCreated );
		}

		#endregion
        #region UserSettings
        
        public static IEnumerable<UserSettings> ToDto( this IEnumerable<CHAOS.Portal.Data.EF.UserSettings> list )
		{ 
            return list.Select( ToDto );
		}

        public static UserSettings ToDto( CHAOS.Portal.Data.EF.UserSettings userSettings )
		{
			return new UserSettings(userSettings.ClientSettingsGUID.ToByteArray(), userSettings.UserGUID.ToByteArray(), userSettings.Settings, userSettings.DateCreated );
		}

        #endregion
	}
}
