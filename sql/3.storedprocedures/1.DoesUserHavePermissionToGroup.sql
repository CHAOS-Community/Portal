CREATE FUNCTION DoesUserHavePermissionToGroup(
    UserGUID        BINARY(16),
    GroupGUID	      BINARY(16),
    PermissionName	VARCHAR(255)
) RETURNS tinyint(1)
BEGIN

    DECLARE	Result             INT;
    DECLARE RequiredPermission INT UNSIGNED;
    
    SET RequiredPermission = GetPermissionForAction( 'Group', PermissionName );

    SELECT	COUNT(*) INTO Result
      FROM	Group_User_Join AS GUJ
     WHERE	GUJ.UserGUID  = UserGUID AND
            GUJ.GroupGUID = GroupGUID AND
            GUJ.Permission & RequiredPermission = RequiredPermission;

    RETURN (Result > 0);
END