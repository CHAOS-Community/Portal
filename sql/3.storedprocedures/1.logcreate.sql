CREATE PROCEDURE `Log_Create`(
    Name            VARCHAR(255),
    Level           VARCHAR(50), 
    SessionGuid     BINARY(16),
    Duration        INT UNSIGNED,
    Message         TEXT
)
BEGIN

	DELETE FROM
	    Log
	WHERE
	    TIMESTAMPDIFF( DAY, DateCreated, NOW() ) > 7;

    INSERT INTO Log( Name, Level, SessionGUID, Duration, Message, DateCreated ) 
             VALUES( Name, Level, SessionGuid, Duration, Message, NOW()       );
             
    SELECT ROW_COUNT();

END