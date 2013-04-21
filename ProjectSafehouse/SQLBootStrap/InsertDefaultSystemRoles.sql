USE [ProjectSafehouse]
GO

delete from dbo.systemroles

INSERT INTO [dbo].[SystemRoles]
           ([RoleName])
     VALUES
           ('SystemAdmin')

INSERT INTO [dbo].[SystemRoles]
           ([RoleName])
     VALUES
           ('CompanyAdmin')

INSERT INTO [dbo].[SystemRoles]
           ([RoleName])
     VALUES
           ('ProjectAdmin')

INSERT INTO [dbo].[SystemRoles]
           ([RoleName])
     VALUES
           ('User')

INSERT INTO [dbo].[SystemRoles]
           ([RoleName])
     VALUES
           ('Spectator')
GO


