CREATE PROCEDURE `Subscription_Delete`(
    Guid			    BINARY(16),
    RequestingUserGuid     BINARY(16)
)
BEGIN
    DECLARE EXIT HANDLER
    FOR SQLEXCEPTION, SQLWARNING, NOT FOUND
	BEGIN
        ROLLBACK;
		SELECT -200;
	END;
		
    IF DoesUserHavePermissionToSubscription( RequestingUserGuid, Guid, 'DELETE' ) = 0 THEN
        SELECT -100;
    ELSE
    
    START TRANSACTION;
    
		DELETE
		FROM	
			Subscription_User_Join
		WHERE	
			SubscriptionGUID = Guid;
	
		DELETE
		FROM	
			Subscription
		WHERE	
			GUID = Guid;
    
		SELECT ROW_COUNT();
    
    COMMIT;
    
    END IF;

END