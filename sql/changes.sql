IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserSettings_ClientSetting]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserSettings]'))
ALTER TABLE [dbo].[UserSettings] DROP CONSTRAINT [FK_UserSettings_ClientSetting]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserSettings_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[UserSettings]'))
ALTER TABLE [dbo].[UserSettings] DROP CONSTRAINT [FK_UserSettings_User]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_UserSettings_DateCreated]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[UserSettings] DROP CONSTRAINT [DF_UserSettings_DateCreated]
END

GO

/****** Object:  Table [dbo].[UserSettings]    Script Date: 11/23/2011 14:14:04 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserSettings]') AND type in (N'U'))
DROP TABLE [dbo].[UserSettings]
GO

/****** Object:  Table [dbo].[UserSettings]    Script Date: 11/23/2011 14:14:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserSettings](
	[ClientSettingGUID] [uniqueidentifier] NOT NULL,
	[UserGUID] [uniqueidentifier] NOT NULL,
	[Setting] [xml] NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_UserSettings] PRIMARY KEY CLUSTERED 
(
	[ClientSettingGUID] ASC,
	[UserGUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UserSettings]  WITH CHECK ADD  CONSTRAINT [FK_UserSettings_User] FOREIGN KEY([UserGUID])
REFERENCES [dbo].[User] ([GUID])
GO

ALTER TABLE [dbo].[UserSettings] CHECK CONSTRAINT [FK_UserSettings_User]
GO

DROP TABLE [ClientSetting]

CREATE TABLE [dbo].[ClientSetting](
	[GUID] [uniqueidentifier] NOT NULL,
	[Title] [varchar](255) NOT NULL,
	[Xml] [xml] NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_ClientSetting] PRIMARY KEY CLUSTERED 
(
	[GUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UserSettings]  WITH CHECK ADD  CONSTRAINT [FK_UserSettings_ClientSetting] FOREIGN KEY([ClientSettingGUID])
REFERENCES [dbo].[ClientSetting] ([GUID])
GO

ALTER TABLE [dbo].[UserSettings] CHECK CONSTRAINT [FK_UserSettings_ClientSetting]
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
		 
END
GO

-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.09.12
--				This SP is used to get ClientSettings
-- =============================================
ALTER PROCEDURE [dbo].[ClientSettings_Get]
	@ClientSettingsGUID	uniqueidentifier
AS
BEGIN

	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT	*
	  FROM	ClientSetting
	 WHERE	ClientSetting.[GUID] = @ClientSettingsGUID
END
GO

-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.09.07
--				This SP is used to create a user setting
-- =============================================
ALTER PROCEDURE [dbo].[UserSettings_Create] 
	@UserGUID			uniqueidentifier = null,
	@ClientSettingsGUID	uniqueidentifier,
	@Setting			xml
AS
BEGIN

	INSERT INTO	[UserSettings]([ClientSettingGUID],[UserGUID],[Setting],[DateCreated])
		 VALUES	(@ClientSettingsGUID ,@UserGUID ,@Setting,GETDATE())

	RETURN @@ROWCOUNT

END
GO

-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.09.07
--				This SP is used to delete a user setting
-- =============================================
ALTER PROCEDURE [dbo].[UserSettings_Delete]
	@UserGUID			uniqueidentifier = null,
	@ClientSettingsGUID	uniqueidentifier
AS
BEGIN

	DELETE	UserSettings
	 WHERE	ClientSettingGUID = @ClientSettingsGUID AND
			UserGUID          = @UserGUID

	RETURN @@ROWCOUNT

END
GO

-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.09.07
--				This SP is used to GET a user setting
-- =============================================
ALTER PROCEDURE [dbo].[UserSettings_Get]
	@UserGUID			uniqueidentifier = null,
	@ClientSettingsGUID	uniqueidentifier
AS
BEGIN

	SELECT	*
	  FROM	UserSettings
	 WHERE	ClientSettingGUID = @ClientSettingsGUID AND
			UserGUID          = @UserGUID

END
GO

-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.09.07
--				This SP is used to update a user setting
-- =============================================
ALTER PROCEDURE [dbo].[UserSettings_Update]
	@UserGUID			uniqueidentifier = null,
	@ClientSettingsGUID	uniqueidentifier,
	@NewSetting			xml
AS
BEGIN

	UPDATE	[UserSettings]
	   SET	[Setting] = @NewSetting
	 WHERE	ClientSettingGUID = @ClientSettingsGUID AND
			UserGUID          = @UserGUID

	RETURN @@ROWCOUNT

END
GO