CREATE PROCEDURE `Group_UpdateUserPermissions`(
    IN  GroupGUID       BINARY(16),
    IN  UserGUID        BINARY(16),
    IN  Permissions      INT(10) UNSIGNED,
    IN  RequestingUserGUID BINARY(16),
    OUT ErrorCode       INTEGER
)
BEGIN
    IF RequestingUserGUID IS NULL OR DoesUserHavePermissionToGroup( RequestingUserGUID, GroupGUID, 'UPDATE_USER' ) = 1 THEN
        UPDATE Group_User_Join as guj SET Permission=Permissions
			WHERE guj.GroupGUID = GroupGUID AND guj.UserGUID = UserGUID;
             
        SET ErrorCode = 1;
    ELSE
        SET ErrorCode = -100;
    END IF;
END