USE [ProjectSafehouse]
GO

/****** Object:  Table [dbo].[ActionItems]    Script Date: 2/19/2013 10:53:18 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ActionItems](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [varchar](256) NOT NULL,
	[Description] [varchar](256) NULL,
	[CreatedByUserId] [uniqueidentifier] NOT NULL,
	[InReleaseId] [uniqueidentifier] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[CurrentStatusId] [uniqueidentifier] NOT NULL,
	[ActionItemTypeId] [uniqueidentifier] NOT NULL,
	[Estimate] [bigint] NULL,
	[TimeSpent] [bigint] NULL,
	[DateCompleted] [datetime] NULL,
	[IndividualTargetDate] [datetime] NULL,
	[CurrentPriority] [int] NOT NULL,
 CONSTRAINT [PK_ActionItems] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ActionItems]  WITH CHECK ADD  CONSTRAINT [FK_ActionItems_ActionItemTypes] FOREIGN KEY([ActionItemTypeId])
REFERENCES [dbo].[ActionItemTypes] ([ID])
GO

ALTER TABLE [dbo].[ActionItems] CHECK CONSTRAINT [FK_ActionItems_ActionItemTypes]
GO

ALTER TABLE [dbo].[ActionItems]  WITH CHECK ADD  CONSTRAINT [FK_ActionItems_Statuses] FOREIGN KEY([CurrentStatusId])
REFERENCES [dbo].[Statuses] ([ID])
GO

ALTER TABLE [dbo].[ActionItems] CHECK CONSTRAINT [FK_ActionItems_Statuses]
GO


