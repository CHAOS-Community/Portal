CREATE TABLE `Subscription_User_Join` (
  `SubscriptionGUID` binary(16) NOT NULL,
  `UserGUID` binary(16) NOT NULL,
  `Permission` int(10) unsigned NOT NULL,
  `DateCreated` datetime NOT NULL,
  PRIMARY KEY (`SubscriptionGUID`,`UserGUID`),
  KEY `FK_Subscription_User_Join_SubscriptionGUID_Subscription_GUID` (`SubscriptionGUID`),
  KEY `FK_Subscription_User_Join_UserGUID_User_GUID` (`UserGUID`),
  CONSTRAINT `FK_Subscription_User_Join_SubscriptionGUID_Subscription_GUID` FOREIGN KEY (`SubscriptionGUID`) REFERENCES `Subscription` (`GUID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Subscription_User_Join_UserGUID_User_GUID` FOREIGN KEY (`UserGUID`) REFERENCES `User` (`GUID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB