/****** Object:  StoredProcedure [dbo].[Repository_Delete]    Script Date: 05/25/2011 21:59:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Repository_Delete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Repository_Delete]
GO

/****** Object:  StoredProcedure [dbo].[Repository_Get]    Script Date: 05/25/2011 21:59:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Repository_Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Repository_Get]
GO

/****** Object:  StoredProcedure [dbo].[Repository_Insert]    Script Date: 05/25/2011 21:59:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Repository_Insert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Repository_Insert]
GO

-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.25.05
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE Subscription_Insert
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

-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2010.06.08
-- Description:	This Stored procedure inserts a user
-- =============================================
ALTER PROCEDURE [dbo].[User_Insert]
	@CreatingUserID			int			 = NULL,
	@Firstname				varchar(255),
	@Middlename				varchar(255) = NULL,
	@Lastname				varchar(255) = NULL,
	@Email					varchar(255)
AS
BEGIN
	
	IF( @CreatingUserID IS NOT NULL AND dbo.DoesUserHavePermissionToActionOnSubscription(@CreatingUserID,'Create User') = 0 )
		RAISERROR ('The User does not have sufficient permissions to create users', 16, 1)
	
	INSERT INTO [User]
           ([GUID]
           ,[Firstname]
           ,[Middlename]
           ,[Lastname]
           ,[Email])
     VALUES
           (NEWID()
           ,@Firstname
           ,@Middlename
           ,@Lastname
           ,@Email)

	SELECT	*
	  FROM	[User]
	 WHERE	[User].ID = @@IDENTITY
	
END

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

-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2010.06.13
-- Description:	This stored procedure inserts a new session
-- =============================================
ALTER PROCEDURE [dbo].[Session_Insert]
	@SessionID			uniqueidentifier = NEWID,
	@UserGUID			uniqueidentifier,
	@ClientSettingID	int
AS
BEGIN

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
