CREATE VIEW  SubscriptionInfo  
AS 
	SELECT 
		Subscription.GUID  AS  GUID ,
		Subscription.Name  AS  Name ,
		Subscription_User_Join.UserGUID  AS  UserGUID ,
		Subscription_User_Join.Permission  AS  Permission ,
		Subscription.DateCreated  AS  DateCreated  
	FROM 
		Subscription  
		LEFT JOIN  Subscription_User_Join  ON  Subscription . GUID  =  Subscription_User_Join . SubscriptionGUID ;

