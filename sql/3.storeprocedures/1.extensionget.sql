CREATE PROCEDURE `Extension_Get`(
    ID  INT,
    Map VARCHAR(255)
)
BEGIN

    SELECT	*
      FROM	Extension AS E
     WHERE	( ID  IS NULL OR E.ID  = ID ) AND
            ( Map IS NULL OR E.Map = Map );

END