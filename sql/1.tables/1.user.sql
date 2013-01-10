CREATE TABLE  User  
(
	GUID  			binary(16) NOT NULL,
	Email  			varchar(1024) NOT NULL,
	DateCreated  	datetime NOT NULL,
	
	PRIMARY KEY ( GUID ),
	UNIQUE KEY  GUID_UNIQUE  ( GUID )
) 
ENGINE=InnoDB