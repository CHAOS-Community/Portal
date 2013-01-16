CREATE PROCEDURE `Group_Create`(
    IN  GUID                BINARY(16),
    IN  Name	              VARCHAR(255),
    IN  RequestUserGUID     BINARY(16),
    IN  SystemPermission    INT UNSIGNED,
    OUT ErrorCode           INT
)
BEGIN
    DECLARE EXIT HANDLER
    FOR SQLEXCEPTION, SQLWARNING, NOT FOUND
        ROLLBACK;
        SET ErrorCode = -200;

    IF RequestUserGUID IS NOT NULL AND DoesUserHavePermissionToSystem( RequestUserGUID, 'CREATE_GROUP' ) = 0  OR
       RequestUserGUID IS NOT NULL AND GetUsersHighestSystemPermission( RequestUserGUID ) & SystemPermission <> SystemPermission THEN
        SET ErrorCode = -100;
    ELSE
    
        START TRANSACTION;
        
        INSERT INTO `Group`( GUID, SystemPermission, Name, DateCreated )
             VALUES ( GUID, SystemPermission, Name, NOW() );
                            
        IF RequestUserGUID IS NOT NULL THEN
            INSERT INTO Group_User_Join ( GroupGUID, UserGUID, Permission, DateCreated )
                 VALUES ( GUID, RequestUserGUID, 4294967295, NOW() );
        END IF;

        SET ErrorCode = 1;

        COMMIT;
    
    END IF;
    

END