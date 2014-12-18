CREATE PROCEDURE `User_Set`(
    Guid    BINARY(16),
    Email   VARCHAR(1024)
)
BEGIN

	IF EXISTS (SELECT * FROM `User` WHERE GUID = Guid) THEN
	
		INSERT INTO `User` (GUID, Email, DateCreated)
			 VALUES ( Guid, Email, NOW() );

		SELECT ROW_COUNT();

	ELSE

		UPDATE
			`User`
		SET
			Email = Email
		WHERE
			GUID = Guid;

		SELECT ROW_COUNT();

	END IF;
END