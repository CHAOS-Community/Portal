CREATE TABLE  ClientSettings  
(
	GUID  binary(16) NOT NULL,
	Name  varchar(255) NOT NULL,
	Settings  text,
	DateCreated  datetime NOT NULL,

	PRIMARY KEY ( GUID ),
	UNIQUE KEY  GUID_UNIQUE  ( GUID )
) ENGINE=InnoDB