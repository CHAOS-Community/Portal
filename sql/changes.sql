IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Subscription_Get]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Subscription_Get]
GO

CREATE VIEW [dbo].[SubscriptionInfo]
AS
SELECT     dbo.Subscription.ID, dbo.Subscription.GUID, dbo.Subscription.Name, dbo.Subscription_User_Join.UserID, dbo.Subscription_User_Join.Permission, 
                      dbo.Subscription.DateCreated
FROM         dbo.Subscription LEFT OUTER JOIN
                      dbo.Subscription_User_Join ON dbo.Subscription.ID = dbo.Subscription_User_Join.SubscriptionID

GO

-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 2011.08.08
--				2011.09.06 : Renamed to SubscriptionInfo
-- =============================================
CREATE PROCEDURE [dbo].[SubscriptionInfo_Get]
	@ID				int					= null,
	@GUID			uniqueidentifier	= null,
	@Name			varchar(255)		= null,
	@RequestUserID	int
AS
BEGIN

	SET NOCOUNT ON;

	SELECT	*
	  FROM	SubscriptionInfo
	 WHERE	( @ID IS NULL OR @ID = [ID] ) AND
			( @GUID IS NULL OR @GUID = [GUID] ) AND
			( @Name IS NULL OR @Name = [Name] ) AND
			dbo.DoesUserHavePermissionToSubscription( @RequestUserID, ID, 'Get' ) = 1 

END
GO

