CREATE TABLE `Log` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) NOT NULL,
  `Level` varchar(50) NOT NULL,
  `SessionGUID` binary(16) DEFAULT NULL,
  `Duration` int(10) unsigned NOT NULL,
  `Message` text NOT NULL,
  `DateCreated` datetime NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `index2` (`Name`)
) ENGINE=InnoDB