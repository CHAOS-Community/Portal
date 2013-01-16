CREATE PROCEDURE `Log_Create`(
    Name            VARCHAR(255),
    Level           VARCHAR(50), 
    SessionGUID     BINARY(16),
    Duration        INT UNSIGNED,
    Message         TEXT
)
BEGIN

    INSERT INTO Log( Name, Level, SessionGUID, Duration, Message, DateCreated ) 
             VALUES( Name, Level, SessionGUID, Duration, Message, NOW()       );
             
    SELECT ROW_COUNT();

END