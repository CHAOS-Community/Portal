CREATE PROCEDURE `User_Set`(
    Guid    BINARY(16),
    Email   VARCHAR(1024)
)
BEGIN

	IF EXISTS (SELECT * FROM `User` WHERE GUID = Guid) THEN
	
		UPDATE
			`User`
		SET
			`User`.Email = Email
		WHERE
			`User`.GUID = Guid;

		SELECT ROW_COUNT();

	ELSE

		INSERT INTO `User` (GUID, Email, DateCreated)
			 VALUES ( Guid, Email, NOW() );

		SELECT ROW_COUNT();
		
	END IF;
END