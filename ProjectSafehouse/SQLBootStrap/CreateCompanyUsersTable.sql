USE [ProjectSafehouse]
GO

/****** Object:  Table [dbo].[CompanyUsers]    Script Date: 3/11/2013 8:48:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CompanyUsers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[AddedBy] [uniqueidentifier] NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_CompanyUsers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[CompanyUsers]  WITH CHECK ADD  CONSTRAINT [FK_CompanyUsers_Companies] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Companies] ([ID])
GO

ALTER TABLE [dbo].[CompanyUsers] CHECK CONSTRAINT [FK_CompanyUsers_Companies]
GO

ALTER TABLE [dbo].[CompanyUsers]  WITH CHECK ADD  CONSTRAINT [FK_CompanyUsers_SystemRoles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[SystemRoles] ([ID])
GO

ALTER TABLE [dbo].[CompanyUsers] CHECK CONSTRAINT [FK_CompanyUsers_SystemRoles]
GO

ALTER TABLE [dbo].[CompanyUsers]  WITH CHECK ADD  CONSTRAINT [FK_CompanyUsers_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([ID])
GO

ALTER TABLE [dbo].[CompanyUsers] CHECK CONSTRAINT [FK_CompanyUsers_Users]
GO

ALTER TABLE [dbo].[CompanyUsers]  WITH CHECK ADD  CONSTRAINT [FK_CompanyUsers_Users1] FOREIGN KEY([AddedBy])
REFERENCES [dbo].[Users] ([ID])
GO

ALTER TABLE [dbo].[CompanyUsers] CHECK CONSTRAINT [FK_CompanyUsers_Users1]
GO


