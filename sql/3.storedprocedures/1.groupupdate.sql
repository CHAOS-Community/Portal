CREATE PROCEDURE `Group_Update`(
    IN  NewName             VARCHAR(255),
    IN  NewSystemPermission INT UNSIGNED,
    IN  WhereGroupGUID      BINARY(16),
    IN  RequestUserGUID     BINARY(16)
)
BEGIN

    IF DoesUserHavePermissionToGroup( RequestUserGUID,WhereGroupGUID, 'UPDATE' ) = 0 THEN
        SELECT -100;
    ELSE
    
        UPDATE	`Group`
           SET	Name             = COALESCE( NewName, Name ),
                SystemPermission = COALESCE( NewSystemPermission, SystemPermission )
         WHERE	GUID = WhereGroupGUID;
         
         SELECT ROW_COUNT();
    
    END IF;

END