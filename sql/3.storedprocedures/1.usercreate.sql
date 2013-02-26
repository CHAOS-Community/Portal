CREATE PROCEDURE `User_Create`(
    Guid    BINARY(16),
    Email   VARCHAR(1024)
)
BEGIN

    INSERT INTO `User` (GUID, Email, DateCreated)
         VALUES ( Guid, Email, NOW() );

    SELECT ROW_COUNT();

END