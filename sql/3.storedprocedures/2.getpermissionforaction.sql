CREATE FUNCTION `GetPermissionForAction`(
    TableIdentifier VARCHAR(16),
    Name	          VARCHAR(255)
) RETURNS int(10) unsigned
BEGIN

    DECLARE Permission INT UNSIGNED;

    SELECT	P.Permission INTO Permission
      FROM	Permission AS P
     WHERE	P.TableIdentifier = TableIdentifier AND
            P.Name            = Name;

    RETURN Permission;
END