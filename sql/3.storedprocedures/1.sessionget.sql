CREATE PROCEDURE `Session_Get`(
	Guid BINARY(16),
	UserGuid    BINARY(16)
)
BEGIN
    
    DELETE
	FROM	
		Session
	WHERE	
		TIMESTAMPDIFF( MINUTE, COALESCE( DateModified, DateCreated ), NOW() ) > 20;
                  
    UPDATE
		Session
	SET
		DateModified = NOW()
    WHERE
			( Guid     IS NULL OR Session.GUID     = Guid )
		AND ( UserGuid IS NULL OR Session.UserGUID = UserGuid );
                  
    SELECT  
		*
	FROM	
		Session
	WHERE	
			( Guid     IS NULL OR Session.GUID     = Guid )
		AND ( UserGuid IS NULL OR Session.UserGUID = UserGuid );
    
END