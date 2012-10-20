CREATE PROCEDURE `SubscriptionInfo_Get`(
    IN  GUID               BINARY(16),
    IN  RequestUserGUID    BINARY(16)
)
BEGIN

    SELECT	SubscriptionInfo.*
	    FROM	SubscriptionInfo 
	   WHERE	( GUID IS NULL OR SubscriptionInfo.GUID = GUID ) AND
            DoesUserHavePermissionToSubscription( RequestUserGUID, SubscriptionInfo.GUID, 'Get' ) = 1 ;

END