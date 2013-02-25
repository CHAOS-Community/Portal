CREATE PROCEDURE `Session_Delete`(
	Guid		BINARY(16),
	UserGuid	BINARY(16)
)
BEGIN

    DELETE
	FROM  
		Session
	WHERE  
			Session.GUID     = Guid
        AND Session.UserGUID = UserGuid;
            
    SELECT ROW_COUNT();
            
END