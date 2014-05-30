CREATE PROCEDURE `Module_Set`
(
    IN  Id 		INT UNSIGNED,
    IN  Name		VARCHAR(255),
    IN  Configuration	TEXT
)
BEGIN

	IF Id IS NULL OR Id = 0 THEN

		INSERT INTO Module( Name, Configuration, DateCreated )
					VALUES( Name, Configuration, UTC_TIMESTAMP() );

		SELECT last_insert_id();

	ELSE

		SELECT -666;

	END IF;

END