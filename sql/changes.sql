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

USE [Portal]
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
	@Title	varchar(255)
AS
BEGIN

	INSERT INTO [ClientSetting]([GUID],[Title],DateCreated)
		 VALUES (@GUID,@Title,GETDATE())
		 
	RETURN @@IDENTITY

END

