CREATE PROCEDURE `Group_Delete`(
    IN  GUID		  BINARY(16),
    IN  UserGUID	BINARY(16),
    OUT ErrorCode INT
)
BEGIN
    DECLARE EXIT HANDLER
    FOR SQLEXCEPTION, SQLWARNING, NOT FOUND
        ROLLBACK;
        SET ErrorCode = -200;

    IF DoesUserHavePermissionToGroup( UserGUID, GUID, 'DELETE' ) = 0 THEN
        SET ErrorCode = -100;
    ELSE
        
        START TRANSACTION;
    
        DELETE
          FROM	Group_User_Join
         WHERE	GroupGUID = GUID;

         DELETE
           FROM	`Group`
          WHERE	`Group`.GUID = GUID;
     
        SET ErrorCode = 1;
        
        COMMIT;
     
     END IF;

END