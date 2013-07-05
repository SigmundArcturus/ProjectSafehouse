USE [ProjectSafehouse]
GO

/****** Object:  Table [dbo].[ActionItemHistory]    Script Date: 7/3/2013 10:43:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ActionItemHistory](
	[ID] [uniqueidentifier] NOT NULL,
	[ActionItemID] [uniqueidentifier] NOT NULL,
	[ThingChanged] [varchar](250) NOT NULL,
	[DescriptionOfChange] [varchar](max) NULL,
	[ChangedBy] [uniqueidentifier] NOT NULL,
	[ChangedWhen] [datetime] NOT NULL,
 CONSTRAINT [PK_ActionItemHistory] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[ActionItemHistory]  WITH CHECK ADD  CONSTRAINT [FK_ActionItemHistory_ActionItems] FOREIGN KEY([ActionItemID])
REFERENCES [dbo].[ActionItems] ([ID])
GO

ALTER TABLE [dbo].[ActionItemHistory] CHECK CONSTRAINT [FK_ActionItemHistory_ActionItems]
GO

ALTER TABLE [dbo].[ActionItemHistory]  WITH CHECK ADD  CONSTRAINT [FK_ActionItemHistory_Users] FOREIGN KEY([ChangedBy])
REFERENCES [dbo].[Users] ([ID])
GO

ALTER TABLE [dbo].[ActionItemHistory] CHECK CONSTRAINT [FK_ActionItemHistory_Users]
GO


