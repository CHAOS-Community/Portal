CREATE PROCEDURE Session_Create(
	Guid BINARY(16),
	UserGuid    BINARY(16)
)
BEGIN

    INSERT INTO Session( GUID, UserGUID, DateCreated, DateModified )
         VALUES ( Guid, UserGuid, NOW(), NULL );

    SELECT ROW_COUNT();

END