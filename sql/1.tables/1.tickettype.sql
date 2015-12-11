CREATE TABLE  TicketType  
(
	ID  	int(10) unsigned NOT NULL AUTO_INCREMENT,
	Name  	varchar(255) NOT NULL,
	
	PRIMARY KEY ( ID ),
	UNIQUE KEY  ID_UNIQUE  ( ID )
) 
ENGINE=InnoDB