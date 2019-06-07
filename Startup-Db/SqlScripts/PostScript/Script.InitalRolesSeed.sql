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
SET IDENTITY_INSERT Roles ON
insert into Roles (Id,Name)
select 1, N'SuperAdmin'
where not exists (select 1 from dbo.Roles where Id = 1)
go
insert into Roles (Id,Name)
select 2, N'Admin'
where not exists (select 2 from dbo.Roles where Id = 2)
go
insert into Roles (Id,Name)
select 3, N'CompanyAdmin'
where not exists (select 3 from dbo.Roles where Id = 3)
go
insert into Roles (Id,Name)
select 4, N'Manager'
where not exists (select 4 from dbo.Roles where Id = 4)
go
insert into Roles (Id,Name)
select 5, N'User'
where not exists (select 5 from dbo.Roles where Id = 5)
go
insert into Roles (Id,Name)
select 6, N'Guest'
where not exists (select 6 from dbo.Roles where Id = 6)
go
