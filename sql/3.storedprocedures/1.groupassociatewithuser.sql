CREATE PROCEDURE `Group_AssociateWithUser`(
    IN  GroupGUID       BINARY(16),
    IN  UserGUID        BINARY(16),
    IN  Permission      INT(10) UNSIGNED,
    IN  RequestUserGUID BINARY(16)
)
BEGIN

    IF RequestUserGUID IS NULL OR DoesUserHavePermissionToGroup( RequestUserGUID, GroupGUID, 'ADD_USER' ) = 1 THEN
        INSERT INTO Group_User_Join ( GroupGUID, UserGUID, Permission, DateCreated) 
             VALUES ( GroupGUID, UserGUID, Permission, NOW() );
             
        SELECT 1;
    ELSE
        SELECT -100;
    END IF;
    

END