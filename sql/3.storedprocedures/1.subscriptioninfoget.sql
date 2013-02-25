CREATE PROCEDURE `SubscriptionInfo_Get`(
	Guid               BINARY(16),
	RequestUserGuid    BINARY(16)
)
BEGIN

    SELECT	
		SubscriptionInfo.*
	FROM	
		SubscriptionInfo 
	WHERE	
			( GUid IS NULL OR SubscriptionInfo.GUID = Guid )
        AND DoesUserHavePermissionToSubscription( RequestUserGuid, SubscriptionInfo.GUID, 'Get' ) = 1 ;

END