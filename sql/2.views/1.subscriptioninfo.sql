CREATE VIEW `SubscriptionInfo` AS 

select 
    `Subscription`.`GUID` AS `GUID`,
    `Subscription`.`Name` AS `Name`,
    `Subscription_User_Join`.`UserGUID` AS `UserGUID`,
    `Subscription_User_Join`.`Permission` AS `Permission`,
    `Subscription`.`DateCreated` AS `DateCreated` 
from 
    `Subscription` 
    left join `Subscription_User_Join` on `Subscription`.`GUID` = `Subscription_User_Join`.`SubscriptionGUID`;

