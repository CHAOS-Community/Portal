CREATE PROCEDURE `Group_Create`(
	Guid                BINARY(16),
	Name	            VARCHAR(255),
	RequestUserGuid     BINARY(16),
	SystemPermission    INT UNSIGNED
)
BEGIN
    DECLARE EXIT HANDLER
    FOR SQLEXCEPTION, SQLWARNING, NOT FOUND
	BEGIN
        ROLLBACK;
        SELECT -200;
	END;

    IF RequestUserGuid IS NOT NULL AND DoesUserHavePermissionToSystem( RequestUserGuid, 'CREATE_GROUP' ) = 0  OR
       RequestUserGuid IS NOT NULL AND GetUsersHighestSystemPermission( RequestUserGuid ) & SystemPermission <> SystemPermission THEN
        SELECT -100;
    ELSE
    
        START TRANSACTION;
        
        INSERT INTO `Group`( GUID, SystemPermission, Name, DateCreated )
             VALUES ( Guid, SystemPermission, Name, NOW() );
                            
        IF RequestUserGUID IS NOT NULL THEN
            INSERT INTO Group_User_Join ( GroupGUID, UserGUID, Permission, DateCreated )
                 VALUES ( Guid, RequestUserGuid, 4294967295, NOW() );
        END IF;

        SELECT 1;

        COMMIT;
    
    END IF;
    
END