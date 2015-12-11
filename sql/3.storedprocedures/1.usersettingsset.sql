CREATE PROCEDURE UserSettings_Set(
    ClientSettingsGuid  BINARY(16),
    UserGuid            BINARY(16),
    Settings            TEXT
)
BEGIN

    IF NOT EXISTS ( SELECT  * 
                      FROM  UserSettings 
                     WHERE  UserSettings.ClientSettingsGUID = ClientSettingsGuid AND 
                            UserSettings.UserGUID           = UserGuid) THEN
        INSERT INTO UserSettings (ClientSettingsGUID, UserGUID, Settings, DateCreated)
             VALUES (ClientSettingsGuid, UserGuid, Settings, NOW());
    ELSE
    
        UPDATE  UserSettings
           SET  UserSettings.Settings = Settings
         WHERE  UserSettings.ClientSettingsGUID = ClientSettingsGuid AND
                UserSettings.UserGUID           = UserGuid;
    
    END IF;

	SELECT ROW_COUNT();

END
