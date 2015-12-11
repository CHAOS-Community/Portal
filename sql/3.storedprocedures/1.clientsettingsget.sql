CREATE PROCEDURE ClientSettings_Get
(
    Guid	BINARY(16)
)
BEGIN

    SELECT  
    	*
	FROM  
		ClientSettings
	WHERE  
		ClientSettings.GUID = Guid;
    
END