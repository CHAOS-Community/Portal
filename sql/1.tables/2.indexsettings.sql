CREATE TABLE  IndexSettings  
(
	ID  			int(10) unsigned NOT NULL,
	ModuleID  		int(10) unsigned NOT NULL,
	Settings  		text,
	DateCreated  	datetime NOT NULL,
	
	PRIMARY KEY ( ID ),
	UNIQUE KEY  ID_UNIQUE  ( ID ),
	KEY  FK_IndexSettings_ModuleID_Module_ID  ( ModuleID ),
	CONSTRAINT  FK_IndexSettings_ModuleID_Module_ID  FOREIGN KEY ( ModuleID ) REFERENCES  Module  ( ID ) ON DELETE NO ACTION ON UPDATE NO ACTION
) 
ENGINE=InnoDB