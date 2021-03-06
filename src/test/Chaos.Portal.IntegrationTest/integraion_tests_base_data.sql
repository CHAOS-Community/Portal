﻿INSERT INTO `User`(GUID, Email, DateCreated) VALUES 
(unhex('00000000000000000000000000000001'),'test@test.test','2000-01-01 00:00:00');
INSERT INTO `Group`(GUID,SystemPermission,Name,DateCreated) VALUES 
(unhex('00000000000000000000000000000010'),255,'test group','2000-01-01 00:00:00');
INSERT INTO `Group`(GUID,SystemPermission,Name,DateCreated) VALUES 
(unhex('00000000000000000000000000000020'),255,'test group 2','2000-01-01 00:00:00');
INSERT INTO `Subscription`(GUID,Name,DateCreated) VALUES
(unhex('00000000000000000000000000010000'),'test subscription','2000-01-01 00:00:00');
INSERT INTO `Group_User_Join`(GroupGUID,UserGUID,Permission,DateCreated) VALUES 
(unhex('00000000000000000000000000000010'),unhex('00000000000000000000000000000001'),255,'2000-01-01 00:00:00');
INSERT INTO `Group_User_Join`(GroupGUID,UserGUID,Permission,DateCreated) VALUES 
(unhex('00000000000000000000000000000020'),unhex('00000000000000000000000000000001'),255,'2000-01-01 00:00:00');
INSERT INTO `Session`(GUID,UserGUID,DateCreated,DateModified) VALUES
(unhex('00000000000000000000000000000100'),unhex('00000000000000000000000000000001'),NOW(),null);
INSERT INTO `ClientSettings`(GUID,Name,Settings,DateCreated) VALUES
(unhex('00000000000000000000000000001000'),'test client','test settings','2000-01-01 00:00:00');
INSERT INTO `Subscription_User_Join`(SubscriptionGUID,UserGUID,Permission,DateCreated)VALUES
(unhex('00000000000000000000000000010000'),unhex('00000000000000000000000000000001'),4294967296,'2000-01-01 00:00:00');
INSERT INTO `UserSettings`(ClientSettingsGUID,UserGUID,Settings,DateCreated)VALUES
(unhex('00000000000000000000000000001000'),unhex('00000000000000000000000000000001'),'user settings','2000-01-01 00:00:00');
INSERT INTO `Module`(ID, Name,Configuration,DateCreated)VALUES
(1,'test module','test configuration','2000-01-01 00:00:00');