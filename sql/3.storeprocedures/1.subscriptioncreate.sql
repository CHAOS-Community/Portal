CREATE PROCEDURE `Subscription_Create`(
    IN  GUID			        BINARY(16),
    IN  Name			        VARCHAR(255),
    IN  RequestUserGUID   BINARY(16),
    OUT ErrorCode         INTEGER
)
BEGIN
    DECLARE EXIT HANDLER
    FOR SQLEXCEPTION, SQLWARNING, NOT FOUND
        ROLLBACK;

    IF DoesUserHavePermissionToSystem( RequestUserGUID, 'CREATE_SUBSCRIPTION' ) = 0 THEN
        SET ErrorCode = -100;
    ELSE
        
        START TRANSACTION;

        INSERT INTO Subscription ( GUID, Name, DateCreated )
		         VALUES ( GUID, Name, NOW() );

        INSERT INTO Subscription_User_Join ( SubscriptionGUID, UserGUID, Permission, DateCreated ) 
		         VALUES ( GUID, RequestUserGUID, 4294967295, NOW() );

        SET ErrorCode = 1;

        COMMIT;

    END IF;

END