CREATE PROCEDURE `Group_Get`(
	Guid				BINARY(16),
	Name				VARCHAR(255),
	RequestingUserGuid	BINARY(16),
	UserGuid			BINARY(16)
)
BEGIN
    SELECT	*
      FROM	`Group` AS G
     WHERE	( Guid IS NULL OR Guid = G.GUID ) AND
            ( Name IS NULL OR Name = G.Name ) AND
            ( RequestingUserGuid IS NULL OR DoesUserHavePermissionToGroup( RequestingUserGuid, G.GUID, 'GET' ) = 1 ) AND
			( UserGuid IS NULL OR EXISTS 
				( SELECT * FROM Group_User_Join AS guj WHERE guj.UserGUID = UserGuid AND guj.GroupGUID = G.GUID)
			);
END