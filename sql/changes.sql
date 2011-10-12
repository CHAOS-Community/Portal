CREATE TABLE [dbo].[TicketType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] varchar(255)NOT NULL,
 CONSTRAINT [PK_TicketType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Ticket](
	[GUID] [uniqueidentifier] NOT NULL,
	[TicketTypeID] [int] NOT NULL,
	[XML] [xml] NULL,
	[Callback] [varchar](max) NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateUsed] [datetime] NULL,
 CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED 
(
	[GUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Ticket]  WITH CHECK ADD  CONSTRAINT [FK_Ticket_TicketType] FOREIGN KEY([TicketTypeID])
REFERENCES [dbo].[TicketType] ([ID])
GO

ALTER TABLE [dbo].[Ticket] CHECK CONSTRAINT [FK_Ticket_TicketType]
GO

ALTER TABLE [dbo].[Ticket] ADD  CONSTRAINT [DF_Ticket_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO

CREATE VIEW [dbo].[TicketInfo]
AS
SELECT     dbo.Ticket.GUID, dbo.Ticket.TicketTypeID, dbo.TicketType.Name, dbo.Ticket.XML, dbo.Ticket.Callback, dbo.Ticket.DateCreated, dbo.Ticket.DateUsed
FROM         dbo.Ticket INNER JOIN
                      dbo.TicketType ON dbo.Ticket.TicketTypeID = dbo.TicketType.ID

GO

INSERT INTO [TicketType]([Name]) VALUES ('Change Password')
INSERT INTO [TicketType]([Name]) VALUES ('Create User')
GO

-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.10.07
--				This SP is used to create a Ticket
-- =============================================
CREATE PROCEDURE [dbo].[Ticket_Create]
	@TicketTypeID	int,
	@XML			xml          = null,
	@Callback		varchar(max) = null
AS
BEGIN
	
	DECLARE @GUID uniqueidentifier
	SET @GUID = NEWID()
	
	INSERT INTO [Ticket]([GUID],[TicketTypeID],[XML],[Callback],[DateCreated],[DateUsed])
         VALUES(@GUID,@TicketTypeID,@XML,@Callback,GETDATE(),null)

	SELECT @GUID AS [GUID]
	
END
GO

-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.10.07
--				This SP is used to get a Ticket
-- =============================================
CREATE PROCEDURE TicketInfo_Get
	@GUID		uniqueidentifier,
	@IsUnUsed	bit	
AS
BEGIN
	
	SELECT	*
      FROM	TicketInfo
     WHERE	[GUID] = @GUID AND 
			( @IsUnUsed = 1 AND DateUsed IS NULL )

END
GO

-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.10.11
--				This sp is used to update a ticket
-- =============================================
CREATE PROCEDURE Ticket_Update
	@WhereGUID	uniqueidentifier
AS
BEGIN
	
	UPDATE [Ticket]
       SET [DateUsed] = GETDATE()
	 WHERE [GUID] = @WhereGUID

	RETURN @@ROWCOUNT
	
END
GO

-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.07.18
-- 
-- =============================================
ALTER PROCEDURE [dbo].[PopulateWithDefaultData]

AS
BEGIN
	
IF( 1 = 1 )
BEGIN

	DELETE FROM [Session]
	DELETE FROM UserSettings
	DELETE FROM Subscription_User_Join
	DELETE FROM Subscription
	DELETE FROM [Group_User_Join]
	DELETE FROM [Group]
	DELETE FROM [User]
	DELETE FROM ClientSetting
	DELETE FROM Module
	DELETE FROM Extension
	DELETE FROM Permission
	DELETE FROM Ticket
	DELETE FROM TicketType

	DBCC CHECKIDENT ("TicketType", RESEED, 0)
	DBCC CHECKIDENT ("Subscription", RESEED,0)
	DBCC CHECKIDENT ("[User]", RESEED,0)
	DBCC CHECKIDENT ("[Group]", RESEED,0)
	DBCC CHECKIDENT ("Module", RESEED,0)
	DBCC CHECKIDENT ("Extension", RESEED,0)
	DBCC CHECKIDENT ("ClientSetting", RESEED,0)

	DECLARE @SubscriptionIdentifier UNIQUEIDENTIFIER
	SET @SubscriptionIdentifier = '9C4E8A99-A69B-41FD-B1C7-E28C54D1D304'

	INSERT INTO [Permission]([TableIdentifier],[RightName],[Permission],[Description]) VALUES ('Subscription','Create User',1,'Permissoin to Create new users')
	INSERT INTO [Permission]([TableIdentifier],[RightName],[Permission],[Description]) VALUES ('Subscription','Get',2,'Permissoin to Get Subscription')
	INSERT INTO [Permission]([TableIdentifier],[RightName],[Permission],[Description]) VALUES ('Subscription','Delete',4,'Permissoin to Delete Subscription')
	INSERT INTO [Permission]([TableIdentifier],[RightName],[Permission],[Description]) VALUES ('Subscription','Update',8,'Permissoin to Update Subscription')
	INSERT INTO [Permission]([TableIdentifier],[RightName],[Permission],[Description]) VALUES ('Subscription','MANAGE',16,'Permissoin to Manage Subscription')
	INSERT INTO [Permission]([TableIdentifier],[RightName],[Permission],[Description]) VALUES ('System','Create Group',1,'Permissoin to Create a Group')
	INSERT INTO [Permission]([TableIdentifier],[RightName],[Permission],[Description]) VALUES ('System','Create Subscription',2,'Permissoin to Create a Subscription')
	INSERT INTO [Permission]([TableIdentifier],[RightName],[Permission],[Description]) VALUES ('System','MANAGE',4,'Permissoin to Manage the system')
	INSERT INTO [Permission]([TableIdentifier],[RightName],[Permission],[Description]) VALUES ('Group','Delete',1,'Permissoin to Delete Group')
	INSERT INTO [Permission]([TableIdentifier],[RightName],[Permission],[Description]) VALUES ('Group','Update',2,'Permissoin to Update Group')
	INSERT INTO [Permission]([TableIdentifier],[RightName],[Permission],[Description]) VALUES ('Group','Get',4,'Permissoin to Get Group')
	INSERT INTO [Permission]([TableIdentifier],[RightName],[Permission],[Description]) VALUES ('Group','Add User',8,'Permission to Add a User to the group')
	INSERT INTO [Permission]([TableIdentifier],[RightName],[Permission],[Description]) VALUES ('Group','List Users',16,'Permission to list users in the group')

	EXECUTE Subscription_Insert @GUID = @SubscriptionIdentifier, @Name = 'Geckon'
	EXECUTE User_Insert @Guid = 'C0B231E9-7D98-4F52-885E-AF4837FAA352', @Firstname = 'Anonymous', @Email = 'Anonymous@Geckon.com'
	EXECUTE User_Insert @Guid = 'A0B231E9-7D98-4F52-885E-AF4837FAA352', @Firstname = 'Administrator', @Email = 'admin@Geckon.com'
	EXECUTE User_Insert @Guid = 'B0B231E9-7D98-4F52-885E-AF4837FAA352', @Firstname = 'Jesper', @Middlename = 'Fyhr', @Lastname ='Knudsen', @Email = 'jesper@Geckon.com'
	EXECUTE ClientSetting_Insert 'D157698A-86AC-4FDF-A304-F5EA9FB6E0F5','XML + HTML Errorcode',null
	EXECUTE Extension_Insert 'Extension', 'Geckon.Portal.Extensions.Standard.ExtensionExtension','Geckon.Portal.Extensions.Standard.dll'
	EXECUTE Extension_Insert 'Session', 'Geckon.Portal.Extensions.Standard.SessionExtension','Geckon.Portal.Extensions.Standard.dll'
	EXECUTE Extension_Insert 'Subscription', 'Geckon.Portal.Extensions.Standard.SubscriptionExtension','Geckon.Portal.Extensions.Standard.dll'
	EXECUTE Extension_Insert 'User', 'Geckon.Portal.Extensions.Standard.UserExtension','Geckon.Portal.Extensions.Standard.dll'
	EXECUTE Extension_Insert 'Group', 'Geckon.Portal.Extensions.Standard.GroupExtension','Geckon.Portal.Extensions.Standard.dll'
	EXECUTE Extension_Insert 'Location', 'Geckon.GeoLocator.PortalExtension.LocationExtension','Geckon.GeoLocator.PortalExtension.dll'
	EXECUTE Extension_Insert 'EmailPassword', 'Geckon.Portal.CHAOS.EmailPasswordExtension.Standard.EmailPasswordExtension','EmailPasswordExtension.dll'
	EXECUTE Extension_Insert 'Folder', 'Geckon.MCM.Extension.Folder.FolderExtension','Geckon.MCM.Extension.dll'
	EXECUTE Extension_Insert 'FolderType', 'Geckon.MCM.Extension.FolderType.FolderTypeExtension','Geckon.MCM.Extension.dll'
	EXECUTE Extension_Insert 'FormatType', 'Geckon.MCM.Extension.FormatType.FormatTypeExtension','Geckon.MCM.Extension.dll'
	EXECUTE Extension_Insert 'Language', 'Geckon.MCM.Extension.Language.LanguageExtension','Geckon.MCM.Extension.dll'
	EXECUTE Extension_Insert 'SecureCookie', 'Geckon.Portal.CHAOS.SecureCookieExtension.Standard.SecureCookieExtension','SecureCookieExtension.dll'
	EXECUTE Extension_Insert 'ObjectRelationType', 'Geckon.MCM.Extension.ObjectRelationType.ObjectRelationTypeExtension','Geckon.MCM.Extension.dll'
	EXECUTE Extension_Insert 'ObjectType', 'Geckon.MCM.Extension.ObjectType.ObjectTypeExtension','Geckon.MCM.Extension.dll'
	
	EXECUTE Module_Insert 'EmailPassword', '<Settings ConnectionString="Data Source=10.4.0.2;Initial Catalog=EmailPassword;Persist Security Info=True;User ID=sa;Password=GECKONpbvu7000" ChangePasswordRequestSubject="Password change request" SMTPPassword="NOREPLYpbvu7000" FromEmailAddress="no-reply@chaos-community.org"><ChangePasswordRequestEmail><![CDATA[<?xml version="1.0" encoding="UTF-16"?><html xsl:version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns="http://www.w3.org/1999/xhtml"><body><a><xsl:attribute name="href"><xsl:value-of select="//URL"/>&amp;TicketGUID=<xsl:value-of select="//TicketInfo/GUID"/></xsl:attribute>Follow the white rabbit</a></body></html>]]></ChangePasswordRequestEmail></Settings>', 'EmailPasswordModule.dll'
	EXECUTE Module_Insert 'GeoLocator', '<Settings ConnectionString="Data Source=10.4.0.1;Initial Catalog=GeoLocator;User ID=Application;Password=-l:bCU''S\923pc[0"/>', 'Geckon.GeoLocator.dll'
	EXECUTE Module_Insert 'MCM', '<Settings ConnectionString="Data Source=10.4.0.2;Initial Catalog=MCM3;Persist Security Info=True;User ID=sa;Password=GECKONpbvu7000"/>', 'Geckon.MCM.Module.dll'
	EXECUTE Module_Insert 'SecureCookie', '<Settings ConnectionString="Data Source=10.4.0.2;Initial Catalog=SecureCookie;Persist Security Info=True;User ID=sa;Password=GECKONpbvu7000"/>', 'SecureCookieModule.dll'
	EXECUTE [UserSettings_Create] @UserGUID = 'A0B231E9-7D98-4F52-885E-AF4837FAA352' ,@ClientSettingsGUID = 'D157698A-86AC-4FDF-A304-F5EA9FB6E0F5' ,@Setting = '<xml />' 
	INSERT INTO [Subscription_User_Join]([SubscriptionID],[UserID],[Permission],[DateCreated]) VALUES (1,2,-1,GETDATE())

	EXECUTE Group_Insert @GUID = 'A0B231E9-7D98-4F52-885E-AAAAAAAAAAAA', @Name = 'Administrators', @SystemPermission = -1
	
	INSERT INTO [Group_User_Join] ([GroupID],[UserID],[Permission],[DateCreated])VALUES (1,2,-1,GETDATE())
	INSERT INTO [Group_User_Join] ([GroupID],[UserID],[Permission],[DateCreated])VALUES (1,3,-1,GETDATE())
	
	INSERT INTO [TicketType]([Name]) VALUES ('Change Password')
	INSERT INTO [TicketType]([Name]) VALUES ('Create User')
	
END

END