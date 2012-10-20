CREATE TABLE `Group_User_Join` (
  `GroupGUID` binary(16) NOT NULL,
  `UserGUID` binary(16) NOT NULL,
  `Permission` int(10) unsigned NOT NULL,
  `DateCreated` datetime NOT NULL,
  PRIMARY KEY (`GroupGUID`,`UserGUID`),
  KEY `FK_Group_User_Join_GroupGUID_Group_GUID` (`GroupGUID`),
  KEY `FK_Group_User_Join_UserGUID_User_GUID` (`UserGUID`),
  CONSTRAINT `FK_Group_User_Join_GroupGUID_Group_GUID` FOREIGN KEY (`GroupGUID`) REFERENCES `Group` (`GUID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Group_User_Join_UserGUID_User_GUID` FOREIGN KEY (`UserGUID`) REFERENCES `User` (`GUID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB