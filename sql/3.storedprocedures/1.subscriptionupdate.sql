CREATE PROCEDURE `Subscription_Update`(
    IN  GUID            BINARY(16),
    IN  NewName         VARCHAR(255),
    IN  RequestUserGUID BINARY(16),
    OUT ErrorCode       INTEGER
)
BEGIN

    IF DoesUserHavePermissionToSubscription( RequestUserGUID, GUID, 'UPDATE' ) = 0 THEN
        SET ErrorCode = -100;
    ELSE
    
        UPDATE	Subscription
           SET	Subscription.Name = NewName
         WHERE	Subscription.GUID = GUID;
         
        SET ErrorCode = 1;
    END IF;

END