CREATE PROCEDURE Session_Create(
    IN  SessionGUID BINARY(16),
    IN  UserGUID    BINARY(16)
)
BEGIN

    INSERT INTO Session( GUID, UserGUID, DateCreated, DateModified )
         VALUES ( SessionGUID, UserGUID, NOW(), NULL );

    SELECT 1;

END