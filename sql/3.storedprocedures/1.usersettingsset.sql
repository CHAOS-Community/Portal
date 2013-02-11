CREATE PROCEDURE UserSettings_Set(
    ClientSettingsGUID  BINARY(16),
    UserGUID            BINARY(16),
    Settings            TEXT
)
BEGIN

    IF NOT EXISTS ( SELECT  ClientSettingsGUID 
                      FROM  UserSettings 
                     WHERE  UserSettings.ClientSettingsGUID = ClientSettingsGUID AND 
                            UserSettings.UserGUID           = UserGUID) THEN
        INSERT INTO UserSettings (ClientSettingsGUID, UserGUID, Settings, DateCreated)
             VALUES (ClientSettingsGUID, UserGUID, Settings, NOW());
    ELSE
    
        UPDATE  UserSettings
           SET  UserSettings.Settings = Settings
         WHERE  UserSettings.ClientSettingsGUID = ClientSettingsGUID AND
                UserSettings.UserGUID           = UserGUID;
    
    END IF;

END
