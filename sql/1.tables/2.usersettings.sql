CREATE TABLE  UserSettings  
(
	ClientSettingsGUID 	binary(16) NOT NULL,
	UserGUID  			binary(16) NOT NULL,
	Settings  			text,
	DateCreated  		datetime NOT NULL,

	PRIMARY KEY ( ClientSettingsGUID , UserGUID ),
	KEY  FK_UserSettings_UserGUID_User_GUID  ( UserGUID ),
	KEY  FK_UserSettings_ClientSettingsGUID_ClientSettings_GUID  ( ClientSettingsGUID ),
	CONSTRAINT  FK_UserSettings_ClientSettingsGUID_ClientSettings_GUID  FOREIGN KEY ( ClientSettingsGUID ) REFERENCES  ClientSettings  ( GUID ),
	CONSTRAINT  FK_UserSettings_UserGUID_User_GUID  FOREIGN KEY ( UserGUID ) REFERENCES  User  ( GUID )
) 
ENGINE=InnoDB