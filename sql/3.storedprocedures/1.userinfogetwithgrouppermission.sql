CREATE PROCEDURE UserInfo_GetWithGroupPermission(
    Guid        BINARY(16)
)
BEGIN

    SELECT  UI.*
      FROM  Group_User_Join AS myGroups
			INNER JOIN Group_User_Join AS guj ON myGroups.GroupGUID = guj.GroupGUID
			INNER JOIN UserInfo AS UI ON guj.UserGUID = UI.GUID
     WHERE  
			myGroups.UserGUID = Guid
		AND myGroups.Permission & 8 = 8;

END