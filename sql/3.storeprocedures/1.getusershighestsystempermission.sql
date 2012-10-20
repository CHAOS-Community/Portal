CREATE FUNCTION `GetUsersHighestSystemPermission`(
    UserGUID        BINARY(16)
) RETURNS int(10) unsigned
BEGIN

    DECLARE Permission              INT UNSIGNED;
    DECLARE AccumulatedPermission   INT UNSIGNED;
    DECLARE NO_MORE_ROWS            BOOLEAN;
    DECLARE SystemPermission_CURSOR CURSOR FOR
        SELECT  G.SystemPermission
          FROM  `Group` AS G
                INNER JOIN Group_User_Join AS GUJ ON GUJ.GroupGUID = G.GUID
         WHERE  GUJ.UserGUID = UserGUID;
    
    DECLARE CONTINUE HANDLER FOR NOT FOUND
        SET NO_MORE_ROWS = TRUE;

    SET AccumulatedPermission = 0;
    
    OPEN SystemPermission_CURSOR;
    
    the_loop: LOOP
    
    FETCH  SystemPermission_CURSOR
     INTO  Permission;
    
    IF NO_MORE_ROWS THEN
        CLOSE SystemPermission_CURSOR;
        LEAVE the_loop;
    END IF;
    
    
    
    SET AccumulatedPermission = AccumulatedPermission | Permission;
    
    END LOOP the_loop;
    

        RETURN AccumulatedPermission;


END