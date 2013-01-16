CREATE PROCEDURE `UserInfo_Get`(
    GUID        BINARY(16),
    SessionGUID BINARY(16),
    Email       VARCHAR(1024)
)
BEGIN

    SELECT  *
      FROM  UserInfo AS UI
     WHERE  ( GUID        IS NULL OR UI.GUID        = GUID ) AND 
            ( SessionGUID IS NULL OR UI.SessionGUID = SessionGUID ) AND
            ( Email       IS NULL OR UI.Email       = Email );

END