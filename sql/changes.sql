DROP PROCEDURE [dbo].[UserSettings_Create]
GO
DROP PROCEDURE [dbo].[UserSettings_Update]
GO

-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.11.29
--				This SP is will create an UserSetting if it doesn't exist otherwise it will update it.
-- =============================================
CREATE PROCEDURE UserSettings_Set
	@UserGUID			uniqueidentifier = null,
	@ClientSettingsGUID	uniqueidentifier,
	@Setting			xml
AS
BEGIN
	
	IF NOT EXISTS( SELECT [ClientSettingGUID] FROM [UserSettings] WHERE	ClientSettingGUID = @ClientSettingsGUID AND UserGUID = @UserGUID )
		INSERT INTO	[UserSettings]([ClientSettingGUID],[UserGUID],[Setting],[DateCreated])
			 VALUES	(@ClientSettingsGUID ,@UserGUID ,@Setting,GETDATE())
	ELSE
		UPDATE	[UserSettings]
		   SET	[Setting] = @Setting
		 WHERE	ClientSettingGUID = @ClientSettingsGUID AND
				UserGUID          = @UserGUID

	RETURN @@ROWCOUNT

END
GO
