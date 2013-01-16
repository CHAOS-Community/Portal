CREATE PROCEDURE `Module_Create`
(
    IN  ID 		INT UNSIGNED,
    IN  Name		VARCHAR(255),
    IN  Configuration	TEXT
)
BEGIN

    INSERT INTO Module( ID, Name, Configuration, DateCreated )
                VALUES( ID, Name, Configuration, NOW() );

    SELECT ID;

END