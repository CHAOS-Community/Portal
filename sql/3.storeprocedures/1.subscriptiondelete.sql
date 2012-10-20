CREATE PROCEDURE `Subscription_Delete`(
    IN  SubscriptionGUID    BINARY(16),
    IN  RequestUserGUID     BINARY(16),
    OUT ErrorCode           INTEGER
)
BEGIN
    DECLARE EXIT HANDLER
    FOR SQLEXCEPTION, SQLWARNING, NOT FOUND
        ROLLBACK;

    IF DoesUserHavePermissionToSubscription( RequestUserGUID, SubscriptionGUID, 'DELETE' ) = 0 THEN
        SET ErrorCode = -100;
    ELSE
    
    START TRANSACTION;
    
    DELETE
	    FROM	Subscription_User_Join
	   WHERE	SubscriptionGUID = SubscriptionGUID;
	
    DELETE
      FROM	Subscription
     WHERE	GUID = SubscriptionGUID;
    
    SET ErrorCode = 1;
    
    COMMIT;
    
    END IF;

END