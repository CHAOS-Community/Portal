CREATE PROCEDURE `UserSettings_Get`(
    ClientSettingsGUID  BINARY(16),
    UserGUID            BINARY(16)
)
BEGIN

    SELECT  *
      FROM  UserSettings
     WHERE  UserSettings.ClientSettingsGUID = ClientSettingsGUID AND
            UserSettings.UserGUID           = UserGUID;

END