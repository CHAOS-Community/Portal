CREATE PROCEDURE `Group_Update`(
	NewName             VARCHAR(255),
	NewSystemPermission INT UNSIGNED,
	WhereGroupGuid      BINARY(16),
	RequestUserGuid     BINARY(16)
)
BEGIN

    IF DoesUserHavePermissionToGroup( RequestUserGuid,WhereGroupGuid, 'UPDATE' ) = 0 THEN
        SELECT -100;
    ELSE
    
        UPDATE	`Group`
           SET	Name             = COALESCE( NewName, Name ),
                SystemPermission = COALESCE( NewSystemPermission, SystemPermission )
         WHERE	GUID = WhereGroupGuid;
         
         SELECT ROW_COUNT();
    
    END IF;

END