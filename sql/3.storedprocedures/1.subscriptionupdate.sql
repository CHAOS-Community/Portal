CREATE PROCEDURE `Subscription_Update`(
	Guid            BINARY(16),
	NewName         VARCHAR(255),
	RequestingUserGuid BINARY(16)
)
BEGIN

    IF DoesUserHavePermissionToSubscription( RequestingUserGuid, Guid, 'UPDATE' ) = 0 THEN
        SELECT -100;
    ELSE
    
        UPDATE	
			Subscription
        SET	
			Subscription.Name = NewName
        WHERE
			Subscription.GUID = Guid;
         
        SELECT ROW_COUNT();

    END IF;

END