CREATE PROCEDURE `UserSettings_Delete`(
    ClientSettingsGuid  BINARY(16),
    UserGuid            BINARY(16)
)
BEGIN

    DELETE  
      FROM  UserSettings
     WHERE  UserSettings.ClientSettingsGUID = ClientSettingsGuid AND
            UserSettings.UserGUID           = UserGuid;
    
    SELECT 1;

END