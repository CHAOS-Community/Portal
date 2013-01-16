CREATE PROCEDURE `UserSettings_Delete`(
    ClientSettingsGUID  BINARY(16),
    UserGUID            BINARY(16)
)
BEGIN

    DELETE  
      FROM  UserSettings
     WHERE  UserSettings.ClientSettingsGUID = ClientSettingsGUID AND
            UserSettings.UserGUID           = UserGUID;
    
    SELECT 1;

END