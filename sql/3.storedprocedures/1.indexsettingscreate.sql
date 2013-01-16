CREATE PROCEDURE `IndexSettings_Create`(
    IN  ID          INT UNSIGNED,
    IN  ModuleID    INT UNSIGNED,
    IN  Settings    TEXT
)
BEGIN

    -- TODO: Add user permission check
    INSERT INTO IndexSettings ( ID, ModuleID, Settings, DateCreated )
         VALUES ( ID, ModuleID, Settings, NOW() );

END