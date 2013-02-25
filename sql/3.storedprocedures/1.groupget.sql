CREATE PROCEDURE `Group_Get`(
	Guid			BINARY(16),
	Name			VARCHAR(255),
	RequestUserGuid BINARY(16)
)
BEGIN
    SELECT	*
      FROM	`Group` AS G
     WHERE	( Guid IS NULL OR Guid = G.GUID ) AND
            ( Name IS NULL OR Name = G.Name ) AND
            ( RequestUserGuid IS NULL OR DoesUserHavePermissionToGroup( RequestUserGuid, G.GUID, 'GET' ) = 1 );
END