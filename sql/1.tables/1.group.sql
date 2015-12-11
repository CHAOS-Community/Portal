CREATE TABLE  `Group`
(
	GUID  binary(16) NOT NULL,
	SystemPermission  int(10) unsigned NOT NULL,
	Name  varchar(255) NOT NULL,
	DateCreated  datetime NOT NULL,

	PRIMARY KEY ( GUID ),
	UNIQUE KEY  GUID_UNIQUE  ( GUID )
) 
ENGINE=InnoDB