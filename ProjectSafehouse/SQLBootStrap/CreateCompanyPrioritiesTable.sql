USE [ProjectSafehouse]
GO

/****** Object:  Table [dbo].[CompanyPriorities]    Script Date: 2/19/2013 10:55:41 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CompanyPriorities](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [uniqueidentifier] NOT NULL,
	[PriorityNumber] [int] NOT NULL,
	[PriorityName] [varchar](256) NOT NULL,
 CONSTRAINT [PK_CompanyPriorities] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CompanyPriorities]  WITH CHECK ADD  CONSTRAINT [FK_CompanyPriorities_Companies] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([ID])
GO

ALTER TABLE [dbo].[CompanyPriorities] CHECK CONSTRAINT [FK_CompanyPriorities_Companies]
GO


