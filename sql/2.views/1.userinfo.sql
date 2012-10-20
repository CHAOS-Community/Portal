CREATE VIEW `UserInfo` AS 
select 
    `User`.`GUID` AS `GUID`,
    `Session`.`GUID` AS `SessionGUID`,
    `GetUsersHighestSystemPermission`(`User`.`GUID`) AS `SystemPermission`,
    `User`.`Email` AS `Email`,
    `Session`.`DateModified` AS `DateModified`,
    `Session`.`DateCreated` AS `SessionDateCreated` 
from 
    `User` 
    left join `Session` on `User`.`GUID` = `Session`.`UserGUID`;