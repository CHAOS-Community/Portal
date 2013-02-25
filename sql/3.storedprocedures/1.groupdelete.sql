CREATE PROCEDURE `Group_Delete`(
	Guid		  BINARY(16),
	UserGuid	BINARY(16)
)
BEGIN
    DECLARE EXIT HANDLER
    FOR SQLEXCEPTION, SQLWARNING, NOT FOUND
    BEGIN
		ROLLBACK;
        SELECT -200;
	END;

    IF DoesUserHavePermissionToGroup( UserGuid, Guid, 'DELETE' ) = 0 THEN
        SELECT -100;
    ELSE
        
        START TRANSACTION;
    
        DELETE
          FROM	Group_User_Join
         WHERE	GroupGUID = Guid;

         DELETE
           FROM	`Group`
          WHERE	`Group`.GUID = Guid;
     
        SELECT ROW_COUNT();
        
        COMMIT;
     
     END IF;

END