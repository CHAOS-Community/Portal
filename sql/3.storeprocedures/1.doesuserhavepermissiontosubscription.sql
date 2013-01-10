CREATE FUNCTION `DoesUserHavePermissionToSubscription`(
    UserGUID            BINARY(16),
    SubscriptionGUID    BINARY(16),
    PermissionName	    VARCHAR(255)
) RETURNS tinyint(1)
BEGIN

    DECLARE	Result	INT;

    DECLARE RequiredPermission INT;
    SET RequiredPermission = GetPermissionForAction( 'Subscription', PermissionName );

    SELECT	
        COUNT(*) INTO Result
    FROM	
        Subscription_User_Join AS SUJ
    WHERE	
            SUJ.UserGUID         = UserGUID 
        AND SUJ.SubscriptionGUID = SubscriptionGUID 
        AND SUJ.Permission & RequiredPermission = RequiredPermission;

    -- Return the result of the function
    RETURN (Result);

END