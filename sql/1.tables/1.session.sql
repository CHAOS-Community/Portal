CREATE TABLE `Session` (
  `GUID` binary(16) NOT NULL,
  `UserGUID` binary(16) NOT NULL,
  `DateCreated` datetime NOT NULL,
  `DateModified` datetime DEFAULT NULL,
  PRIMARY KEY (`GUID`),
  UNIQUE KEY `SessionGUID_UNIQUE` (`GUID`),
  KEY `FK_Session_UserGUID_User_GUID` (`UserGUID`)
) ENGINE=InnoDB