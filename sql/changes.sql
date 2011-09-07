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

