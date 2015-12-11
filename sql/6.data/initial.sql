DELETE FROM Group_User_Join;
DELETE FROM Subscription_User_Join;
DELETE FROM Session;
DELETE FROM Ticket;
DELETE FROM UserSettings;
DELETE FROM `User`;
DELETE FROM TicketType;
DELETE FROM Subscription;
DELETE FROM Permission;
DELETE FROM Module;
DELETE FROM `Log`;
DELETE FROM `Group`;
DELETE FROM ClientSettings;

INSERT INTO Permission(TableIdentifier,Permission,Name,Description) VALUES('Group', '1', 'DELETE', 'Permission to Delete Group' );
INSERT INTO Permission(TableIdentifier,Permission,Name,Description) VALUES('Group', '2', 'UPDATE', 'Permission to Update Group');
INSERT INTO Permission(TableIdentifier,Permission,Name,Description) VALUES('Group', '4', 'GET', 'Permission to Get Group');
INSERT INTO Permission(TableIdentifier,Permission,Name,Description) VALUES('Group', '8', 'ADD_USER', 'Permission to Add a User to the group');
INSERT INTO Permission(TableIdentifier,Permission,Name,Description) VALUES('Group', '16', 'LIST_USER', 'Permission to list users in the group');
INSERT INTO Permission(TableIdentifier,Permission,Name,Description) VALUES('Group', '32', 'REMOVE_USER', 'Permission to remove user from the group');
INSERT INTO Permission(TableIdentifier,Permission,Name,Description) VALUES('Group', '64', 'UPDATE_USER', 'Permission to update users permissions to a group');
INSERT INTO Permission(TableIdentifier,Permission,Name,Description) VALUES('Subscription', '1', 'CREATE_USER', 'Permission to Create new users');
INSERT INTO Permission(TableIdentifier,Permission,Name,Description) VALUES('Subscription', '2', 'GET', 'Permission to Get Subscription');
INSERT INTO Permission(TableIdentifier,Permission,Name,Description) VALUES('Subscription', '4', 'DELETE', 'Permission to Delete Subscription');
INSERT INTO Permission(TableIdentifier,Permission,Name,Description) VALUES('Subscription', '8', 'UPDATE', 'Permission to Update Subscription');
INSERT INTO Permission(TableIdentifier,Permission,Name,Description) VALUES('Subscription', '16', 'MANAGE', 'Permission to Manage Subscription');
INSERT INTO Permission(TableIdentifier,Permission,Name,Description) VALUES('System', '1', 'CREATE_GROUP', 'Permission to Create a Group');
INSERT INTO Permission(TableIdentifier,Permission,Name,Description) VALUES('System', '2', 'CREATE_SUBSCRIPTION', 'Permission to Create a Subscription');
INSERT INTO Permission(TableIdentifier,Permission,Name,Description) VALUES('System', '4', 'MANAGE', 'Permissoin to Manage the system');

INSERT INTO 
	`Group`(`GUID`,`SystemPermission`,`Name`,`DateCreated`)
VALUES
	(unhex('2a2595ae9fed4006b57744ee7539c516'), 0,'User', NOW());

INSERT INTO 
	`Group`(`GUID`,`SystemPermission`,`Name`,`DateCreated`)
VALUES
	(unhex('a703cfdb476342a9b470369b77669059'), 4294967295,'System Administrator', NOW());


INSERT INTO 
	`User`(`GUID`,`Email`,`DateCreated`) 
VALUES 
	(unhex('E931B2C0987D524F885EAF4837FAA352'),'anon@ymo.us',NOW());

INSERT INTO 
	`User`(`GUID`,`Email`,`DateCreated`) 
VALUES 
	(unhex('34613336383661632D333562392D3131'),'admin',NOW());


INSERT INTO 
	`Group_User_Join` (`GroupGUID`,`UserGUID`,`Permission`,`DateCreated`)
VALUES
	(unhex('2a2595ae9fed4006b57744ee7539c516'), unhex('34613336383661632D333562392D3131'), 4294967295, NOW());

INSERT INTO 
	`Group_User_Join` (`GroupGUID`,`UserGUID`,`Permission`,`DateCreated`)
VALUES
	(unhex('a703cfdb476342a9b470369b77669059'), unhex('34613336383661632D333562392D3131'), 4294967295, NOW());