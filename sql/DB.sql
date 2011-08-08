USE [master]
GO
/****** Object:  Database [Portal]    Script Date: 08/08/2011 12:32:06 ******/
CREATE DATABASE [Portal] ON  PRIMARY 
( NAME = N'Portal', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\Portal.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Portal_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\Portal_log.ldf' , SIZE = 1536KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Portal] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Portal].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Portal] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [Portal] SET ANSI_NULLS OFF
GO
ALTER DATABASE [Portal] SET ANSI_PADDING OFF
GO
ALTER DATABASE [Portal] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [Portal] SET ARITHABORT OFF
GO
ALTER DATABASE [Portal] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [Portal] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [Portal] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [Portal] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [Portal] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [Portal] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [Portal] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [Portal] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [Portal] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [Portal] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [Portal] SET  DISABLE_BROKER
GO
ALTER DATABASE [Portal] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [Portal] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [Portal] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [Portal] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [Portal] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [Portal] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [Portal] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [Portal] SET  READ_WRITE
GO
ALTER DATABASE [Portal] SET RECOVERY FULL
GO
ALTER DATABASE [Portal] SET  MULTI_USER
GO
ALTER DATABASE [Portal] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [Portal] SET DB_CHAINING OFF
GO
USE [Portal]
GO
/****** Object:  ForeignKey [FK_Group_User_Join_Group]    Script Date: 08/08/2011 12:32:09 ******/
ALTER TABLE [dbo].[Group_User_Join] DROP CONSTRAINT [FK_Group_User_Join_Group]
GO
/****** Object:  ForeignKey [FK_Group_User_Join_User]    Script Date: 08/08/2011 12:32:09 ******/
ALTER TABLE [dbo].[Group_User_Join] DROP CONSTRAINT [FK_Group_User_Join_User]
GO
/****** Object:  ForeignKey [FK_ClientSetting_XmlType]    Script Date: 08/08/2011 12:32:09 ******/
ALTER TABLE [dbo].[ClientSetting] DROP CONSTRAINT [FK_ClientSetting_XmlType]
GO
/****** Object:  ForeignKey [FK_AuthenticationProvider_User_Join_AuthenticationProvider]    Script Date: 08/08/2011 12:32:09 ******/
ALTER TABLE [dbo].[AuthenticationProvider_User_Join] DROP CONSTRAINT [FK_AuthenticationProvider_User_Join_AuthenticationProvider]
GO
/****** Object:  ForeignKey [FK_AuthenticationProvider_User_Join_User]    Script Date: 08/08/2011 12:32:09 ******/
ALTER TABLE [dbo].[AuthenticationProvider_User_Join] DROP CONSTRAINT [FK_AuthenticationProvider_User_Join_User]
GO
/****** Object:  ForeignKey [FK_Subscription_User_Join_Subscription]    Script Date: 08/08/2011 12:32:09 ******/
ALTER TABLE [dbo].[Subscription_User_Join] DROP CONSTRAINT [FK_Subscription_User_Join_Subscription]
GO
/****** Object:  ForeignKey [FK_Subscription_User_Join_User]    Script Date: 08/08/2011 12:32:09 ******/
ALTER TABLE [dbo].[Subscription_User_Join] DROP CONSTRAINT [FK_Subscription_User_Join_User]
GO
/****** Object:  ForeignKey [FK_Session_ClientSetting]    Script Date: 08/08/2011 12:32:10 ******/
ALTER TABLE [dbo].[Session] DROP CONSTRAINT [FK_Session_ClientSetting]
GO
/****** Object:  ForeignKey [FK_Session_User]    Script Date: 08/08/2011 12:32:10 ******/
ALTER TABLE [dbo].[Session] DROP CONSTRAINT [FK_Session_User]
GO
/****** Object:  StoredProcedure [dbo].[Session_Get]    Script Date: 08/08/2011 12:32:10 ******/
DROP PROCEDURE [dbo].[Session_Get]
GO
/****** Object:  StoredProcedure [dbo].[UserInfo_Get]    Script Date: 08/08/2011 12:32:10 ******/
DROP PROCEDURE [dbo].[UserInfo_Get]
GO
/****** Object:  StoredProcedure [dbo].[Session_Delete]    Script Date: 08/08/2011 12:32:10 ******/
DROP PROCEDURE [dbo].[Session_Delete]
GO
/****** Object:  StoredProcedure [dbo].[User_Delete]    Script Date: 08/08/2011 12:32:10 ******/
DROP PROCEDURE [dbo].[User_Delete]
GO
/****** Object:  View [dbo].[UserInfo]    Script Date: 08/08/2011 12:32:10 ******/
DROP VIEW [dbo].[UserInfo]
GO
/****** Object:  StoredProcedure [dbo].[Session_Insert]    Script Date: 08/08/2011 12:32:10 ******/
DROP PROCEDURE [dbo].[Session_Insert]
GO
/****** Object:  StoredProcedure [dbo].[Session_Update]    Script Date: 08/08/2011 12:32:10 ******/
DROP PROCEDURE [dbo].[Session_Update]
GO
/****** Object:  View [dbo].[SessionInfo]    Script Date: 08/08/2011 12:32:10 ******/
DROP VIEW [dbo].[SessionInfo]
GO
/****** Object:  StoredProcedure [dbo].[Group_Delete]    Script Date: 08/08/2011 12:32:10 ******/
DROP PROCEDURE [dbo].[Group_Delete]
GO
/****** Object:  StoredProcedure [dbo].[Group_Update]    Script Date: 08/08/2011 12:32:10 ******/
DROP PROCEDURE [dbo].[Group_Update]
GO
/****** Object:  StoredProcedure [dbo].[PopulateWithDefaultData]    Script Date: 08/08/2011 12:32:10 ******/
DROP PROCEDURE [dbo].[PopulateWithDefaultData]
GO
/****** Object:  Table [dbo].[Session]    Script Date: 08/08/2011 12:32:10 ******/
ALTER TABLE [dbo].[Session] DROP CONSTRAINT [FK_Session_ClientSetting]
GO
ALTER TABLE [dbo].[Session] DROP CONSTRAINT [FK_Session_User]
GO
ALTER TABLE [dbo].[Session] DROP CONSTRAINT [DF_Session_DateCreated]
GO
ALTER TABLE [dbo].[Session] DROP CONSTRAINT [DF_Session_DateModified]
GO
DROP TABLE [dbo].[Session]
GO
/****** Object:  StoredProcedure [dbo].[Group_Get]    Script Date: 08/08/2011 12:32:09 ******/
DROP PROCEDURE [dbo].[Group_Get]
GO
/****** Object:  StoredProcedure [dbo].[AuthenticationProvider_User_Join_Get]    Script Date: 08/08/2011 12:32:09 ******/
DROP PROCEDURE [dbo].[AuthenticationProvider_User_Join_Get]
GO
/****** Object:  StoredProcedure [dbo].[AssociateUserWithGroup]    Script Date: 08/08/2011 12:32:09 ******/
DROP PROCEDURE [dbo].[AssociateUserWithGroup]
GO
/****** Object:  StoredProcedure [dbo].[ClientSetting_Insert]    Script Date: 08/08/2011 12:32:09 ******/
DROP PROCEDURE [dbo].[ClientSetting_Insert]
GO
/****** Object:  UserDefinedFunction [dbo].[DoesUserHavePermissionToActionOnSubscription]    Script Date: 08/08/2011 12:32:09 ******/
DROP FUNCTION [dbo].[DoesUserHavePermissionToActionOnSubscription]
GO
/****** Object:  UserDefinedFunction [dbo].[DoesUserHavePermissionToGroup]    Script Date: 08/08/2011 12:32:09 ******/
DROP FUNCTION [dbo].[DoesUserHavePermissionToGroup]
GO
/****** Object:  StoredProcedure [dbo].[User_AssociateWithAuthenticationProvider]    Script Date: 08/08/2011 12:32:09 ******/
DROP PROCEDURE [dbo].[User_AssociateWithAuthenticationProvider]
GO
/****** Object:  StoredProcedure [dbo].[User_Get]    Script Date: 08/08/2011 12:32:09 ******/
DROP PROCEDURE [dbo].[User_Get]
GO
/****** Object:  StoredProcedure [dbo].[User_Insert]    Script Date: 08/08/2011 12:32:09 ******/
DROP PROCEDURE [dbo].[User_Insert]
GO
/****** Object:  StoredProcedure [dbo].[User_Update]    Script Date: 08/08/2011 12:32:09 ******/
DROP PROCEDURE [dbo].[User_Update]
GO
/****** Object:  StoredProcedure [dbo].[XmlType_Insert]    Script Date: 08/08/2011 12:32:09 ******/
DROP PROCEDURE [dbo].[XmlType_Insert]
GO
/****** Object:  StoredProcedure [dbo].[Subscription_Insert]    Script Date: 08/08/2011 12:32:09 ******/
DROP PROCEDURE [dbo].[Subscription_Insert]
GO
/****** Object:  Table [dbo].[Subscription_User_Join]    Script Date: 08/08/2011 12:32:09 ******/
ALTER TABLE [dbo].[Subscription_User_Join] DROP CONSTRAINT [FK_Subscription_User_Join_Subscription]
GO
ALTER TABLE [dbo].[Subscription_User_Join] DROP CONSTRAINT [FK_Subscription_User_Join_User]
GO
ALTER TABLE [dbo].[Subscription_User_Join] DROP CONSTRAINT [DF_Subscription_User_Join_Permission]
GO
ALTER TABLE [dbo].[Subscription_User_Join] DROP CONSTRAINT [DF_Subscription_User_Join_DateCreated]
GO
DROP TABLE [dbo].[Subscription_User_Join]
GO
/****** Object:  StoredProcedure [dbo].[SubsystemRelation_Get]    Script Date: 08/08/2011 12:32:09 ******/
DROP PROCEDURE [dbo].[SubsystemRelation_Get]
GO
/****** Object:  StoredProcedure [dbo].[SubsystemRelation_Insert]    Script Date: 08/08/2011 12:32:09 ******/
DROP PROCEDURE [dbo].[SubsystemRelation_Insert]
GO
/****** Object:  StoredProcedure [dbo].[Extension_Get]    Script Date: 08/08/2011 12:32:09 ******/
DROP PROCEDURE [dbo].[Extension_Get]
GO
/****** Object:  StoredProcedure [dbo].[Extension_Insert]    Script Date: 08/08/2011 12:32:09 ******/
DROP PROCEDURE [dbo].[Extension_Insert]
GO
/****** Object:  UserDefinedFunction [dbo].[GetPermissionForAction]    Script Date: 08/08/2011 12:32:09 ******/
DROP FUNCTION [dbo].[GetPermissionForAction]
GO
/****** Object:  StoredProcedure [dbo].[AuthenticationProvider_Get]    Script Date: 08/08/2011 12:32:09 ******/
DROP PROCEDURE [dbo].[AuthenticationProvider_Get]
GO
/****** Object:  StoredProcedure [dbo].[AuthenticationProvider_Insert]    Script Date: 08/08/2011 12:32:09 ******/
DROP PROCEDURE [dbo].[AuthenticationProvider_Insert]
GO
/****** Object:  Table [dbo].[AuthenticationProvider_User_Join]    Script Date: 08/08/2011 12:32:09 ******/
ALTER TABLE [dbo].[AuthenticationProvider_User_Join] DROP CONSTRAINT [FK_AuthenticationProvider_User_Join_AuthenticationProvider]
GO
ALTER TABLE [dbo].[AuthenticationProvider_User_Join] DROP CONSTRAINT [FK_AuthenticationProvider_User_Join_User]
GO
DROP TABLE [dbo].[AuthenticationProvider_User_Join]
GO
/****** Object:  Table [dbo].[ClientSetting]    Script Date: 08/08/2011 12:32:09 ******/
ALTER TABLE [dbo].[ClientSetting] DROP CONSTRAINT [FK_ClientSetting_XmlType]
GO
ALTER TABLE [dbo].[ClientSetting] DROP CONSTRAINT [DF_ClientSetting_DateCreated]
GO
DROP TABLE [dbo].[ClientSetting]
GO
/****** Object:  StoredProcedure [dbo].[Group_Insert]    Script Date: 08/08/2011 12:32:09 ******/
DROP PROCEDURE [dbo].[Group_Insert]
GO
/****** Object:  Table [dbo].[Group_User_Join]    Script Date: 08/08/2011 12:32:09 ******/
ALTER TABLE [dbo].[Group_User_Join] DROP CONSTRAINT [FK_Group_User_Join_Group]
GO
ALTER TABLE [dbo].[Group_User_Join] DROP CONSTRAINT [FK_Group_User_Join_User]
GO
ALTER TABLE [dbo].[Group_User_Join] DROP CONSTRAINT [DF_Group_User_Join_Permission]
GO
ALTER TABLE [dbo].[Group_User_Join] DROP CONSTRAINT [DF_Group_User_Join_DateCreated]
GO
DROP TABLE [dbo].[Group_User_Join]
GO
/****** Object:  StoredProcedure [dbo].[Module_Get]    Script Date: 08/08/2011 12:32:09 ******/
DROP PROCEDURE [dbo].[Module_Get]
GO
/****** Object:  StoredProcedure [dbo].[Module_Insert]    Script Date: 08/08/2011 12:32:09 ******/
DROP PROCEDURE [dbo].[Module_Insert]
GO
/****** Object:  Table [dbo].[Permission]    Script Date: 08/08/2011 12:32:09 ******/
DROP TABLE [dbo].[Permission]
GO
/****** Object:  StoredProcedure [dbo].[RaiseLastError]    Script Date: 08/08/2011 12:32:09 ******/
DROP PROCEDURE [dbo].[RaiseLastError]
GO
/****** Object:  Table [dbo].[RelationType]    Script Date: 08/08/2011 12:32:09 ******/
DROP TABLE [dbo].[RelationType]
GO
/****** Object:  Table [dbo].[Module]    Script Date: 08/08/2011 12:32:09 ******/
ALTER TABLE [dbo].[Module] DROP CONSTRAINT [DF_Module_DateCreated]
GO
DROP TABLE [dbo].[Module]
GO
/****** Object:  Table [dbo].[AuthenticationProvider]    Script Date: 08/08/2011 12:32:08 ******/
ALTER TABLE [dbo].[AuthenticationProvider] DROP CONSTRAINT [DF_AuthenticationProvider_UniqueIdentifier]
GO
DROP TABLE [dbo].[AuthenticationProvider]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 08/08/2011 12:32:08 ******/
ALTER TABLE [dbo].[Group] DROP CONSTRAINT [DF_Group_UniqueIdentifier]
GO
ALTER TABLE [dbo].[Group] DROP CONSTRAINT [DF_Group_DateCreadted]
GO
DROP TABLE [dbo].[Group]
GO
/****** Object:  Table [dbo].[Extension]    Script Date: 08/08/2011 12:32:08 ******/
ALTER TABLE [dbo].[Extension] DROP CONSTRAINT [DF_Entrypoint_DateCreated]
GO
DROP TABLE [dbo].[Extension]
GO
/****** Object:  StoredProcedure [dbo].[ThrowException]    Script Date: 08/08/2011 12:32:08 ******/
DROP PROCEDURE [dbo].[ThrowException]
GO
/****** Object:  Table [dbo].[User]    Script Date: 08/08/2011 12:32:08 ******/
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[SubsystemRelation]    Script Date: 08/08/2011 12:32:08 ******/
ALTER TABLE [dbo].[SubsystemRelation] DROP CONSTRAINT [DF_SubsystemRelation_UniqueIdentifier]
GO
ALTER TABLE [dbo].[SubsystemRelation] DROP CONSTRAINT [DF__Subsystem__DateC__2E1BDC42]
GO
DROP TABLE [dbo].[SubsystemRelation]
GO
/****** Object:  Table [dbo].[Subscription]    Script Date: 08/08/2011 12:32:07 ******/
ALTER TABLE [dbo].[Subscription] DROP CONSTRAINT [DF_Repository_UniqueIdentifier]
GO
ALTER TABLE [dbo].[Subscription] DROP CONSTRAINT [DF_Repository_DateCreated]
GO
DROP TABLE [dbo].[Subscription]
GO
/****** Object:  Table [dbo].[XmlType]    Script Date: 08/08/2011 12:32:07 ******/
DROP TABLE [dbo].[XmlType]
GO
/****** Object:  Table [dbo].[XmlType]    Script Date: 08/08/2011 12:32:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[XmlType](
	[ID] [int] NOT NULL,
	[Name] [varchar](64) NULL,
 CONSTRAINT [PK_XmlType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Subscription]    Script Date: 08/08/2011 12:32:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Subscription](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Repository_UniqueIdentifier]  DEFAULT (newid()),
	[Name] [varchar](255) NOT NULL,
	[DateCreated] [datetime] NOT NULL CONSTRAINT [DF_Repository_DateCreated]  DEFAULT (getdate()),
 CONSTRAINT [PK_Repository] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [QK_Subscription_UniqueIdentifier_A] UNIQUE NONCLUSTERED 
(
	[GUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SubsystemRelation]    Script Date: 08/08/2011 12:32:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SubsystemRelation](
	[Object1Type] [varchar](128) NOT NULL,
	[Object1Field] [varchar](128) NOT NULL,
	[Object1Value] [int] NOT NULL,
	[Object2Type] [varchar](128) NOT NULL,
	[Object2Field] [varchar](128) NOT NULL,
	[Object2Value] [int] NOT NULL,
	[UniqueIdentifier] [uniqueidentifier] NOT NULL CONSTRAINT [DF_SubsystemRelation_UniqueIdentifier]  DEFAULT (newid()),
	[DateCreated] [datetime] NOT NULL DEFAULT (getdate()),
 CONSTRAINT [PK_SubsystemRelation] PRIMARY KEY CLUSTERED 
(
	[Object1Type] ASC,
	[Object1Field] ASC,
	[Object1Value] ASC,
	[Object2Type] ASC,
	[Object2Field] ASC,
	[Object2Value] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 08/08/2011 12:32:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL,
	[Firstname] [varchar](255) NOT NULL,
	[Middlename] [varchar](255) NULL,
	[Lastname] [varchar](255) NULL,
	[Email] [varchar](255) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [QK_Email_A] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [QK_User_UniqueIdentifier_A] UNIQUE NONCLUSTERED 
(
	[GUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[ThrowException]    Script Date: 08/08/2011 12:32:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Throws an error that will cause SqlException in .NET. Does not halt code execution unless wrapped in try-catch!
-- History: 2009.09.15 (RMJ)
--			2010.06.16 (RMJ)
-- 2011.03.03 RMJ - added to Portal
-- =============================================
create PROCEDURE [dbo].[ThrowException]
	@ErrorMessage nvarchar(MAX),
	@ErrorMessage2 nvarchar(MAX) = NULL
AS
BEGIN

	IF (@ErrorMessage2 IS NOT NULL)
	SET @ErrorMessage = @ErrorMessage + ' ' + @ErrorMessage2

	RAISERROR (@ErrorMessage, 16, 1)

END
GO
/****** Object:  Table [dbo].[Extension]    Script Date: 08/08/2011 12:32:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Extension](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Map] [varchar](255) NOT NULL,
	[Fullname] [varchar](255) NOT NULL,
	[Path] [varchar](1024) NOT NULL,
	[DateCreated] [datetime] NOT NULL CONSTRAINT [DF_Entrypoint_DateCreated]  DEFAULT (getdate()),
 CONSTRAINT [PK_Entrypoint] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [QK_Extension_Map_A] UNIQUE NONCLUSTERED 
(
	[Map] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Group]    Script Date: 08/08/2011 12:32:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Group](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_Group_UniqueIdentifier]  DEFAULT (newid()),
	[Name] [varchar](max) NOT NULL,
	[DateCreadted] [datetime] NOT NULL CONSTRAINT [DF_Group_DateCreadted]  DEFAULT (getdate()),
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [QK_Group_UniqueIdentifier_A] UNIQUE NONCLUSTERED 
(
	[GUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AuthenticationProvider]    Script Date: 08/08/2011 12:32:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AuthenticationProvider](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[GUID] [uniqueidentifier] NOT NULL CONSTRAINT [DF_AuthenticationProvider_UniqueIdentifier]  DEFAULT (newid()),
	[Title] [varchar](255) NULL,
 CONSTRAINT [PK_AuthenticationProvider] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [QK_AuthenticationProvider_UniqueIdentifier_A] UNIQUE NONCLUSTERED 
(
	[GUID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Module]    Script Date: 08/08/2011 12:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Module](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Configuration] [xml] NULL,
	[Path] [varchar](1024) NOT NULL,
	[DateCreated] [datetime] NOT NULL CONSTRAINT [DF_Module_DateCreated]  DEFAULT (getdate()),
 CONSTRAINT [PK_Module] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RelationType]    Script Date: 08/08/2011 12:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RelationType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_RelationType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[RaiseLastError]    Script Date: 08/08/2011 12:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- 2011.03.03 RMJ - added to Portal
create PROCEDURE [dbo].[RaiseLastError]
	@PrefixErrorMessage nvarchar(MAX) = NULL
AS
BEGIN

	DECLARE 
		@ErrMsg nvarchar(4000), 
		@ErrSeverity int
		
	SET @ErrMsg = @PrefixErrorMessage + CHAR(13)
		
	IF (@ErrMsg IS NULL)
		SET @ErrMsg = ''
	
	SET @ErrMsg = 'Line ' + CAST(ERROR_LINE() AS nvarchar(MAX)) + ': ' 
		
	SELECT 
		@ErrMsg = @ErrMsg + ERROR_MESSAGE(), 
		@ErrSeverity = ERROR_SEVERITY()

	RAISERROR(@ErrMsg, @ErrSeverity, 1)

END
GO
/****** Object:  Table [dbo].[Permission]    Script Date: 08/08/2011 12:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Permission](
	[TableIdentifier] [varchar](16) NOT NULL,
	[RightName] [varchar](64) NOT NULL,
	[Permission] [int] NOT NULL,
	[Description] [varchar](max) NULL,
 CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED 
(
	[TableIdentifier] ASC,
	[Permission] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[Module_Insert]    Script Date: 08/08/2011 12:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 27.04.2011
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[Module_Insert]
	@Name			varchar(255),
	@Configuration	xml,
	@Path			varchar(1024)
AS
BEGIN
	INSERT INTO [Module]
           ([Name]
           ,[Configuration]
           ,[Path]
           ,[DateCreated])
     VALUES
           (@Name
           ,@Configuration
           ,@Path
           ,GETDATE())
           
	SELECT	*
      FROM	[Module]
     WHERE	ID = @@IDENTITY  
     
END
GO
/****** Object:  StoredProcedure [dbo].[Module_Get]    Script Date: 08/08/2011 12:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Module_Get]
	@ID				int          = null,
	@Name			varchar(255) = null
AS
BEGIN

	SET NOCOUNT ON;

	SELECT	*
	  FROM	Module
	 WHERE	( @ID IS NULL OR ID = @ID ) AND
			( @Name IS NULL OR Name = @Name )
END
GO
/****** Object:  Table [dbo].[Group_User_Join]    Script Date: 08/08/2011 12:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group_User_Join](
	[GroupID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Permission] [int] NOT NULL CONSTRAINT [DF_Group_User_Join_Permission]  DEFAULT ((0)),
	[DateCreated] [datetime] NOT NULL CONSTRAINT [DF_Group_User_Join_DateCreated]  DEFAULT (getdate()),
 CONSTRAINT [PK_Group_User_Join] PRIMARY KEY CLUSTERED 
(
	[GroupID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[Group_Insert]    Script Date: 08/08/2011 12:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.07.22
-- 
-- =============================================
CREATE PROCEDURE [dbo].[Group_Insert]
	@GUID		uniqueidentifier = null,
	@Name		varchar(max)
AS
BEGIN
	
	IF( @GUID IS NULL )
		SET @GUID = NEWID()
	
	INSERT INTO [Group]([GUID],[Name],[DateCreadted])
     VALUES
           (@GUID
           ,@Name
           ,GETDATE())

	SELECT	*
	  FROM	[Group]
	 WHERE	ID = @@IDENTITY

END
GO
/****** Object:  Table [dbo].[ClientSetting]    Script Date: 08/08/2011 12:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientSetting](
	[ID] [int] NOT NULL,
	[XmlTypeID] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL CONSTRAINT [DF_ClientSetting_DateCreated]  DEFAULT (getdate()),
 CONSTRAINT [PK_ClientSetting] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AuthenticationProvider_User_Join]    Script Date: 08/08/2011 12:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[AuthenticationProvider_User_Join](
	[AuthenticationProviderID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[UniqueIdentifier] [varchar](255) NOT NULL,
 CONSTRAINT [PK_AuthenticationProvider_User_Join] PRIMARY KEY CLUSTERED 
(
	[AuthenticationProviderID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[AuthenticationProvider_Insert]    Script Date: 08/08/2011 12:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2010.08.06
-- Description:	This stored procedure inserts a authentication provider
-- =============================================
CREATE PROCEDURE [dbo].[AuthenticationProvider_Insert]
	@Title			varchar(255),
	@GUID			uniqueidentifier = NULL
	
AS
BEGIN

	IF( @GUID IS NULL )
		SET @GUID = NEWID()

	INSERT INTO [AuthenticationProvider]
				([Title],[GUID])
		 VALUES (@Title,@GUID)
		 
	SELECT	*
	  FROM	[AuthenticationProvider]
	 WHERE	[AuthenticationProvider].ID = @@IDENTITY

END
GO
/****** Object:  StoredProcedure [dbo].[AuthenticationProvider_Get]    Script Date: 08/08/2011 12:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2010.06.29
-- Description:	This stored procedure is ued to select from the AuthenticationProvider table
-- 2010.12.23 RMJ - removed database name 
-- =============================================
CREATE PROCEDURE [dbo].[AuthenticationProvider_Get]
	@ID				int			 = null,
	@Title			varchar(255) = null,
	@Fullname		varchar(512) = null,
	@AssemblyPath	varchar(512) = null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT	[AuthenticationProvider].*
	  FROM	[AuthenticationProvider]
	 WHERE	(@ID IS NULL OR [AuthenticationProvider].ID = @ID) AND
			(@Title IS NULL OR [AuthenticationProvider].Title = @Title) AND
			(@Fullname IS NULL OR [AuthenticationProvider].Fullname = @Fullname) AND
			(@AssemblyPath IS NULL OR [AuthenticationProvider].AssemblyPath = @AssemblyPath) 

END
GO
/****** Object:  UserDefinedFunction [dbo].[GetPermissionForAction]    Script Date: 08/08/2011 12:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2010.07.01
-- Description:	This function is used to select a permission
-- =============================================
CREATE FUNCTION [dbo].[GetPermissionForAction]
(
	@TableIdentifier	varchar(16),
	@RightName	    	varchar(64)
)
RETURNS int
AS
BEGIN
	DECLARE @Permission int

	SELECT	@Permission = [Permission].Permission
	  FROM	[Permission]
	 WHERE	[Permission].TableIdentifier = @TableIdentifier AND
			[Permission].RightName = @RightName

	RETURN @Permission

END
GO
/****** Object:  StoredProcedure [dbo].[Extension_Insert]    Script Date: 08/08/2011 12:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 27.04.2011
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[Extension_Insert]
	@Map			varchar(255),
	@Fullname       varchar(255),
	@Path			varchar(1024)
AS
BEGIN
	INSERT INTO Extension
           ([Map]
           ,[Fullname]
           ,[Path]
           ,[DateCreated])
     VALUES
           (@Map
           ,@Fullname
           ,@Path
           ,GETDATE())
           
	SELECT	*
	  FROM	Extension
	 WHERE	ID = @@IDENTITY
           
END
GO
/****** Object:  StoredProcedure [dbo].[Extension_Get]    Script Date: 08/08/2011 12:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 27.04.2011
-- Description:	
-- =============================================
CREATE PROCEDURE [dbo].[Extension_Get]
	@ID	  int         = null,
	@Map varchar(255) = null
AS
BEGIN

	SET NOCOUNT ON;

    SELECT	*
      FROM	Extension
     WHERE	( @ID IS NULL OR ID = @ID ) AND
			( @Map IS NULL OR Map = @Map )

END
GO
/****** Object:  StoredProcedure [dbo].[SubsystemRelation_Insert]    Script Date: 08/08/2011 12:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	
-- History: 2010.08.09 (RMJ)
-- =============================================
create PROCEDURE [dbo].[SubsystemRelation_Insert](
	@Object1Type varchar(128) ,
	@Object1Field varchar(128),
	@Object1Value int,
	@Object2Type varchar(128),
	@Object2Field varchar(128),
	@Object2Value int,
	@RelationTypeID int 
) AS
BEGIN

	INSERT INTO  [SubsystemRelation] (
		[Object1Type],
		[Object1Field],
		[Object1Value],
		[Object2Type],
		[Object2Field],
		[Object2Value],
		[RelationTypeID]
		) 
	OUTPUT 
		[inserted].*
	VALUES (
		@Object1Type,
		@Object1Field,
		@Object1Value,
		@Object2Type,
		@Object2Field,
		@Object2Value,
		@RelationTypeID
		)

END
GO
/****** Object:  StoredProcedure [dbo].[SubsystemRelation_Get]    Script Date: 08/08/2011 12:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	
-- 2010.08.09 (RMJ)
-- 2010.09.13 (RMJ)
-- 2010.10.06 RMJ
-- 2010.10.19 RMJ
-- 2010.11.19 RMJ - ordering is now descending on datecreated.
-- =============================================
CREATE PROCEDURE [dbo].[SubsystemRelation_Get](
	@Object1Type varchar(128) = NULL ,
	@Object1Field varchar(128) = NULL,
	@Object1Value int = NULL,
	@Object2Type varchar(128) = NULL,
	@Object2Field varchar(128) = NULL,
	@Object2Value int = NULL,
	@RelationTypeName nvarchar(MAX) = NULL,
	@Identifier uniqueidentifier = NULL,
	@PageSize int = NULL,
	@PageIndex int = NULL
) AS
BEGIN

	DECLARE @RelationTypeID int
	
	IF (@RelationTypeName IS NOT NULL)
	BEGIN
	
		IF NOT EXISTS(
			SELECT *
			FROM [RelationType]
			WHERE [Name] = @RelationTypeName
			)
			INSERT INTO [RelationType] (Name) VALUES (@RelationTypeName)
			
		SELECT @RelationTypeID = [ID]
		FROM [RelationType]
		WHERE [Name] = @RelationTypeName
	END
	
	IF (@PageSize IS NULL)
		SET @PageSize = 9999999
	IF (@PageIndex IS NULL)
		SET @PageIndex = 0		

	
	DECLARE 
		@FirstRowIndex int, 
		@LastRowIndex int
	
	SET	@FirstRowIndex = (@PageIndex * @PageSize)
	SET	@LastRowIndex = @FirstRowIndex + @PageSize -1


	;WITH RowNumberedRelation AS (
		SELECT 
			[UniqueIdentifier],
			ROW_NUMBER() OVER (ORDER BY [DateCreated] DESC) AS RowNumber
		FROM 
			[SubsystemRelation]
		WHERE
			(@Object1Type IS NULL OR [Object1Type] = @Object1Type)
		AND
			(@Object1Field IS NULL OR [Object1Field] = @Object1Field)
		AND
			(@Object1Value IS NULL OR [Object1Value] = @Object1Value)
		AND
			(@Object2Type IS NULL OR [Object2Type] = @Object2Type)
		AND
			(@Object2Field IS NULL OR [Object2Field] = @Object2Field)
		AND
			(@Object2Value IS NULL OR [Object2Value] = @Object2Value)
		AND
			(@RelationTypeID IS NULL OR [RelationTypeID] = @RelationTypeID)
		AND
			(@Identifier IS NULL OR [UniqueIdentifier] = @Identifier)
		)
		SELECT [SubsystemRelation].* 
		FROM [SubsystemRelation]
		INNER JOIN 
			[RowNumberedRelation] ON 
			[RowNumberedRelation].[UniqueIdentifier] = [SubsystemRelation].[UniqueIdentifier]
		WHERE
			[RowNumber] BETWEEN @FirstRowIndex AND @LastRowIndex
		
		
END
GO
/****** Object:  Table [dbo].[Subscription_User_Join]    Script Date: 08/08/2011 12:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subscription_User_Join](
	[SubscriptionID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[Permission] [int] NOT NULL CONSTRAINT [DF_Subscription_User_Join_Permission]  DEFAULT ((0)),
	[DateCreated] [datetime] NOT NULL CONSTRAINT [DF_Subscription_User_Join_DateCreated]  DEFAULT (getdate()),
 CONSTRAINT [PK_Subscription_User_Join] PRIMARY KEY CLUSTERED 
(
	[SubscriptionID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[Subscription_Insert]    Script Date: 08/08/2011 12:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.25.05
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Subscription_Insert]
	@GUID	uniqueidentifier,
	@Name	varchar(255)
AS
BEGIN

	SET NOCOUNT ON;

	INSERT INTO [Subscription] ([GUID],[Name],[DateCreated])
		 VALUES (@GUID,@Name,GETDATE())

	SELECT	*
	  FROM	Subscription
	 WHERE	ID = @@IDENTITY

END
GO
/****** Object:  StoredProcedure [dbo].[XmlType_Insert]    Script Date: 08/08/2011 12:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesper Fyhr	Knudsen
-- Create date: 2010.06.13
-- Description:	This stored procedure inserts a XmlType
-- =============================================
CREATE PROCEDURE [dbo].[XmlType_Insert]
	@ID		int,
	@Name	varchar(64)
AS
BEGIN

	INSERT INTO [XmlType](ID, Name)
		 VALUES (@ID,@Name)
		 
	SELECT	*
	  FROM	[XmlType]
	 WHERE	[XmlType].ID = @ID

END
GO
/****** Object:  StoredProcedure [dbo].[User_Update]    Script Date: 08/08/2011 12:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.07.18
--				Updates a user by GUID
-- =============================================
CREATE PROCEDURE [dbo].[User_Update]
	@WhereGUID		uniqueidentifier	= null,
	@NewGUID		uniqueidentifier	= null,
	@NewFirstname	varchar(255)		= null,
	@NewMiddlename	varchar(255)		= null,
	@NewLastname	varchar(255)		= null,
	@Newemail		varchar(255)		= null
AS
BEGIN
	
	UPDATE	[User]
	   SET	[GUID]       = ISNULL( @NewGUID, [GUID] ),
			[Firstname]  = ISNULL( @NewFirstname, [Firstname] ),
			[Middlename] = ISNULL( @NewMiddlename, [Middlename] ),
			[Lastname]   = ISNULL( @NewLastname, [Lastname] ),
			[Email]      = ISNULL( @Newemail, [Email] )
	 WHERE	[GUID] = @WhereGUID

	SELECT	*
	  FROM	[User]
	 WHERE	[GUID] = @WhereGUID
	
END
GO
/****** Object:  StoredProcedure [dbo].[User_Insert]    Script Date: 08/08/2011 12:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2010.06.08
-- Description:	This Stored procedure inserts a user
-- Update date: 2011.07.15
--              Removed CreatingUserID, anyone can create them selves, but they wont have any permissions
-- =============================================
CREATE PROCEDURE [dbo].[User_Insert]
--	@CreatingUserID			int			     = NULL,
	@GUID					uniqueidentifier = NULL,
	@Firstname				varchar(255),
	@Middlename				varchar(255)     = NULL,
	@Lastname				varchar(255)     = NULL,
	@Email					varchar(255)
AS
BEGIN
	
	IF( @GUID IS NULL )
		SET @GUID = NEWID()
	
--	IF( @CreatingUserID IS NOT NULL AND dbo.DoesUserHavePermissionToActionOnSubscription(@CreatingUserID,'Create User') = 0 )
--		RAISERROR ('The User does not have sufficient permissions to create users', 16, 1)
	
	INSERT INTO [User]
           ([GUID]
           ,[Firstname]
           ,[Middlename]
           ,[Lastname]
           ,[Email])
     VALUES
           (@GUID
           ,@Firstname
           ,@Middlename
           ,@Lastname
           ,@Email)

	SELECT	*
	  FROM	[User]
	 WHERE	[User].ID = @@IDENTITY
	
END
GO
/****** Object:  StoredProcedure [dbo].[User_Get]    Script Date: 08/08/2011 12:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2010.06.09
-- Description:	This stored procedure Gets a single user based on the unique identifier.
-- =============================================
CREATE PROCEDURE [dbo].[User_Get]
	@UserID				int          = NULL,
	@UniqueIdentifier	varchar(255) = NULL,
	@RepositoryID		int          = NULL
	
AS
BEGIN
	SET NOCOUNT ON;

	IF( @UniqueIdentifier IS NOT NULL )
	BEGIN
		SELECT	[User].*
		  FROM	[User] INNER JOIN 
					AuthenticationProvider_User_Join ON [User].ID = AuthenticationProvider_User_Join.UserID
		 WHERE	AuthenticationProvider_User_Join.UniqueIdentifier = @UniqueIdentifier AND
				( @UserID IS NULL OR [User].ID = @UserID ) AND
				( @RepositoryID IS NULL OR [User].RepositoryID = @RepositoryID )
	END
	ELSE
	BEGIN
		SELECT	[User].*
		  FROM	[User]
		 WHERE	( @UserID IS NULL OR [User].ID = @UserID ) AND
				( @RepositoryID IS NULL OR [User].RepositoryID = @RepositoryID )
	END
    
END
GO
/****** Object:  StoredProcedure [dbo].[User_AssociateWithAuthenticationProvider]    Script Date: 08/08/2011 12:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2010.06.08
-- Description:	This stored procedure inserts the relation between a user and 
--              the authentication provider
-- =============================================
CREATE PROCEDURE [dbo].[User_AssociateWithAuthenticationProvider]
	@UserGUID					uniqueidentifier,
	@AuthenticationProviderGUID	uniqueidentifier,
	@UniqueIdentifier			varchar(255)
AS
BEGIN

	DECLARE @UserID	INT
	SELECT  @UserID = ID
	  FROM	[User]
	 WHERE	[GUID] = @UserGUID

	DECLARE @AuthenticationProviderID INT
	SELECT	@AuthenticationProviderID = ID
	  FROM	AuthenticationProvider
	 WHERE	[GUID] = @AuthenticationProviderGUID

	INSERT INTO	[AuthenticationProvider_User_Join] ([AuthenticationProviderID],[UserID],[UniqueIdentifier])
		 VALUES (@AuthenticationProviderID, @UserID, @UniqueIdentifier)

	SELECT	*
	  FROM	[AuthenticationProvider_User_Join]
	 WHERE	[AuthenticationProvider_User_Join].UserID = @UserID AND
			[AuthenticationProvider_User_Join].AuthenticationProviderID = @AuthenticationProviderID

END
GO
/****** Object:  UserDefinedFunction [dbo].[DoesUserHavePermissionToGroup]    Script Date: 08/08/2011 12:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.08.08
-- 
-- =============================================
CREATE FUNCTION [dbo].[DoesUserHavePermissionToGroup]
(
	@UserID			int,
	@GroupID		int,
	@PermissionName	varchar(255)
)
RETURNS bit
AS
BEGIN
	-- Declare the return variable here
	DECLARE	@Result	INT

	-- Add the T-SQL statements to compute the return value here
	DECLARE @RequiredPermission INT
	
	SET @RequiredPermission = dbo.GetPermissionForAction('Group',@PermissionName)
	
	SELECT	@Result = COUNT(*)
	  FROM	Group_User_Join
	 WHERE	UserID  = @UserID AND
			GroupID = @GroupID AND
			Permission & @RequiredPermission = @RequiredPermission

	-- Return the result of the function
	RETURN @Result

END
GO
/****** Object:  UserDefinedFunction [dbo].[DoesUserHavePermissionToActionOnSubscription]    Script Date: 08/08/2011 12:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2010.07.01
-- Description:	This function check if a user has adequate permissions to an action on the repository
-- =============================================
CREATE FUNCTION [dbo].[DoesUserHavePermissionToActionOnSubscription]
(
	@UserID		int,
	@RightName	varchar(64)
)
RETURNS bit
AS
BEGIN
	DECLARE @Result bit
	DECLARE @RequiredPermission	int
	SET @RequiredPermission = dbo.GetPermissionForAction('Subscription',@RightName)
	
	DECLARE @ActualPermission int
	
	SELECT	@ActualPermission = Subscription_User_Join.Permission
	  FROM	[User] INNER JOIN
				Subscription_User_Join ON [User].ID = Subscription_User_Join.UserID
	 WHERE	[User].ID = @UserID

	IF( ( @RequiredPermission & @ActualPermission ) = @RequiredPermission )
		SET @Result = 1
	ELSE
		SET @Result = 0
	
	RETURN @Result

END
GO
/****** Object:  StoredProcedure [dbo].[ClientSetting_Insert]    Script Date: 08/08/2011 12:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesper Fyhr	Knudsen
-- Create date: 2010.06.13
-- Description:	This stored procedure inserts a ClientSetting
-- =============================================
CREATE PROCEDURE [dbo].[ClientSetting_Insert]
	@ID			int,
	@XmlTypeID	int
AS
BEGIN

	INSERT INTO [ClientSetting](ID, XmlTypeID)
		 VALUES (@ID,@XmlTypeID)
		 
	SELECT	*
	  FROM	[ClientSetting]
	 WHERE	[ClientSetting].ID = @ID

END
GO
/****** Object:  StoredProcedure [dbo].[AssociateUserWithGroup]    Script Date: 08/08/2011 12:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.07.22
-- 
-- =============================================
CREATE PROCEDURE [dbo].[AssociateUserWithGroup]
	@UserID		int = null,
	@GroupID	int	= null,
	@UserGUID	uniqueidentifier = null,
	@GroupGUID	uniqueidentifier = null,
	@Permission	int	= null
AS
BEGIN

	IF( ( @UserID IS NULL AND @UserGUID IS NULL ) OR ( @GroupID IS NULL AND @GroupGUID IS NULL ) )
		EXECUTE RaiseLastError 'Both userID and userGUID or groupID and groupGUID cant be null' 
		
	IF( @UserID IS NULL )
		SELECT	@UserID = ID
		  FROM	[User]
		 WHERE	[GUID] = @UserGUID
		
	IF( @GroupID IS NULL )
		SELECT	@GroupID = ID
		  FROM	[Group]
		 WHERE	[GUID] = @GroupGUID
		
	INSERT INTO [Group_User_Join] ([GroupID],[UserID],[Permission],[DateCreated])
    VALUES 
           (@GroupID
           ,@UserID
           ,ISNULL( @Permission, 0 )
           ,GETDATE())

	SELECT	*
	  FROM	[Group_User_Join]
	 WHERE	GroupID = @GroupID AND
			UserID  = @UserID

END
GO
/****** Object:  StoredProcedure [dbo].[AuthenticationProvider_User_Join_Get]    Script Date: 08/08/2011 12:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.07.15
--              .
-- =============================================
CREATE PROCEDURE [dbo].[AuthenticationProvider_User_Join_Get]
	@UserID						INT          = NULL,
	@AuthenticationProviderID	INT	         = NULL,
	@UniqueIdentifier			VARCHAR(255) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	*
	  FROM	[AuthenticationProvider_User_Join]
	 WHERE	( @UserID IS NULL OR UserID = @UserID ) AND
			( @AuthenticationProviderID IS NULL OR AuthenticationProviderID = @AuthenticationProviderID ) AND
			( @UniqueIdentifier IS NULL OR [UniqueIdentifier] = @UniqueIdentifier )

END
GO
/****** Object:  StoredProcedure [dbo].[Group_Get]    Script Date: 08/08/2011 12:32:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.07.20
--              Get Group 
-- =============================================
CREATE PROCEDURE [dbo].[Group_Get]
	@GroupGUID	uniqueidentifier = null,
	@UserGUID	uniqueidentifier = null
AS
	
	IF( @UserGUID IS NULL )
	BEGIN
	
		SELECT	*
		  FROM	[Group]
		 WHERE	( @GroupGUID IS NULL OR @GroupGUID = [GUID] ) 		
	END
	ELSE
	BEGIN
	
		DECLARE @UserID INT
		SELECT	@UserID = ID 
		  FROM	[User]
		 WHERE	[GUID] = @UserGUID
	
		SELECT	[Group].*
		  FROM	[Group] INNER JOIN
					Group_User_Join ON [Group].ID = Group_User_Join.GroupID
		 WHERE	( @UserID IS NULL OR @UserID = UserID )

	END
GO
/****** Object:  Table [dbo].[Session]    Script Date: 08/08/2011 12:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Session](
	[SessionID] [uniqueidentifier] NOT NULL,
	[UserID] [int] NOT NULL,
	[ClientSettingID] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL CONSTRAINT [DF_Session_DateCreated]  DEFAULT (getdate()),
	[DateModified] [datetime] NOT NULL CONSTRAINT [DF_Session_DateModified]  DEFAULT (getdate()),
 CONSTRAINT [PK_Session] PRIMARY KEY CLUSTERED 
(
	[SessionID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[PopulateWithDefaultData]    Script Date: 08/08/2011 12:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.07.18
-- 
-- =============================================
CREATE PROCEDURE [dbo].[PopulateWithDefaultData]

AS
BEGIN
	
IF( 1 = 1 )
BEGIN

	DELETE FROM [Session]
	DELETE FROM Subscription_User_Join
	DELETE FROM Subscription
	DELETE FROM AuthenticationProvider_User_Join
	DELETE FROM [Group_User_Join]
	DELETE FROM [Group]
	DELETE FROM [User]
	DELETE FROM ClientSetting
	DELETE FROM XmlType
	DELETE FROM Module
	DELETE FROM Extension
	DELETE FROM Permission
	DELETE FROM AuthenticationProvider

END

DBCC CHECKIDENT ("AuthenticationProvider", RESEED,0)
DBCC CHECKIDENT ("Subscription", RESEED,0)
DBCC CHECKIDENT ("[User]", RESEED,0)
DBCC CHECKIDENT ("[Group]", RESEED,0)
DBCC CHECKIDENT ("Module", RESEED,0)
DBCC CHECKIDENT ("Extension", RESEED,0)

DECLARE @SubscriptionIdentifier UNIQUEIDENTIFIER
SET @SubscriptionIdentifier = NEWID()

EXECUTE Subscription_Insert @GUID = @SubscriptionIdentifier, @Name = 'Geckon'
EXECUTE User_Insert @Guid = 'C0B231E9-7D98-4F52-885E-AF4837FAA352', @Firstname = 'Anonymous', @Email = 'Anonymous@Geckon.com'
EXECUTE User_Insert @Guid = 'A0B231E9-7D98-4F52-885E-AF4837FAA352', @Firstname = 'Administrator', @Email = 'admin@Geckon.com'
EXECUTE Group_Insert @GUID = 'A0B231E9-7D98-4F52-885E-AAAAAAAAAAAA', @Name = 'Administrators'
EXECUTE [AssociateUserWithGroup] @UserGUID = 'A0B231E9-7D98-4F52-885E-AF4837FAA352', @GroupGUID = 'A0B231E9-7D98-4F52-885E-AAAAAAAAAAAA', @Permission = -1
EXECUTE XmlType_Insert 1, 'XML'
EXECUTE ClientSetting_Insert 1, 1
EXECUTE Extension_Insert 'Session', 'Geckon.Portal.Extensions.Standard.SessionExtension','Geckon.Portal.Extensions.Standard.dll'
EXECUTE Extension_Insert 'User', 'Geckon.Portal.Extensions.Standard.UserExtension','Geckon.Portal.Extensions.Standard.dll'
EXECUTE Extension_Insert 'Group', 'Geckon.Portal.Extensions.Standard.GroupExtension','Geckon.Portal.Extensions.Standard.dll'
EXECUTE Extension_Insert 'Location', 'Geckon.GeoLocator.PortalExtension.LocationExtension','Geckon.GeoLocator.PortalExtension.dll'
EXECUTE Extension_Insert 'EmailPassword', 'Geckon.Portal.Extensions.Standard.EmailPasswordExtension','Geckon.Portal.Extensions.Standard.dll'
EXECUTE AuthenticationProvider_Insert 'Email Password', 'F9089905-3134-4A35-B475-9CA8EA9FDC26'
EXECUTE User_AssociateWithAuthenticationProvider @UserGUID = 'A0B231E9-7D98-4F52-885E-AF4837FAA352', @AuthenticationProviderGUID = 'F9089905-3134-4A35-B475-9CA8EA9FDC26', @UniqueIdentifier = '24ebbdee2640cdec50550a6c4bed6d3ab731342b'
EXECUTE Module_Insert 'GeoLocator', '<Settings ConnectionString="Data Source=mssql00;Initial Catalog=GeoLocator;User ID=Application;Password=-l:bCU''S\923pc[0"/>', 'Geckon.GeoLocator.dll'

INSERT INTO [Permission]([TableIdentifier],[RightName],[Permission],[Description]) VALUES ('Subscription','Create User',1,'Permissoin to Create new users')
INSERT INTO [Permission]([TableIdentifier],[RightName],[Permission],[Description]) VALUES ('Group','Delete',1,'Permissoin to Delete Group')
INSERT INTO [Permission]([TableIdentifier],[RightName],[Permission],[Description]) VALUES ('Group','Update',2,'Permissoin to Update Group')
	
END
GO
/****** Object:  StoredProcedure [dbo].[Group_Update]    Script Date: 08/08/2011 12:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.08.08
-- 
-- =============================================
CREATE PROCEDURE [dbo].[Group_Update] 
	@NewName		varchar(MAX),
	@GroupID	int					= null,
	@GroupGUID	uniqueidentifier	= null,
	@UserID			int					= null,
	@UserGUID		uniqueidentifier	= null,
	@ErrorCode		int output
AS
BEGIN

	SET NOCOUNT ON;

	IF( @GroupID IS NULL AND @GroupGUID IS NULL OR 
	    @UserID  IS NULL AND @UserGUID  IS NULL )
	BEGIN
	    EXECUTE RaiseLastError 'Both ID and GUID or UserID and UserGUID cant be null'
		
		SET @ErrorCode = -10
	END
	
	IF( @GroupGUID IS NOT NULL )
		SELECT	@GroupID = ID
		  FROM	[Group]
		 WHERE	[GUID] = @GroupGUID
	
	IF( @UserGUID IS NOT NULL )
		SELECT	@UserID = ID
		  FROM	[User]
		 WHERE	[GUID] = @UserGUID

    IF( dbo.DoesUserHavePermissionToGroup( @UserID,@GroupID, 'Update' ) = 0 )
	BEGIN
		EXECUTE RaiseLastError 'User does not have update permission to group'
		
		SET @ErrorCode = -100
	END
    
    UPDATE	[Group]
	   SET	Name = ISNULL( @NewName, Name )
	 WHERE	ID = @GroupID

	SELECT	*
	  FROM	[Group]
	 WHERE	ID = @GroupID
    
END
GO
/****** Object:  StoredProcedure [dbo].[Group_Delete]    Script Date: 08/08/2011 12:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.08.08
--				Deletes a group
-- =============================================
CREATE PROCEDURE [dbo].[Group_Delete]
	@ID			int					= null,
	@GUID		uniqueidentifier	= null,
	@UserID		int					= null,
	@UserGUID	uniqueidentifier	= null
AS
BEGIN
	
	IF( @ID     IS NULL AND @GUID     IS NULL OR 
	    @UserID IS NULL AND @UserGUID IS NULL )
	BEGIN
	    EXECUTE RaiseLastError 'Both ID and GUID or UserID and UserGUID cant be null'
		
		RETURN -100
	END
	
	IF( @GUID IS NOT NULL )
		SELECT	@ID = ID
		  FROM	[Group]
		 WHERE	[GUID] = @GUID
	
	IF( @UserGUID IS NOT NULL )
		SELECT	@UserID = ID
		  FROM	[User]
		 WHERE	[GUID] = @UserGUID

	IF( dbo.DoesUserHavePermissionToGroup( @UserID,@ID, 'Delete' ) = 0 )
	BEGIN
		EXECUTE RaiseLastError 'User does not have delete permíssion to group'
		
		RETURN -100
	END

	DELETE
	  FROM	Group_User_Join
	 WHERE	GroupID = @ID

    DELETE
      FROM	[Group]
     WHERE	[ID] = @ID
			
	RETURN @@ROWCOUNT
    
END
GO
/****** Object:  View [dbo].[SessionInfo]    Script Date: 08/08/2011 12:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[SessionInfo]
AS
SELECT     SessionID, UserID, ClientSettingID, DateCreated, DateModified, DATEDIFF(minute, DateModified, GETDATE()) AS MinutesSinceRenewal
FROM         dbo.Session
GO
/****** Object:  StoredProcedure [dbo].[Session_Update]    Script Date: 08/08/2011 12:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2010.07.07
-- Description:	This stored procedure is used to update a session
-- =============================================
CREATE PROCEDURE [dbo].[Session_Update]
	@SessionID				uniqueidentifier = NULL,
	@UserGUID				uniqueidentifier = NULL,
	@ClientSettingID		int				 = NULL,
	@WhereSessionID			uniqueidentifier = NULL,
	@WhereUserGUID			uniqueidentifier = NULL,
	@WhereClientSettingID	int				 = NULL
AS
BEGIN

	IF( @WhereSessionID IS NULL AND @WhereUserGUID IS NULL AND @WhereClientSettingID iS NULL )
		RAISERROR ('At least one where parameter must be set', 16, 1)

	DECLARE @UserID INT
	DECLARE @WhereUserID INT
	
	SELECT	@UserID = [User].ID
	  FROM	[User]
	 WHERE	[User].GUID = @UserGUID
		
	SELECT	@WhereUserID = [User].ID
	  FROM	[User]
	 WHERE	[User].GUID = @WhereUserGUID

	UPDATE [Session]
	   SET [SessionID]       = ISNULL(@SessionID,[SessionID])
		  ,[UserID]          = ISNULL(@UserID,[UserID] )
		  ,[ClientSettingID] = ISNULL(@ClientSettingID,[ClientSettingID])
		  ,[DateModified]    = getdate()
	 WHERE ( @WhereSessionID IS NULL OR [Session].SessionID = @WhereSessionID ) AND
		   ( @WhereUserID IS NULL OR [Session].UserID = @WhereUserID ) AND
		   ( @WhereClientSettingID IS NULL OR [Session].ClientSettingID = @WhereClientSettingID )
		   
	 SELECT	*
	   FROM	[Session]
	  WHERE ( @WhereSessionID IS NULL OR [Session].SessionID = @WhereSessionID ) AND
		    ( @WhereUserID IS NULL OR [Session].UserID = @WhereUserID ) AND
		    ( @WhereClientSettingID IS NULL OR [Session].ClientSettingID = @WhereClientSettingID )

END
GO
/****** Object:  StoredProcedure [dbo].[Session_Insert]    Script Date: 08/08/2011 12:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2010.06.13
-- Description:	This stored procedure inserts a new session
-- =============================================
CREATE PROCEDURE [dbo].[Session_Insert]
	@SessionID			uniqueidentifier = NULL,
	@UserGUID			uniqueidentifier,
	@ClientSettingID	int
AS
BEGIN

	IF( @SessionID IS NULL )
		SET @SessionID = NEWID()

	DECLARE @UserID INT
	
	SELECT	@UserID = ID
	  FROM	[User]
	 WHERE	[User].GUID = @UserGUID

	INSERT
	  INTO	[Session]([SessionID],[UserID],[ClientSettingID])
	VALUES	(@SessionID,@UserID, @ClientSettingID)
	
	SELECT	*
	  FROM	[Session]
	 WHERE	[Session].SessionID = @SessionID

END
GO
/****** Object:  View [dbo].[UserInfo]    Script Date: 08/08/2011 12:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[UserInfo]
AS
SELECT     dbo.[User].ID, dbo.[User].GUID, dbo.Session.SessionID, dbo.[User].Firstname, dbo.[User].Middlename, dbo.[User].Lastname, dbo.[User].Email, 
                      dbo.Session.ClientSettingID, dbo.Session.DateModified, dbo.Session.DateCreated AS SessionDateCreated
FROM         dbo.[User] LEFT OUTER JOIN
                      dbo.Session ON dbo.[User].ID = dbo.Session.UserID
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[42] 4[21] 2[15] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Session"
            Begin Extent = 
               Top = 24
               Left = 583
               Bottom = 190
               Right = 744
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "User"
            Begin Extent = 
               Top = 6
               Left = 242
               Bottom = 125
               Right = 402
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 18
         Width = 284
         Width = 1500
         Width = 3165
         Width = 3150
         Width = 1035
         Width = 885
         Width = 555
         Width = 1725
         Width = 1380
         Width = 900
         Width = 1275
         Width = 1125
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'UserInfo'
GO
/****** Object:  StoredProcedure [dbo].[User_Delete]    Script Date: 08/08/2011 12:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.07.01
-- Description:	Creates a user
-- =============================================
CREATE PROCEDURE [dbo].[User_Delete]
	@GUID		uniqueidentifier
AS
BEGIN
	
	DELETE
	  FROM	[Session]
	 WHERE	UserID IN ( SELECT	UserID
	                      FROM	[User]
	                     WHERE	[GUID] = @GUID )
	
	DELETE
	  FROM	AuthenticationProvider_User_Join
	 WHERE	UserID IN ( SELECT	UserID
	                      FROM	[User]
	                     WHERE	[GUID] = @GUID )
	
	DELETE 
	  FROM	[User]
     WHERE  [User].GUID = @GUID
     
     RETURN @@ROWCOUNT
	
END
GO
/****** Object:  StoredProcedure [dbo].[Session_Delete]    Script Date: 08/08/2011 12:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2010.06.29
-- Description:	This stored procedure will delete
-- =============================================
CREATE PROCEDURE [dbo].[Session_Delete]
	@SessionID			uniqueidentifier	= null,
	@UserID				int					= null,
	@ClientSettingID	int					= null
AS
BEGIN

	IF( @SessionID IS NULL AND @UserID IS NULL AND @ClientSettingID IS NULL )
		RAISERROR ('Either @SessionID, @UserID or @ClientSettingID must be set', 16, 1)
	
	DELETE FROM	[Session]
		  WHERE ( @SessionID IS NULL OR [Session].SessionID = @SessionID ) AND
				( @UserID IS NULL OR [Session].UserID = @UserID ) AND
				( @ClientSettingID IS NULL OR [Session].ClientSettingID = @ClientSettingID ) 

	RETURN @@ROWCOUNT
	
END
GO
/****** Object:  StoredProcedure [dbo].[UserInfo_Get]    Script Date: 08/08/2011 12:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =======================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2010.06.09
-- Description:	This stored procedure Gets a single user based on the unique identifier.
-- =============================================
CREATE PROCEDURE [dbo].[UserInfo_Get]
	@GUID									uniqueidentifier = NULL,
	@SessionID								uniqueidentifier = NULL,
	@Email									varchar(255)	 = NULL,
	@AuthenticationProviderUniqueidentifier	varchar(255)	 = NULL,
	@AuthenticationProviderGUID				uniqueidentifier = NULL
AS
BEGIN

	SET NOCOUNT ON;

	DECLARE @AuthenticationProviderID int
	SELECT	@AuthenticationProviderID = ID
	  FROM	AuthenticationProvider
	 WHERE	[GUID] = @AuthenticationProviderGUID

	SELECT	[UserInfo].*
	  FROM	[UserInfo]
	 WHERE	( @GUID IS NULL OR [UserInfo].[GUID] = @GUID ) AND
			( @SessionID IS NULL OR [UserInfo].SessionID = @SessionID ) AND
			( @Email IS NULL OR [UserInfo].Email = @Email ) AND
			( @AuthenticationProviderUniqueidentifier IS NULL OR [UserInfo].ID IN ( SELECT UserID
																					  FROM AuthenticationProvider_User_Join
																					 WHERE [UniqueIdentifier] = @AuthenticationProviderUniqueidentifier AND
																					       AuthenticationProviderID = @AuthenticationProviderID ) ) 
    
END
GO
/****** Object:  StoredProcedure [dbo].[Session_Get]    Script Date: 08/08/2011 12:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2010.07.01
-- Description:	This stored procedure is used to select sessions
-- =============================================
CREATE PROCEDURE [dbo].[Session_Get]
	@SessionID			uniqueidentifier = NULL,
	@UserID				int				 = NULL,
	@ClientSettingID	int				 = NULL,
	@PageIndex			int				 = 0,
	@PageSize			int				 = 10,
	@TotalCount			int	output

AS
BEGIN

	DELETE
	  FROM	[Session]
	 WHERE	SessionID IN ( SELECT	SessionID
							 FROM	SessionInfo
							WHERE	MinutesSinceRenewal > 20 )

	IF( @PageIndex IS NULL )
		SET @PageIndex = 0
		
	IF( @PageSize IS NULL )
		SET @PageSize = 10;

	DECLARE @PagedResults AS TABLE (
		[RowNumber]			int,
		[TotalCount]		int,
	    [SessionID]			uniqueidentifier,
        [UserID]			int,
        [ClientSettingID]	int,
        [DateCreated]		datetime,
        [DateModified]		datetime
	);

	WITH OrdersRN AS
	(
		SELECT ROW_NUMBER() OVER(ORDER BY SessionID, SessionID) AS RowNumber,
			   COUNT(*) OVER() AS TotalCount,
			   [SessionID],[UserID],[ClientSettingID],[DateCreated],[DateModified]
		  FROM	[Session]
		 WHERE	( @SessionID IS NULL OR [Session].SessionID = @SessionID ) AND
				( @UserID IS NULL OR [Session].UserID = @UserID ) AND
				( @ClientSettingID IS NULL OR [Session].ClientSettingID = @ClientSettingID )
	)

	INSERT INTO @PagedResults
		SELECT	* 
		  FROM	OrdersRN
		 WHERE RowNumber BETWEEN (@PageIndex) * @PageSize + 1 
					     AND (@PageIndex + 1) * @PageSize
	  
	SELECT TOP 1 @TotalCount = TotalCount
	  FROM	@PagedResults
	  
	SELECT	*
	  FROM	@PagedResults  
END
GO
/****** Object:  ForeignKey [FK_Group_User_Join_Group]    Script Date: 08/08/2011 12:32:09 ******/
ALTER TABLE [dbo].[Group_User_Join]  WITH CHECK ADD  CONSTRAINT [FK_Group_User_Join_Group] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Group] ([ID])
GO
ALTER TABLE [dbo].[Group_User_Join] CHECK CONSTRAINT [FK_Group_User_Join_Group]
GO
/****** Object:  ForeignKey [FK_Group_User_Join_User]    Script Date: 08/08/2011 12:32:09 ******/
ALTER TABLE [dbo].[Group_User_Join]  WITH CHECK ADD  CONSTRAINT [FK_Group_User_Join_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Group_User_Join] CHECK CONSTRAINT [FK_Group_User_Join_User]
GO
/****** Object:  ForeignKey [FK_ClientSetting_XmlType]    Script Date: 08/08/2011 12:32:09 ******/
ALTER TABLE [dbo].[ClientSetting]  WITH CHECK ADD  CONSTRAINT [FK_ClientSetting_XmlType] FOREIGN KEY([XmlTypeID])
REFERENCES [dbo].[XmlType] ([ID])
GO
ALTER TABLE [dbo].[ClientSetting] CHECK CONSTRAINT [FK_ClientSetting_XmlType]
GO
/****** Object:  ForeignKey [FK_AuthenticationProvider_User_Join_AuthenticationProvider]    Script Date: 08/08/2011 12:32:09 ******/
ALTER TABLE [dbo].[AuthenticationProvider_User_Join]  WITH CHECK ADD  CONSTRAINT [FK_AuthenticationProvider_User_Join_AuthenticationProvider] FOREIGN KEY([AuthenticationProviderID])
REFERENCES [dbo].[AuthenticationProvider] ([ID])
GO
ALTER TABLE [dbo].[AuthenticationProvider_User_Join] CHECK CONSTRAINT [FK_AuthenticationProvider_User_Join_AuthenticationProvider]
GO
/****** Object:  ForeignKey [FK_AuthenticationProvider_User_Join_User]    Script Date: 08/08/2011 12:32:09 ******/
ALTER TABLE [dbo].[AuthenticationProvider_User_Join]  WITH CHECK ADD  CONSTRAINT [FK_AuthenticationProvider_User_Join_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[AuthenticationProvider_User_Join] CHECK CONSTRAINT [FK_AuthenticationProvider_User_Join_User]
GO
/****** Object:  ForeignKey [FK_Subscription_User_Join_Subscription]    Script Date: 08/08/2011 12:32:09 ******/
ALTER TABLE [dbo].[Subscription_User_Join]  WITH CHECK ADD  CONSTRAINT [FK_Subscription_User_Join_Subscription] FOREIGN KEY([SubscriptionID])
REFERENCES [dbo].[Subscription] ([ID])
GO
ALTER TABLE [dbo].[Subscription_User_Join] CHECK CONSTRAINT [FK_Subscription_User_Join_Subscription]
GO
/****** Object:  ForeignKey [FK_Subscription_User_Join_User]    Script Date: 08/08/2011 12:32:09 ******/
ALTER TABLE [dbo].[Subscription_User_Join]  WITH CHECK ADD  CONSTRAINT [FK_Subscription_User_Join_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Subscription_User_Join] CHECK CONSTRAINT [FK_Subscription_User_Join_User]
GO
/****** Object:  ForeignKey [FK_Session_ClientSetting]    Script Date: 08/08/2011 12:32:10 ******/
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_Session_ClientSetting] FOREIGN KEY([ClientSettingID])
REFERENCES [dbo].[ClientSetting] ([ID])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_Session_ClientSetting]
GO
/****** Object:  ForeignKey [FK_Session_User]    Script Date: 08/08/2011 12:32:10 ******/
ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_Session_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_Session_User]
GO
