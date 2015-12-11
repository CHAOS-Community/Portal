CREATE PROCEDURE ClientSettings_Set
(
    Guid        BINARY(16),
    Name        VARCHAR(255),
    Settings    TEXT
)
BEGIN

    IF EXISTS( SELECT * FROM ClientSettings WHERE ClientSettings.GUID = GUID ) THEN

        UPDATE
            ClientSettings
        SET
            ClientSettings.Name     = COALESCE(Name, ClientSettings.Name),
            ClientSettings.Settings = COALESCE(Settings, ClientSettings.Settings)
        WHERE
            ClientSettings.GUID = Guid;
    ELSE

        INSERT INTO 
            ClientSettings
            ( GUID, Name, Settings, DateCreated )
        VALUES 
            ( Guid, Name, Settings, NOW() );

    END IF;

    SELECT ROW_COUNT();

END