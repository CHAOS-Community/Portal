CREATE PROCEDURE `Module_Get`(
    IN  ID      INTEGER,
    IN  Name    VARCHAR(255)
)
BEGIN

    SELECT	*
	    FROM	Module AS M
	   WHERE	( ID   IS NULL OR M.ID   = ID ) AND
			      ( Name IS NULL AND M.ID IN (1,2,3,4) OR M.Name = Name );

END