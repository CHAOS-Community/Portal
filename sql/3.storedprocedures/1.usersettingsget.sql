CREATE PROCEDURE `UserSettings_Get`(
    ClientSettingsGuid  BINARY(16),
    UserGuid            BINARY(16)
)
BEGIN

    SELECT  *
      FROM  UserSettings
     WHERE  UserSettings.ClientSettingsGUID = ClientSettingsGuid AND
            UserSettings.UserGUID           = UserGuid;

END