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
USE [master];


GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creating $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [$(DatabaseName)], FILENAME = N'$(DefaultDataPath)$(DefaultFilePrefix)_Primary.mdf')
    LOG ON (NAME = [$(DatabaseName)_log], FILENAME = N'$(DefaultLogPath)$(DefaultFilePrefix)_Primary.ldf') COLLATE SQL_Latin1_General_CP1_CI_AS
GO
USE [$(DatabaseName)];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF),
                CONTAINMENT = NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF),
                MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = OFF,
                DELAYED_DURABILITY = DISABLED 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (QUERY_CAPTURE_MODE = ALL, DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_PLANS_PER_QUERY = 200, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367), MAX_STORAGE_SIZE_MB = 100) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE = OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
    END


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
PRINT N'Creating [dbo].[Company]...';


GO
CREATE TABLE [dbo].[Company] (
    [Id]        NVARCHAR (128) NOT NULL,
    [Name]      NVARCHAR (50)  NULL,
    [Address]   NVARCHAR (50)  NULL,
    [DbConnStr] NVARCHAR (50)  NULL,
    [Suspended] BIT            NULL,
    [ExpDate]   DATETIME       NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [AK_Name] UNIQUE NONCLUSTERED ([Name] ASC)
);


GO
PRINT N'Creating [dbo].[OutletGroup]...';


GO
CREATE TABLE [dbo].[OutletGroup] (
    [Id]         NVARCHAR (128) NOT NULL,
    [Name]       NVARCHAR (128) NULL,
    [Company_Id] NVARCHAR (128) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Outlets]...';


GO
CREATE TABLE [dbo].[Outlets] (
    [Id]         NVARCHAR (128) NOT NULL,
    [Name]       NVARCHAR (128) NULL,
    [OutletCode] NVARCHAR (MAX) NULL,
    [Address]    NVARCHAR (128) NULL,
    [Suspended]  BIT            NULL,
    [Company_Id] NVARCHAR (128) NULL,
    [Group_Id]   NVARCHAR (128) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Roles]...';


GO
CREATE TABLE [dbo].[Roles] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (128) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[User]...';


GO
CREATE TABLE [dbo].[User] (
    [Id]             NVARCHAR (128) NOT NULL,
    [Email]          NVARCHAR (50)  NULL,
    [Name]           NVARCHAR (50)  NULL,
    [Passsword]      NVARCHAR (50)  NULL,
    [Suspended]      BIT            NULL,
    [EmailVerified]  BIT            NULL,
    [OutletGroup_Id] NVARCHAR (128) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [AK_Email] UNIQUE NONCLUSTERED ([Email] ASC)
);


GO
PRINT N'Creating [dbo].[UserCompany(RS)]...';


GO
CREATE TABLE [dbo].[UserCompany(RS)] (
    [CompanyId] NVARCHAR (128) NOT NULL,
    [UserId]    NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.UserCompanyInfo] PRIMARY KEY CLUSTERED ([UserId] ASC, [CompanyId] ASC)
);


GO
PRINT N'Creating [dbo].[UserRole(RS)]...';


GO
CREATE TABLE [dbo].[UserRole(RS)] (
    [UserId] NVARCHAR (128) NOT NULL,
    [RoleId] INT            NOT NULL,
    CONSTRAINT [PK_dbo.UserRoles(RS)] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC)
);


GO
PRINT N'Creating [dbo].[Fk_OutletGroup_Company_RS]...';


GO
ALTER TABLE [dbo].[OutletGroup]
    ADD CONSTRAINT [Fk_OutletGroup_Company_RS] FOREIGN KEY ([Company_Id]) REFERENCES [dbo].[Company] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Outlet_Group_RS]...';


GO
ALTER TABLE [dbo].[Outlets]
    ADD CONSTRAINT [FK_Outlet_Group_RS] FOREIGN KEY ([Group_Id]) REFERENCES [dbo].[OutletGroup] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Outlet_CompanyId_RS]...';


GO
ALTER TABLE [dbo].[Outlets]
    ADD CONSTRAINT [FK_Outlet_CompanyId_RS] FOREIGN KEY ([Company_Id]) REFERENCES [dbo].[Company] ([Id]);


GO
PRINT N'Creating [dbo].[FK_User_OutletGroup_RS]...';


GO
ALTER TABLE [dbo].[User]
    ADD CONSTRAINT [FK_User_OutletGroup_RS] FOREIGN KEY ([OutletGroup_Id]) REFERENCES [dbo].[OutletGroup] ([Id]);


GO
PRINT N'Creating [dbo].[FK_dbo.UserCompanyInfo_dbo.Company_CompanyId]...';


GO
ALTER TABLE [dbo].[UserCompany(RS)]
    ADD CONSTRAINT [FK_dbo.UserCompanyInfo_dbo.Company_CompanyId] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Creating [dbo].[FK_dbo.UserCompanyInfo_dbo.Users_UserId]...';


GO
ALTER TABLE [dbo].[UserCompany(RS)]
    ADD CONSTRAINT [FK_dbo.UserCompanyInfo_dbo.Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Creating [dbo].[FK_dbo.UserRoles(RS)_UserId]...';


GO
ALTER TABLE [dbo].[UserRole(RS)]
    ADD CONSTRAINT [FK_dbo.UserRoles(RS)_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Creating [dbo].[FK_dbo.UserRoles(RS)_dbo.RoleId]...';


GO
ALTER TABLE [dbo].[UserRole(RS)]
    ADD CONSTRAINT [FK_dbo.UserRoles(RS)_dbo.RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id]) ON DELETE CASCADE;


GO
-- Refactoring step to update target server with deployed transaction logs

IF OBJECT_ID(N'dbo.__RefactorLog') IS NULL
BEGIN
    CREATE TABLE [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
    EXEC sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
END
GO
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '824c5ea7-624a-44c7-9671-2631e3d68be0')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('824c5ea7-624a-44c7-9671-2631e3d68be0')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'a0a63116-5129-41da-ad16-53a2cc272796')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('a0a63116-5129-41da-ad16-53a2cc272796')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'ac8abe6d-515e-40f8-a1cc-55fb1f4e91a3')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('ac8abe6d-515e-40f8-a1cc-55fb1f4e91a3')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '046ee604-e471-4ed2-93ac-57c4f394374b')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('046ee604-e471-4ed2-93ac-57c4f394374b')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '5ca30639-274b-4261-b1ba-5f8a6110bad1')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('5ca30639-274b-4261-b1ba-5f8a6110bad1')

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
select 1, N'Admin'
where not exists (select 1 from dbo.Roles where Id = 1)
GO
insert into dbo.Roles (Name)
select 1, N'CompanyAdmin'
where not exists (select 1 from dbo.Roles where Id = 1)
GO
insert into dbo.Roles (Name)
select 1, N'Manager'
where not exists (select 1 from dbo.Roles where Id = 1)
GO
insert into dbo.Roles (Name)
select 1, N'User'
where not exists (select 1 from dbo.Roles where Id = 1)
GO
insert into dbo.Roles (Name)
select 1, N'Guest'
where not exists (select 1 from dbo.Roles where Id = 1)
GO

GO
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END


GO
PRINT N'Update complete.';


GO