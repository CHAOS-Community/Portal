DROP PROCEDURE [dbo].[User_AssociateWithAuthenticationProvider]
GO
DROP PROCEDURE [dbo].[AuthenticationProvider_User_Join_Get]
GO
DROP PROCEDURE [dbo].[AuthenticationProvider_Insert]
GO
DROP PROCEDURE [dbo].[AuthenticationProvider_Get]
GO
ALTER TABLE [dbo].[AuthenticationProvider_User_Join] DROP CONSTRAINT [FK_AuthenticationProvider_User_Join_AuthenticationProvider]
GO
ALTER TABLE [dbo].[AuthenticationProvider_User_Join] DROP CONSTRAINT [FK_AuthenticationProvider_User_Join_User]
GO
ALTER TABLE [dbo].[AuthenticationProvider] DROP CONSTRAINT [DF_AuthenticationProvider_UniqueIdentifier]
GO
DROP TABLE [dbo].[AuthenticationProvider_User_Join]
GO
DROP TABLE [dbo].[AuthenticationProvider]
GO
-- =======================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2010.06.09
-- Description:	This stored procedure Gets a single user based on the unique identifier.
-- =============================================
ALTER PROCEDURE [dbo].[UserInfo_Get]
	@GUID									uniqueidentifier = NULL,
	@SessionID								uniqueidentifier = NULL,
	@Email									varchar(255)	 = NULL
AS
BEGIN

	SET NOCOUNT ON;

	SELECT	[UserInfo].*
	  FROM	[UserInfo]
	 WHERE	( @GUID IS NULL OR [UserInfo].[GUID] = @GUID ) AND
			( @SessionID IS NULL OR [UserInfo].SessionID = @SessionID ) AND
			( @Email IS NULL OR [UserInfo].Email = @Email )
    
END
GO
-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2010.06.09
-- Description:	This stored procedure Gets a single user based on the unique identifier.
-- =============================================
ALTER PROCEDURE [dbo].[User_Get]
	@UserID	int
	
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	[User].*
	  FROM	[User]
	 WHERE	@UserID IS NULL OR [User].ID = @UserID
    
END
GO
-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.07.01
-- Description:	Creates a user
-- =============================================
ALTER PROCEDURE [dbo].[User_Delete]
	@GUID		uniqueidentifier
AS
BEGIN
	
	DELETE
	  FROM	[Session]
	 WHERE	UserID IN ( SELECT	UserID
	                      FROM	[User]
	                     WHERE	[GUID] = @GUID )
	
	DELETE 
	  FROM	[User]
     WHERE  [User].GUID = @GUID
     
     RETURN @@ROWCOUNT
	
END
