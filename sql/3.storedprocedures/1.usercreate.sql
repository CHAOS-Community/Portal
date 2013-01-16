CREATE PROCEDURE `User_Create`(
    GUID    BINARY(16),
    Email   VARCHAR(1024)
)
BEGIN

    INSERT INTO `User` (GUID, Email, DateCreated)
         VALUES ( GUID, Email, NOW() );



    SELECT ROW_COUNT();

END