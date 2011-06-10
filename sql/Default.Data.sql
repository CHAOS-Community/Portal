IF( 1 = 1 )
BEGIN

	DELETE FROM [Session]
	DELETE FROM Subscription_User_Join
	DELETE FROM Subscription
	DELETE FROM [User]
	DELETE FROM ClientSetting
	DELETE FROM XmlType
	DELETE FROM Module
	DELETE FROM Entrypoint

END

DBCC CHECKIDENT ("Subscription", RESEED,0)
DBCC CHECKIDENT ("[User]", RESEED,0)
DBCC CHECKIDENT ("Module", RESEED,0)
DBCC CHECKIDENT ("Entrypoint", RESEED,0)

DECLARE @SubscriptionIdentifier UNIQUEIDENTIFIER
SET @SubscriptionIdentifier = NEWID()

EXECUTE Subscription_Insert @GUID = @SubscriptionIdentifier, @Name = 'Geckon'
EXECUTE User_Insert @Firstname = 'Anonymous', @Email = 'Anonymous@Geckon.com'
EXECUTE XmlType_Insert 1, 'XML'
EXECUTE ClientSetting_Insert 1, 1
EXECUTE Module_Insert 'Geckon Portal',null,'Geckon.Portal.Module.Standard.dll'
EXECUTE Module_Insert 'Geckon GeoLocator','<ConnectionStrings Name="GeoLocatorDB" ConnectionString="Data Source=MSSQL00;Initial Catalog=GeoLocator;Persist Security Info=True;User ID=Application;Password=GECKONpbvu7000" />','Geckon.GeoLocator.dll'
EXECUTE Entrypoint_Insert 'Session','Geckon.Portal.Extensions.Session.Standard.dll'
EXECUTE Entrypoint_Insert 'GeoLocator','Geckon.Portal.Extensions.GeoLocator.dll'

INSERT INTO [Permission]([TableIdentifier],[RightName],[Permission],[Description]) VALUES ('Subscription','Create User',1,'Permissoin to Create new users')