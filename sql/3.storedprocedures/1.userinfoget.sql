CREATE PROCEDURE `UserInfo_Get`(
    Guid        BINARY(16),
    SessionGuid BINARY(16),
    Email       VARCHAR(1024),
	GroupGuid	BINARY(16)
)
BEGIN

    SELECT  *
      FROM  UserInfo AS UI
     WHERE  ( Guid        IS NULL OR UI.GUID        = Guid ) AND 
            ( SessionGuid IS NULL OR UI.SessionGUID = SessionGuid ) AND
            ( Email       IS NULL OR UI.Email       = Email ) AND
			( GroupGuid	  IS NULL OR EXISTS 
				( SELECT * FROM Group_User_Join AS guj WHERE guj.UserGUID = UI.GUID AND guj.GroupGUID = GroupGuid)
			);

END