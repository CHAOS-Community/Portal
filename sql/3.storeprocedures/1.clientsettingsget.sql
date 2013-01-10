CREATE PROCEDURE ClientSettings_Get
(
    GUID    BINARY(16)
)
BEGIN

    SELECT  
    	*
	FROM  
		ClientSettings AS CS
	WHERE  
		CS.GUID = GUID;
    
END