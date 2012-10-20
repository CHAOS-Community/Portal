CREATE PROCEDURE `IndexSettings_Get`(
    IN ModuleID INTEGER
)
BEGIN

    SELECT	*
      FROM	IndexSettings AS InS
     WHERE	InS.ModuleID = ModuleID;

END