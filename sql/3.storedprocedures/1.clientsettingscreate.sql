CREATE PROCEDURE ClientSetting_Create(
    GUID        BINARY(16),
    Name        VARCHAR(255),
    Settings    TEXT
)
BEGIN

    INSERT INTO ClientSettings
    	( GUID, Name, Settings, DateCreated )
	VALUES 
		( GUID, Name, Settings, NOW() );

END