USE [Portal3]
GO
/****** Object:  StoredProcedure [dbo].[PopulateWithDefaultData]    Script Date: 09/27/2011 13:41:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.07.18
-- 
-- =============================================
ALTER PROCEDURE [dbo].[PopulateWithDefaultData]

AS
BEGIN
	
IF( 1 = 1 )
BEGIN

	DELETE FROM [Session]
	DELETE FROM UserSettings
	DELETE FROM Subscription_User_Join
	DELETE FROM Subscription
	DELETE FROM AuthenticationProvider_User_Join
	DELETE FROM [Group_User_Join]
	DELETE FROM [Group]
	DELETE FROM [User]
	DELETE FROM ClientSetting
	DELETE FROM Module
	DELETE FROM Extension
	DELETE FROM Permission
	DELETE FROM AuthenticationProvider

	DBCC CHECKIDENT ("AuthenticationProvider", RESEED,0)
	DBCC CHECKIDENT ("Subscription", RESEED,0)
	DBCC CHECKIDENT ("[User]", RESEED,0)
	DBCC CHECKIDENT ("[Group]", RESEED,0)
	DBCC CHECKIDENT ("Module", RESEED,0)
	DBCC CHECKIDENT ("Extension", RESEED,0)
	DBCC CHECKIDENT ("ClientSetting", RESEED,0)

	DECLARE @SubscriptionIdentifier UNIQUEIDENTIFIER
	SET @SubscriptionIdentifier = '9C4E8A99-A69B-41FD-B1C7-E28C54D1D304'

	INSERT INTO [Permission]([TableIdentifier],[RightName],[Permission],[Description]) VALUES ('Subscription','Create User',1,'Permissoin to Create new users')
	INSERT INTO [Permission]([TableIdentifier],[RightName],[Permission],[Description]) VALUES ('Subscription','Get',2,'Permissoin to Get Subscription')
	INSERT INTO [Permission]([TableIdentifier],[RightName],[Permission],[Description]) VALUES ('Subscription','Delete',4,'Permissoin to Delete Subscription')
	INSERT INTO [Permission]([TableIdentifier],[RightName],[Permission],[Description]) VALUES ('Subscription','Update',8,'Permissoin to Update Subscription')
	INSERT INTO [Permission]([TableIdentifier],[RightName],[Permission],[Description]) VALUES ('Subscription','MANAGE',16,'Permissoin to Manage Subscription')
	INSERT INTO [Permission]([TableIdentifier],[RightName],[Permission],[Description]) VALUES ('System','Create Group',1,'Permissoin to Create a Group')
	INSERT INTO [Permission]([TableIdentifier],[RightName],[Permission],[Description]) VALUES ('System','Create Subscription',2,'Permissoin to Create a Subscription')
	INSERT INTO [Permission]([TableIdentifier],[RightName],[Permission],[Description]) VALUES ('System','MANAGE',4,'Permissoin to Manage the system')
	INSERT INTO [Permission]([TableIdentifier],[RightName],[Permission],[Description]) VALUES ('Group','Delete',1,'Permissoin to Delete Group')
	INSERT INTO [Permission]([TableIdentifier],[RightName],[Permission],[Description]) VALUES ('Group','Update',2,'Permissoin to Update Group')
	INSERT INTO [Permission]([TableIdentifier],[RightName],[Permission],[Description]) VALUES ('Group','Get',4,'Permissoin to Get Group')
	INSERT INTO [Permission]([TableIdentifier],[RightName],[Permission],[Description]) VALUES ('Group','Add User',8,'Permission to Add a User to the group')
	INSERT INTO [Permission]([TableIdentifier],[RightName],[Permission],[Description]) VALUES ('Group','List Users',16,'Permission to list users in the group')

	EXECUTE Subscription_Insert @GUID = @SubscriptionIdentifier, @Name = 'Geckon'
	EXECUTE User_Insert @Guid = 'C0B231E9-7D98-4F52-885E-AF4837FAA352', @Firstname = 'Anonymous', @Email = 'Anonymous@Geckon.com'
	EXECUTE User_Insert @Guid = 'A0B231E9-7D98-4F52-885E-AF4837FAA352', @Firstname = 'Administrator', @Email = 'admin@Geckon.com'
	EXECUTE ClientSetting_Insert 'D157698A-86AC-4FDF-A304-F5EA9FB6E0F5','XML + HTML Errorcode',null
	EXECUTE Extension_Insert 'Session', 'Geckon.Portal.Extensions.Standard.SessionExtension','Geckon.Portal.Extensions.Standard.dll'
	EXECUTE Extension_Insert 'Subscription', 'Geckon.Portal.Extensions.Standard.SubscriptionExtension','Geckon.Portal.Extensions.Standard.dll'
	EXECUTE Extension_Insert 'User', 'Geckon.Portal.Extensions.Standard.UserExtension','Geckon.Portal.Extensions.Standard.dll'
	EXECUTE Extension_Insert 'Group', 'Geckon.Portal.Extensions.Standard.GroupExtension','Geckon.Portal.Extensions.Standard.dll'
	EXECUTE Extension_Insert 'Location', 'Geckon.GeoLocator.PortalExtension.LocationExtension','Geckon.GeoLocator.PortalExtension.dll'
	EXECUTE Extension_Insert 'EmailPassword', 'Geckon.Portal.Extensions.Standard.EmailPasswordExtension','Geckon.Portal.Extensions.Standard.dll'
	EXECUTE Extension_Insert 'Folder', 'Geckon.MCM.Extension.Folder.FolderExtension','Geckon.MCM.Extension.dll'
	EXECUTE Extension_Insert 'FolderType', 'Geckon.MCM.Extension.FolderType.FolderTypeExtension','Geckon.MCM.Extension.dll'
	EXECUTE Extension_Insert 'FormatType', 'Geckon.MCM.Extension.FormatType.FormatTypeExtension','Geckon.MCM.Extension.dll'
	EXECUTE Extension_Insert 'Language', 'Geckon.MCM.Extension.Language.LanguageExtension','Geckon.MCM.Extension.dll'
	EXECUTE Extension_Insert 'ObjectRelationType', 'Geckon.MCM.Extension.ObjectRelationType.ObjectRelationTypeExtension','Geckon.MCM.Extension.dll'
	EXECUTE Extension_Insert 'ObjectType', 'Geckon.MCM.Extension.ObjectType.ObjectTypeExtension','Geckon.MCM.Extension.dll'
	EXECUTE Extension_Insert 'SecureCookie', 'Geckon.Portal.CHAOS.SecureCookieExtension.Standard.SecureCookieExtension','SecureCookieExtension.dll'
	
	EXECUTE AuthenticationProvider_Insert 'Email Password', 'F9089905-3134-4A35-B475-9CA8EA9FDC26'
	EXECUTE User_AssociateWithAuthenticationProvider @UserGUID = 'A0B231E9-7D98-4F52-885E-AF4837FAA352', @AuthenticationProviderGUID = 'F9089905-3134-4A35-B475-9CA8EA9FDC26', @UniqueIdentifier = '24ebbdee2640cdec50550a6c4bed6d3ab731342b'
	EXECUTE Module_Insert 'GeoLocator', '<Settings ConnectionString="Data Source=10.4.0.1;Initial Catalog=GeoLocator;User ID=Application;Password=-l:bCU''S\923pc[0"/>', 'Geckon.GeoLocator.dll'
	EXECUTE Module_Insert 'MCM', '<Settings ConnectionString="Data Source=10.4.0.2;Initial Catalog=MCM3;Persist Security Info=True;User ID=sa;Password=GECKONpbvu7000"/>', 'Geckon.MCM.Module.dll'
	EXECUTE Module_Insert 'SecureCookie', '<Settings ConnectionString="Data Source=10.4.0.2;Initial Catalog=SecureCookie;Persist Security Info=True;User ID=sa;Password=GECKONpbvu7000"/>', 'SecureCookieModule.dll'
	EXECUTE [UserSettings_Create] @UserGUID = 'A0B231E9-7D98-4F52-885E-AF4837FAA352' ,@ClientSettingsGUID = 'D157698A-86AC-4FDF-A304-F5EA9FB6E0F5' ,@Setting = '<xml />' 
	INSERT INTO [Subscription_User_Join]([SubscriptionID],[UserID],[Permission],[DateCreated]) VALUES (1,2,-1,GETDATE())

	EXECUTE Group_Insert @GUID = 'A0B231E9-7D98-4F52-885E-AAAAAAAAAAAA', @Name = 'Administrators', @SystemPermission = -1
	
	INSERT INTO [Group_User_Join] ([GroupID],[UserID],[Permission],[DateCreated])VALUES (1,2,-1,GETDATE())
	
END

END