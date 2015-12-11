CREATE VIEW UserInfo 
AS
SELECT 
    User.GUID AS GUID,
    Session.GUID AS SessionGUID,
    GetUsersHighestSystemPermission(User.GUID) AS SystemPermission,
    User.Email AS Email,
    User.DateCreated AS DateCreated,
    Session.DateModified AS DateModified,
    Session.DateCreated AS SessionDateCreated 
FROM 
    User 
    LEFT JOIN Session ON User.GUID = Session.UserGUID;