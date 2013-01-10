CREATE VIEW  SessionInfo  
AS 
	SELECT 
		Session.GUID  AS  SessionGUID ,
		Session.UserGUID  AS  UserGUID ,
		Session.DateCreated  AS  DateCreated ,
		Session.DateModified  AS  DateModified ,
		timestampdiff(MINUTE, Session . DateModified ,now()) AS  MinutesSinceRenewal  
	FROM 
		Session;