USE [ProjectSafehouse]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 02/07/2013 23:10:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Users](
	[ID] [uniqueidentifier] NOT NULL,
	[Email] [varchar](100) NOT NULL UNIQUE,
	[Name] [varchar](100) NULL,
	[Password] [varchar](100) NOT NULL,
	[AvatarURL] [varchar](100) NULL,
	[HourlyCost] [money] NULL,
	[OvertimeMultiplier] [decimal](18, 2) NULL,
	[OvertimeThreshold] [decimal](18, 2) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


