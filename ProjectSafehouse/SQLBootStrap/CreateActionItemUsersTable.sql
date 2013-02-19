USE [ProjectSafehouse]
GO

/****** Object:  Table [dbo].[ActionItemUsers]    Script Date: 2/19/2013 11:21:03 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ActionItemUsers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ActionItemId] [uniqueidentifier] NOT NULL,
	[AssignedToUserID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ActionItemUsers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ActionItemUsers]  WITH CHECK ADD  CONSTRAINT [FK_ActionItemUsers_ActionItems] FOREIGN KEY([ActionItemId])
REFERENCES [dbo].[ActionItems] ([ID])
GO

ALTER TABLE [dbo].[ActionItemUsers] CHECK CONSTRAINT [FK_ActionItemUsers_ActionItems]
GO

ALTER TABLE [dbo].[ActionItemUsers]  WITH CHECK ADD  CONSTRAINT [FK_ActionItemUsers_Users] FOREIGN KEY([AssignedToUserID])
REFERENCES [dbo].[Users] ([ID])
GO

ALTER TABLE [dbo].[ActionItemUsers] CHECK CONSTRAINT [FK_ActionItemUsers_Users]
GO


