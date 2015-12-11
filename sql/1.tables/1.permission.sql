CREATE TABLE  Permission  
(
	TableIdentifier  	varchar(255) NOT NULL,
	Permission  		int(10) unsigned NOT NULL,
	Name  				varchar(255) NOT NULL,
	Description  		varchar(1024) DEFAULT NULL,

	PRIMARY KEY ( TableIdentifier , Permission )
) 
ENGINE=InnoDB