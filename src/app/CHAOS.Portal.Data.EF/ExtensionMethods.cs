using System.Collections.Generic;

namespace CHAOS.Portal.Data.EF
{
	public static class ExtensionMethods
	{
		public static IEnumerable<DTO.User> ToDTO( this IEnumerable<User> list )
		{
			foreach( User user in list )
				yield return new DTO.User( user.GUID.ToByteArray(), user.Email, user.DateCreated );
		}

		public static IEnumerable<DTO.UserInfo> ToDTO( this IEnumerable<UserInfo> list )
		{ // TODO: Fix so it works with null SessionGUIDs
			foreach( UserInfo user in list )
				yield return new DTO.UserInfo( user.GUID.ToByteArray(), user.SessionGUID.Value.ToByteArray(), user.SystemPermission, user.Email, user.SessionDateCreated, user.DateModified );
		}

		public static IEnumerable<DTO.Group> ToDTO( this IEnumerable<Group> list )
		{ // TODO: Fix so it works with null SessionGUIDs
			foreach( Group group in list )
				yield return new DTO.Group( group.GUID.ToByteArray(), group.SystemPermission, group.Name, group.DateCreated );
		}

		public static IEnumerable<DTO.Session> ToDTO(this IEnumerable<Session> list)
		{
			foreach( Session session in list )
				yield return new DTO.Session( session.GUID.ToByteArray(), session.UserGUID.ToByteArray(), session.DateCreated, session.DateModified );
		}

		#region Extension

		public static IEnumerable<DTO.Extension> ToDTO( this IEnumerable<Extension> list )
		{
			foreach( Extension extension in list )
				yield return ToDTO( extension );
		}

		public static DTO.Extension ToDTO( this Extension dto )
		{
			return new DTO.Extension( dto.ID, dto.Map, dto.Path, dto.DateCreated );
		}

		#endregion
		#region Module

		public static IEnumerable<DTO.Module> ToDTO(this IEnumerable<Module> list)
		{
			foreach( Module module  in list )
				yield return new DTO.Module( module.ID, module.Name, module.Path, module.Configuration, module.DateCreated );
		}

		#endregion
		#region SubscriptionInfo

		public static IEnumerable<DTO.SubscriptionInfo> ToDTO(this IEnumerable<SubscriptionInfo> list)
		{
			foreach( SubscriptionInfo subscriptionInfo in list )
				yield return new DTO.SubscriptionInfo( subscriptionInfo.GUID.ToByteArray(), subscriptionInfo.UserGUID.HasValue ? subscriptionInfo.UserGUID.Value.ToByteArray() : null, subscriptionInfo.Name, subscriptionInfo.Permission, subscriptionInfo.DateCreated );
		}

		#endregion
		public static IEnumerable<DTO.UserSettings> ToDTO(this IEnumerable<UserSettings> list)
		{
			foreach( UserSettings userSettings in list )
				yield return new DTO.UserSettings(userSettings.ClientSettingsGUID.ToByteArray(), userSettings.UserGUID.ToByteArray(), userSettings.Settings, userSettings.DateCreated );
		}
	}
}
