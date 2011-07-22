IF( 1 = 1 )
BEGIN

	DELETE FROM [Session]
	DELETE FROM Subscription_User_Join
	DELETE FROM Subscription
	DELETE FROM AuthenticationProvider_User_Join
	DELETE FROM [Group_User_Join]
	DELETE FROM [Group]
	DELETE FROM [User]
	DELETE FROM ClientSetting
	DELETE FROM XmlType
	DELETE FROM Module
	DELETE FROM Extension
	DELETE FROM Permission
	DELETE FROM AuthenticationProvider

END

DBCC CHECKIDENT ("AuthenticationProvider", RESEED,0)
DBCC CHECKIDENT ("Subscription", RESEED,0)
DBCC CHECKIDENT ("[User]", RESEED,0)
DBCC CHECKIDENT ("[Group]", RESEED,0)
DBCC CHECKIDENT ("Module", RESEED,0)
DBCC CHECKIDENT ("Extension", RESEED,0)

DECLARE @SubscriptionIdentifier UNIQUEIDENTIFIER
SET @SubscriptionIdentifier = NEWID()

EXECUTE Subscription_Insert @GUID = @SubscriptionIdentifier, @Name = 'Geckon'
EXECUTE User_Insert @Guid = 'C0B231E9-7D98-4F52-885E-AF4837FAA352', @Firstname = 'Anonymous', @Email = 'Anonymous@Geckon.com'
EXECUTE User_Insert @Guid = 'A0B231E9-7D98-4F52-885E-AF4837FAA352', @Firstname = 'Administrator', @Email = 'admin@Geckon.com'
EXECUTE Group_Insert @GUID = 'A0B231E9-7D98-4F52-885E-AAAAAAAAAAAA', @Name = 'Administrators'
EXECUTE [AssociateUserWithGroup] @UserGUID = 'A0B231E9-7D98-4F52-885E-AF4837FAA352', @GroupGUID = 'A0B231E9-7D98-4F52-885E-AAAAAAAAAAAA', @Permission = -1
EXECUTE XmlType_Insert 1, 'XML'
EXECUTE ClientSetting_Insert 1, 1
EXECUTE Extension_Insert 'Session', 'Geckon.Portal.Extensions.Standard.SessionExtension','Geckon.Portal.Extensions.Standard.dll'
EXECUTE Extension_Insert 'User', 'Geckon.Portal.Extensions.Standard.UserExtension','Geckon.Portal.Extensions.Standard.dll'
EXECUTE Extension_Insert 'Group', 'Geckon.Portal.Extensions.Standard.GroupExtension','Geckon.Portal.Extensions.Standard.dll'
EXECUTE Extension_Insert 'Location', 'Geckon.GeoLocator.PortalExtension.LocationExtension','Geckon.GeoLocator.PortalExtension.dll'
EXECUTE Extension_Insert 'EmailPassword', 'Geckon.Portal.Extensions.Standard.EmailPasswordExtension','Geckon.Portal.Extensions.Standard.dll'
EXECUTE AuthenticationProvider_Insert 'Email Password', 'F9089905-3134-4A35-B475-9CA8EA9FDC26'

EXECUTE Module_Insert 'GeoLocator', '<Settings ConnectionString="Data Source=mssql00;Initial Catalog=GeoLocator;User ID=Application;Password=-l:bCU''S\923pc[0"/>', 'Geckon.GeoLocator.dll'

INSERT INTO [Permission]([TableIdentifier],[RightName],[Permission],[Description]) VALUES ('Subscription','Create User',1,'Permissoin to Create new users')