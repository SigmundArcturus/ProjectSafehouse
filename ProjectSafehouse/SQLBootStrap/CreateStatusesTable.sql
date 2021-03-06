USE [ProjectSafehouse]
GO

/****** Object:  Table [dbo].[Statuses]    Script Date: 2/19/2013 10:56:07 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Statuses](
	[ID] [uniqueidentifier] NOT NULL,
	[CompanyId] [uniqueidentifier] NULL,
	[Name] [varchar](256) NOT NULL,
	[Description] [varchar](max) NULL,
 CONSTRAINT [PK_Statuses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


