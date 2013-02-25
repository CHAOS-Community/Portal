CREATE PROCEDURE `Session_Update`(
	UserGuid            BINARY(16),
	WhereSessionGuid    BINARY(16),
	WhereUserGuid       BINARY(16)
)
BEGIN
    
    UPDATE
		Session
    SET
		Session.UserGUID     = COALESCE( UserGuid, Session.UserGUID ),
        Session.DateModified = NOW()
    WHERE
			Session.GUID = WhereSessionGuid
        AND ( WhereUserGuid IS NULL OR Session.UserGUID = WhereUserGuid );
            
    SELECT ROW_COUNT();
    
END