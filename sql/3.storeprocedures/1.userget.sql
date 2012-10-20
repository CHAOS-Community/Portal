CREATE PROCEDURE `User_Get`(
    IN  GUID    BINARY(16)
)
BEGIN

    SELECT  *
      FROM  `User`
     WHERE  `User`.GUID = GUID;

END