CREATE PROCEDURE `Session_Delete`(
    IN  SessionGUID  BINARY(16),
    IN  UserGUID     BINARY(16)
)
BEGIN

    DELETE
      FROM  Session
     WHERE  Session.GUID     = SessionGUID AND
            Session.UserGUID = UserGUID;
            
    SELECT 1;
            
END