USE [ProjectSafehouse]
GO

/****** Object:  Table [dbo].[Releases]    Script Date: 2/17/2013 3:23:00 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Releases](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [varchar](256) NOT NULL,
	[Description] [varchar](256) NULL,
	[ScheduledDate] [datetime] NULL,
	[StartDate] [datetime] NULL,
	[ScheduledByID] [uniqueidentifier] NOT NULL,
	[ProjectID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Releases] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Releases]  WITH CHECK ADD  CONSTRAINT [FK_Releases_Projects] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Projects] ([ID])
GO

ALTER TABLE [dbo].[Releases] CHECK CONSTRAINT [FK_Releases_Projects]
GO


