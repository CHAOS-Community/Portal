CREATE FUNCTION `DoesUserHavePermissionToSystem`(
    UserGUID        BINARY(16),
    PermissionName  VARCHAR(255)
) RETURNS tinyint(1)
BEGIN

    DECLARE RequiredPermission	INT UNSIGNED;
    SET RequiredPermission = GetPermissionForAction( 'System', PermissionName );
	
    RETURN RequiredPermission & GetUsersHighestSystemPermission( UserGUID ) = RequiredPermission;

END