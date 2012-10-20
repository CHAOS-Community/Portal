CREATE PROCEDURE `Permission_Create`(
    TableIdentifier VARCHAR(255),
    Permission      INT(10) UNSIGNED,
    Name            VARCHAR(255),
    Description     VARCHAR(1024)
)
BEGIN

    INSERT INTO Permission( TableIdentifier, Permission, Name, Description )
         VALUES ( TableIdentifier, Permission, Name, Description );

END