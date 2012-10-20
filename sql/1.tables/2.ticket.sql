CREATE TABLE `Ticket` (
  `GUID` binary(16) NOT NULL,
  `TicketTypeID` int(10) unsigned NOT NULL,
  `XML` text,
  `Callback` varchar(2048) DEFAULT NULL,
  `DateCreated` datetime NOT NULL,
  `DateUsed` datetime DEFAULT NULL,
  PRIMARY KEY (`GUID`),
  UNIQUE KEY `GUID_UNIQUE` (`GUID`),
  KEY `FK_Ticket_TicketTypeID_TicketType_ID` (`TicketTypeID`),
  CONSTRAINT `FK_Ticket_TicketTypeID_TicketType_ID` FOREIGN KEY (`TicketTypeID`) REFERENCES `TicketType` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB