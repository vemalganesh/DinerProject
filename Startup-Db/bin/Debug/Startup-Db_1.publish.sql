﻿/*
Deployment script for AppDb

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "AppDb"
:setvar DefaultFilePrefix "AppDb"
:setvar DefaultDataPath "C:\Users\vemal\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB"
:setvar DefaultLogPath "C:\Users\vemal\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'The following operation was generated from a refactoring log file 5901ddb2-4f0c-4a5e-82bb-de1234d93006';

PRINT N'Rename [dbo].[User] to Users';


GO
EXECUTE sp_rename @objname = N'[dbo].[User]', @newname = N'Users', @objtype = N'OBJECT';


GO
-- Refactoring step to update target server with deployed transaction logs
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '5901ddb2-4f0c-4a5e-82bb-de1234d93006')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('5901ddb2-4f0c-4a5e-82bb-de1234d93006')

GO

GO
/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


insert into dbo.Roles (Name)
select 1, N'SuperAdmin'
where not exists (select 1 from dbo.Roles where Id = 1)
GO
insert into dbo.Roles (Name)
select 2, N'Admin'
where not exists (select 2 from dbo.Roles where Id = 2)
GO
insert into dbo.Roles (Name)
select 3, N'CompanyAdmin'
where not exists (select 3 from dbo.Roles where Id = 3)
GO
insert into dbo.Roles (Name)
select 4, N'Manager'
where not exists (select 4 from dbo.Roles where Id = 4)
GO
insert into dbo.Roles (Name)
select 5, N'User'
where not exists (select 5 from dbo.Roles where Id = 5)
GO
insert into dbo.Roles (Name)
select 6, N'Guest'
where not exists (select 6 from dbo.Roles where Id = 6)
GO

GO
PRINT N'Update complete.';


GO
