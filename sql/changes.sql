CREATE TABLE [dbo].[IndexSettings](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ModuleID] [int] NOT NULL,
	[Settings] [xml] NOT NULL,
	[DateCreated] [date] NULL,
 CONSTRAINT [PK_IndexSettings] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[IndexSettings]  WITH CHECK ADD  CONSTRAINT [FK_IndexSettings_Module] FOREIGN KEY([ModuleID])
REFERENCES [dbo].[Module] ([ID])
GO

ALTER TABLE [dbo].[IndexSettings] CHECK CONSTRAINT [FK_IndexSettings_Module]
GO

ALTER TABLE [dbo].[IndexSettings] ADD  CONSTRAINT [DF_IndexSettings_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO

-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 01.11.2011
--				This SP is used to create IndexSettings
-- =============================================
CREATE PROCEDURE [dbo].[IndexSettings_Create]
	@ModuleID	int,
	@Settings	xml
AS
BEGIN
	
	INSERT INTO [IndexSettings] ([ModuleID],[Settings],[DateCreated])
         VALUES (@ModuleID,@Settings,GETDATE())

	RETURN @@IDENTITY
 
END


-- =============================================
-- Author:		Jesper Fyhr Knudsen
-- Create date: 01.11.2011
--				This SP is used to GET a IndexSettings
-- =============================================
CREATE PROCEDURE [dbo].[IndexSettings_Get]
	@ModuleID	int
AS
BEGIN

	SELECT	*
	  FROM	[IndexSettings]
	 WHERE	ModuleID = @ModuleID
 
END
GO

CREATE TABLE [dbo].[Log] (
    [Id] [int] IDENTITY (1, 1) NOT NULL,
    [Date] [datetime] NOT NULL,
    [Thread] [varchar] (255) NOT NULL,
    [Level] [varchar] (50) NOT NULL,
    [Logger] [varchar] (255) NOT NULL,
    [Message] [varchar] (4000) NOT NULL,
    [Exception] [varchar] (2000) NULL
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

