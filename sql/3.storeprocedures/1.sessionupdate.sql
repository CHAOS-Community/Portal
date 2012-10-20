CREATE PROCEDURE `Session_Update`(
    IN  UserGUID            BINARY(16),
    IN  WhereSessionGUID    BINARY(16),
    IN  WhereUserGUID       BINARY(16)
)
BEGIN
    
    UPDATE  Session
       SET  Session.UserGUID     = COALESCE( UserGUID, Session.UserGUID ),
            Session.DateModified = NOW()
     WHERE  Session.GUID     = WhereSessionGUID AND
            ( WhereUserGUID IS NULL OR Session.UserGUID = WhereUserGUID );
            
    SELECT ROW_COUNT();
    
END