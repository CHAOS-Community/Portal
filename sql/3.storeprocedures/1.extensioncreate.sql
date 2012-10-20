CREATE PROCEDURE `Extension_Create`(
    ID          INT,
    Map			    VARCHAR(255),
    Fullname    VARCHAR(255),
    Path        VARCHAR(1024)
)
BEGIN

    INSERT INTO Extension( ID, Map, Fullname, Path, DateCreated )
                   VALUES( ID, Map, Fullname, Path, NOW() );

END