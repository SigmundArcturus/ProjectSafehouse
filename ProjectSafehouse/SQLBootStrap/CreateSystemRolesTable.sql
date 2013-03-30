USE [ProjectSafehouse]
GO

/****** Object:  Table [dbo].[SystemRoles]    Script Date: 3/11/2013 8:53:52 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[SystemRoles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](256) NOT NULL,
 CONSTRAINT [PK_SystemRoles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


