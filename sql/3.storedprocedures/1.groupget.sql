CREATE PROCEDURE `Group_Get`(
    IN  GUID		        BINARY(16),
    IN  Name	          VARCHAR(255),
    IN  RequestUserGUID BINARY(16)
)
BEGIN
    SELECT	*
      FROM	`Group` AS G
     WHERE	( GUID IS NULL OR GUID = G.GUID ) AND
            ( Name IS NULL OR Name = G.Name ) AND
            ( RequestUserGUID IS NULL OR DoesUserHavePermissionToGroup( RequestUserGUID, G.GUID, 'GET' ) = 1 );
END