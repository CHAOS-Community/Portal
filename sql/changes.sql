CREATE TABLE [dbo].UserSettings(
	[ClientSettingID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Setting] [xml] NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_UserSettings] PRIMARY KEY CLUSTERED 
(
	[ClientSettingID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].UserSettings  WITH CHECK ADD  CONSTRAINT [FK_UserSettings_ClientSetting] FOREIGN KEY([ClientSettingID])
REFERENCES [dbo].[ClientSetting] ([ID])
GO

ALTER TABLE [dbo].UserSettings CHECK CONSTRAINT [FK_UserSettings_ClientSetting]
GO

ALTER TABLE [dbo].UserSettings  WITH CHECK ADD  CONSTRAINT [FK_UserSettings_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO

ALTER TABLE [dbo].UserSettings CHECK CONSTRAINT [FK_UserSettings_User]
GO

ALTER TABLE [dbo].UserSettings ADD  CONSTRAINT [DF_UserSettings_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO

-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.09.07
--				This SP is used to create a user setting
-- =============================================
CREATE PROCEDURE UserSettings_Create 
	@UserID				int				 = null,
	@UserGUID			uniqueidentifier = null,
	@ClientSettingID	int,
	@Setting			xml
AS
BEGIN

	IF( @UserID IS NULL AND @UserGUID IS NULL )
		RETURN -10

	IF( @UserGUID IS NOT NULL )
		SELECT	@UserID = ID
		  FROM	[User]
		 WHERE	[GUID] = @UserGUID

	INSERT INTO	[UserSettings]([ClientSettingID],[UserID],[Setting],[DateCreated])
		 VALUES	(@ClientSettingID ,@UserID ,@Setting,GETDATE())

	RETURN @@ROWCOUNT
	
END
GO

-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.09.07
--				This SP is used to update a user setting
-- =============================================
CREATE PROCEDURE UserSettings_Update
	@UserID				int				 = null,
	@UserGUID			uniqueidentifier = null,
	@ClientSettingID	int,
	@NewSetting			xml
AS
BEGIN

	IF( @UserID IS NULL AND @UserGUID IS NULL )
		RETURN -10

	IF( @UserGUID IS NOT NULL )
		SELECT	@UserID = ID
		  FROM	[User]
		 WHERE	[GUID] = @UserGUID

	UPDATE	[UserSettings]
	   SET	[Setting] = @NewSetting
	 WHERE	ClientSettingID = @ClientSettingID AND
			UserID = @UserID

	RETURN @@ROWCOUNT

END
GO

-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.09.07
--				This SP is used to delete a user setting
-- =============================================
CREATE PROCEDURE UserSettings_Delete
	@UserID				int				 = null,
	@UserGUID			uniqueidentifier = null,
	@ClientSettingID	int
AS
BEGIN

	IF( @UserID IS NULL AND @UserGUID IS NULL )
		RETURN -10

	IF( @UserGUID IS NOT NULL )
		SELECT	@UserID = ID
		  FROM	[User]
		 WHERE	[GUID] = @UserGUID

	DELETE	UserSettings
	 WHERE	ClientSettingID = @ClientSettingID AND
			UserID          = @UserID

	RETURN @@ROWCOUNT

END
GO

-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.09.07
--				This SP is used to GET a user setting
-- =============================================
CREATE PROCEDURE UserSettings_Get
	@UserID				int				 = null,
	@UserGUID			uniqueidentifier = null,
	@ClientSettingID	int
AS
BEGIN

	IF( @UserID IS NULL AND @UserGUID IS NULL )
		RETURN -10

	IF( @UserGUID IS NOT NULL )
		SELECT	@UserID = ID
		  FROM	[User]
		 WHERE	[GUID] = @UserGUID

	SELECT	*
	  FROM	UserSettings
	 WHERE	ClientSettingID = @ClientSettingID AND
			UserID          = @UserID

END
GO

ALTER TABLE [dbo].[Session] DROP CONSTRAINT [FK_Session_ClientSetting]
GO

ALTER TABLE [dbo].[Session] DROP COLUMN ClientSettingID
GO

ALTER TABLE [dbo].[ClientSetting] DROP CONSTRAINT [FK_ClientSetting_XmlType]
GO

ALTER TABLE [dbo].[ClientSetting] DROP COLUMN XmlTypeID
GO

DROP TABLE [dbo].[XmlType]
GO

ALTER VIEW [dbo].[SessionInfo]
AS
SELECT     SessionID, UserID, DateCreated, DateModified, DATEDIFF(minute, DateModified, GETDATE()) AS MinutesSinceRenewal
FROM         dbo.Session
GO

ALTER VIEW [dbo].[UserInfo]
AS
SELECT     dbo.[User].ID, dbo.[User].GUID, dbo.Session.SessionID, dbo.GetUsersHighestSystemPermission(dbo.[User].ID) AS SystemPermission, dbo.[User].Firstname, 
                      dbo.[User].Middlename, dbo.[User].Lastname, dbo.[User].Email, dbo.Session.DateModified, 
                      dbo.Session.DateCreated AS SessionDateCreated
FROM         dbo.[User] LEFT OUTER JOIN
                      dbo.Session ON dbo.[User].ID = dbo.Session.UserID
GO

DROP PROCEDURE [dbo].[XmlType_Insert]
GO

ALTER TABLE [dbo].[UserSettings] DROP CONSTRAINT [FK_UserSettings_ClientSetting]
GO

ALTER TABLE [dbo].[ClientSetting] DROP CONSTRAINT [DF_ClientSetting_DateCreated]
GO

DROP TABLE [dbo].[ClientSetting]
GO

CREATE TABLE [dbo].[ClientSetting](
	[ID] int IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL,
	[Title] varchar(255) NOT NULL,
	[Xml] xml NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_ClientSetting] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ClientSetting] ADD  CONSTRAINT [DF_ClientSetting_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO

INSERT INTO [ClientSetting]([GUID],[Title],[DateCreated]) VALUES('D157698A-86AC-4FDF-A304-F5EA9FB6E0F5','XML + HTML Errorcode',GETDATE())
GO

ALTER TABLE [dbo].UserSettings  WITH CHECK ADD  CONSTRAINT [FK_UserSettings_ClientSetting] FOREIGN KEY([ClientSettingID])
REFERENCES [dbo].[ClientSetting] ([ID])
GO

ALTER TABLE [dbo].UserSettings CHECK CONSTRAINT [FK_UserSettings_ClientSetting]
GO

-- =============================================
-- Author:		Jesper Fyhr	Knudsen
-- Create date: 2010.06.13
-- Description:	This stored procedure inserts a ClientSetting
-- =============================================
ALTER PROCEDURE [dbo].[ClientSetting_Insert]
	@GUID	uniqueidentifier,
	@Title	varchar(255),
	@Xml	xml = null
AS
BEGIN

	INSERT INTO [ClientSetting]([GUID],[Title],[Xml],DateCreated)
		 VALUES (@GUID,@Title,@Xml,GETDATE())
		 
	RETURN @@IDENTITY

END
GO

-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2010.06.13
-- Description:	This stored procedure inserts a new session
-- =============================================
ALTER PROCEDURE [dbo].[Session_Insert]
	@SessionID			uniqueidentifier = NULL,
	@UserGUID			uniqueidentifier
AS
BEGIN

	IF( @SessionID IS NULL )
		SET @SessionID = NEWID()

	DECLARE @UserID INT
	
	SELECT	@UserID = ID
	  FROM	[User]
	 WHERE	[User].GUID = @UserGUID

	INSERT
	  INTO	[Session]([SessionID],[UserID])
	VALUES	(@SessionID,@UserID)
	
	SELECT	*
	  FROM	[Session]
	 WHERE	[Session].SessionID = @SessionID

END
GO

-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2010.06.29
-- Description:	This stored procedure will delete
-- =============================================
ALTER PROCEDURE [dbo].[Session_Delete]
	@SessionID			uniqueidentifier	= null,
	@UserID				int					= null
AS
BEGIN

	IF( @SessionID IS NULL AND @UserID IS NULL )
		RAISERROR ('Either @SessionID or @UserID must be set', 16, 1)
	
	DELETE FROM	[Session]
		  WHERE ( @SessionID IS NULL OR [Session].SessionID = @SessionID ) AND
				( @UserID IS NULL OR [Session].UserID = @UserID )

	RETURN @@ROWCOUNT
	
END
GO

-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2010.07.01
-- Description:	This stored procedure is used to select sessions
-- =============================================
ALTER PROCEDURE [dbo].[Session_Get]
	@SessionID			uniqueidentifier = NULL,
	@UserID				int				 = NULL,
	@PageIndex			int				 = 0,
	@PageSize			int				 = 10,
	@TotalCount			int	output

AS
BEGIN

	DELETE
	  FROM	[Session]
	 WHERE	SessionID IN ( SELECT	SessionID
							 FROM	SessionInfo
							WHERE	MinutesSinceRenewal > 20 )

	IF( @PageIndex IS NULL )
		SET @PageIndex = 0
		
	IF( @PageSize IS NULL )
		SET @PageSize = 10;

	DECLARE @PagedResults AS TABLE (
		[RowNumber]			int,
		[TotalCount]		int,
	    [SessionID]			uniqueidentifier,
        [UserID]			int,
        [DateCreated]		datetime,
        [DateModified]		datetime
	);

	WITH OrdersRN AS
	(
		SELECT ROW_NUMBER() OVER(ORDER BY SessionID, SessionID) AS RowNumber,
			   COUNT(*) OVER() AS TotalCount,
			   [SessionID],[UserID],[DateCreated],[DateModified]
		  FROM	[Session]
		 WHERE	( @SessionID IS NULL OR [Session].SessionID = @SessionID ) AND
				( @UserID IS NULL OR [Session].UserID = @UserID )
	)

	INSERT INTO @PagedResults
		SELECT	* 
		  FROM	OrdersRN
		 WHERE RowNumber BETWEEN (@PageIndex) * @PageSize + 1 
					     AND (@PageIndex + 1) * @PageSize
	  
	SELECT TOP 1 @TotalCount = TotalCount
	  FROM	@PagedResults
	  
	SELECT	*
	  FROM	@PagedResults  
END
GO

-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2010.07.07
-- Description:	This stored procedure is used to update a session
-- =============================================
ALTER PROCEDURE [dbo].[Session_Update]
	@SessionID				uniqueidentifier = NULL,
	@UserGUID				uniqueidentifier = NULL,
	@WhereSessionID			uniqueidentifier = NULL,
	@WhereUserGUID			uniqueidentifier = NULL
AS
BEGIN

	IF( @WhereSessionID IS NULL AND @WhereUserGUID IS NULL )
		RETURN -10

	DECLARE @UserID INT
	DECLARE @WhereUserID INT
	
	SELECT	@UserID = [User].ID
	  FROM	[User]
	 WHERE	[User].GUID = @UserGUID
		
	SELECT	@WhereUserID = [User].ID
	  FROM	[User]
	 WHERE	[User].GUID = @WhereUserGUID

	UPDATE [Session]
	   SET [SessionID]       = ISNULL(@SessionID,[SessionID])
		  ,[UserID]          = ISNULL(@UserID,[UserID] )
		  ,[DateModified]    = getdate()
	 WHERE ( @WhereSessionID IS NULL OR [Session].SessionID = @WhereSessionID ) AND
		   ( @WhereUserID IS NULL OR [Session].UserID = @WhereUserID )
		   
	 SELECT	*
	   FROM	[Session]
	  WHERE ( @WhereSessionID IS NULL OR [Session].SessionID = @WhereSessionID ) AND
		    ( @WhereUserID IS NULL OR [Session].UserID = @WhereUserID )

END
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
	EXECUTE AuthenticationProvider_Insert 'Email Password', 'F9089905-3134-4A35-B475-9CA8EA9FDC26'
	EXECUTE User_AssociateWithAuthenticationProvider @UserGUID = 'A0B231E9-7D98-4F52-885E-AF4837FAA352', @AuthenticationProviderGUID = 'F9089905-3134-4A35-B475-9CA8EA9FDC26', @UniqueIdentifier = '24ebbdee2640cdec50550a6c4bed6d3ab731342b'
	EXECUTE Module_Insert 'GeoLocator', '<Settings ConnectionString="Data Source=10.4.0.1;Initial Catalog=GeoLocator;User ID=Application;Password=-l:bCU''S\923pc[0"/>', 'Geckon.GeoLocator.dll'
	EXECUTE Module_Insert 'MCM', '<Settings ConnectionString="Data Source=192.168.56.102;Initial Catalog=MCM;Persist Security Info=True;User ID=sa;Password=GECKONpbvu7000"/>', 'Geckon.MCM.Module.dll'
	EXECUTE [UserSettings_Create] @UserGUID = 'A0B231E9-7D98-4F52-885E-AF4837FAA352' ,@ClientSettingID = 1 ,@Setting = '<xml />' 
	INSERT INTO [Subscription_User_Join]([SubscriptionID],[UserID],[Permission],[DateCreated]) VALUES (1,2,-1,GETDATE())

	EXECUTE Group_Insert @GUID = 'A0B231E9-7D98-4F52-885E-AAAAAAAAAAAA', @Name = 'Administrators', @SystemPermission = -1
	
	INSERT INTO [Group_User_Join] ([GroupID],[UserID],[Permission],[DateCreated])VALUES (1,2,-1,GETDATE())
	
END

END
GO

-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.09.12
--				This SP is used to get ClientSettings
-- =============================================
CREATE PROCEDURE ClientSettings_Get
	@ClientSettingsID	uniqueidentifier
AS
BEGIN

	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT	*
	  FROM	ClientSetting
	 WHERE	ClientSetting.[GUID] = @ClientSettingsID
END
GO

-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.09.07
--				This SP is used to GET a user setting
-- =============================================
ALTER PROCEDURE [dbo].[UserSettings_Get]
	@UserID				int				 = null,
	@UserGUID			uniqueidentifier = null,
	@ClientSettingsGUID	uniqueidentifier
AS
BEGIN

	IF( @UserID IS NULL AND @UserGUID IS NULL )
		RETURN -10

	IF( @UserGUID IS NOT NULL )
		SELECT	@UserID = ID
		  FROM	[User]
		 WHERE	[GUID] = @UserGUID

	DECLARE @ClientSettingID INT

	SELECT	@ClientSettingID = [ID]
	  FROM	ClientSetting
	 WHERE	[GUID] = @ClientSettingsGUID

	SELECT	*
	  FROM	UserSettings
	 WHERE	ClientSettingID = @ClientSettingID AND
			UserID          = @UserID

END
GO

-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.09.07
--				This SP is used to create a user setting
-- =============================================
ALTER PROCEDURE [dbo].[UserSettings_Create] 
	@UserID				int				 = null,
	@UserGUID			uniqueidentifier = null,
	@ClientSettingsGUID	uniqueidentifier,
	@Setting			xml
AS
BEGIN

	IF( @UserID IS NULL AND @UserGUID IS NULL )
		RETURN -10

	IF( @UserGUID IS NOT NULL )
		SELECT	@UserID = ID
		  FROM	[User]
		 WHERE	[GUID] = @UserGUID

	DECLARE @ClientSettingID INT

	SELECT	@ClientSettingID = [ID]
	  FROM	ClientSetting
	 WHERE	[GUID] = @ClientSettingsGUID

	INSERT INTO	[UserSettings]([ClientSettingID],[UserID],[Setting],[DateCreated])
		 VALUES	(@ClientSettingID ,@UserID ,@Setting,GETDATE())

	RETURN @@ROWCOUNT

END
GO

-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.09.07
--				This SP is used to delete a user setting
-- =============================================
ALTER PROCEDURE [dbo].[UserSettings_Delete]
	@UserID				int				 = null,
	@UserGUID			uniqueidentifier = null,
	@ClientSettingsGUID	uniqueidentifier
AS
BEGIN

	IF( @UserID IS NULL AND @UserGUID IS NULL )
		RETURN -10

	IF( @UserGUID IS NOT NULL )
		SELECT	@UserID = ID
		  FROM	[User]
		 WHERE	[GUID] = @UserGUID

	DECLARE @ClientSettingID INT

	SELECT	@ClientSettingID = [ID]
	  FROM	ClientSetting
	 WHERE	[GUID] = @ClientSettingsGUID

	DELETE	UserSettings
	 WHERE	ClientSettingID = @ClientSettingID AND
			UserID          = @UserID

	RETURN @@ROWCOUNT

END
GO

-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.09.07
--				This SP is used to update a user setting
-- =============================================
ALTER PROCEDURE [dbo].[UserSettings_Update]
	@UserID				int				 = null,
	@UserGUID			uniqueidentifier = null,
	@ClientSettingsGUID	uniqueidentifier,
	@NewSetting			xml
AS
BEGIN

	IF( @UserID IS NULL AND @UserGUID IS NULL )
		RETURN -10

	IF( @UserGUID IS NOT NULL )
		SELECT	@UserID = ID
		  FROM	[User]
		 WHERE	[GUID] = @UserGUID

	DECLARE @ClientSettingID INT

	SELECT	@ClientSettingID = [ID]
	  FROM	ClientSetting
	 WHERE	[GUID] = @ClientSettingsGUID

	UPDATE	[UserSettings]
	   SET	[Setting] = @NewSetting
	 WHERE	ClientSettingID = @ClientSettingID AND
			UserID = @UserID

	RETURN @@ROWCOUNT

END
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
	EXECUTE AuthenticationProvider_Insert 'Email Password', 'F9089905-3134-4A35-B475-9CA8EA9FDC26'
	EXECUTE User_AssociateWithAuthenticationProvider @UserGUID = 'A0B231E9-7D98-4F52-885E-AF4837FAA352', @AuthenticationProviderGUID = 'F9089905-3134-4A35-B475-9CA8EA9FDC26', @UniqueIdentifier = '24ebbdee2640cdec50550a6c4bed6d3ab731342b'
	EXECUTE Module_Insert 'GeoLocator', '<Settings ConnectionString="Data Source=10.4.0.1;Initial Catalog=GeoLocator;User ID=Application;Password=-l:bCU''S\923pc[0"/>', 'Geckon.GeoLocator.dll'
	EXECUTE Module_Insert 'MCM', '<Settings ConnectionString="Data Source=192.168.56.102;Initial Catalog=MCM;Persist Security Info=True;User ID=sa;Password=GECKONpbvu7000"/>', 'Geckon.MCM.Module.dll'
	EXECUTE [UserSettings_Create] @UserGUID = 'A0B231E9-7D98-4F52-885E-AF4837FAA352' ,@ClientSettingsGUID = 'D157698A-86AC-4FDF-A304-F5EA9FB6E0F5' ,@Setting = '<xml />' 
	INSERT INTO [Subscription_User_Join]([SubscriptionID],[UserID],[Permission],[DateCreated]) VALUES (1,2,-1,GETDATE())

	EXECUTE Group_Insert @GUID = 'A0B231E9-7D98-4F52-885E-AAAAAAAAAAAA', @Name = 'Administrators', @SystemPermission = -1
	
	INSERT INTO [Group_User_Join] ([GroupID],[UserID],[Permission],[DateCreated])VALUES (1,2,-1,GETDATE())
	
END

END