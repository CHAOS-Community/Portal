CREATE PROCEDURE `Session_Get`(
    IN  SessionGUID BINARY(16),
    IN  UserGUID    BINARY(16)
)
BEGIN
    
    DELETE
      FROM	Session
     WHERE	TIMESTAMPDIFF( MINUTE, COALESCE( DateModified, DateCreated ), NOW() ) > 20;
                  
    UPDATE  Session
       SET  DateModified = NOW()
     WHERE	( SessionGUID IS NULL OR Session.GUID     = SessionGUID ) AND
				    ( UserGUID    IS NULL OR Session.UserGUID = UserGUID    );
                  
    SELECT  *
		  FROM	Session
		 WHERE	( SessionGUID IS NULL OR Session.GUID     = SessionGUID ) AND
				    ( UserGUID    IS NULL OR Session.UserGUID = UserGUID    );
    
END