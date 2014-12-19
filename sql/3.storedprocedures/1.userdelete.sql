CREATE PROCEDURE `User_Delete`(
    Guid    BINARY(16)
)
BEGIN

	DELETE 
    FROM 
		`User`
	WHERE 
		`User`.GUID = Guid;

    SELECT ROW_COUNT();

END