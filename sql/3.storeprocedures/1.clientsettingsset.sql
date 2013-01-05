CREATE PROCEDURE `ClientSettings_Set`(
    GUID        BINARY(16),
    Name        VARCHAR(255),
    Settings    TEXT
)
BEGIN

    IF EXISTS( SELECT * FROM ClientSettings WHERE ClientSettings.GUID = GUID ) THEN

        UPDATE
            ClientSettings
        SET
            ClientSettings.Name     = IFNULL(Name, ClientSettings.Name),
            ClientSettings.Settings = IFNULL(Settings, ClientSettings.Settings)
        WHERE
            ClientSettings.GUID = GUID;
    ELSE

        INSERT INTO 
            ClientSettings
            ( GUID, Name, Settings, DateCreated )
        VALUES 
            ( GUID, Name, Settings, NOW() );

    END IF;

    SELECT ROW_COUNT();

END