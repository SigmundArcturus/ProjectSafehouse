USE [ProjectSafehouse]
GO

/****** Object:  Table [dbo].[Priorities]    Script Date: 2/20/2013 10:32:32 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Priorities](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [uniqueidentifier] NOT NULL,
	[Number] [int] NOT NULL,
	[Name] [varchar](256) NOT NULL,
	[Description] [varchar](max) NULL,
 CONSTRAINT [PK_CompanyPriorities] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Priorities]  WITH CHECK ADD  CONSTRAINT [FK_CompanyPriorities_Companies] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([ID])
GO

ALTER TABLE [dbo].[Priorities] CHECK CONSTRAINT [FK_CompanyPriorities_Companies]
GO


