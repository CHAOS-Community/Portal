CREATE PROCEDURE `Subscription_Create`(
	Guid			BINARY(16),
	Name			VARCHAR(255),
	RequestUserGuid	BINARY(16)
)
BEGIN
    DECLARE EXIT HANDLER
    FOR SQLEXCEPTION, SQLWARNING, NOT FOUND
	BEGIN
        ROLLBACK;
		SELECT -200;
	END;

    IF DoesUserHavePermissionToSystem( RequestUserGuid, 'CREATE_SUBSCRIPTION' ) = 0 THEN
        SELECT -100;
    ELSE
        
        START TRANSACTION;

        INSERT INTO Subscription ( GUID, Name, DateCreated )
		         VALUES ( Guid, Name, NOW() );

        INSERT INTO Subscription_User_Join ( SubscriptionGUID, UserGUID, Permission, DateCreated ) 
		         VALUES ( Guid, RequestUserGuid, 4294967295, NOW() );

		SELECT ROW_COUNT();

        COMMIT;

    END IF;

END