CREATE TABLE [dbo].[Company]
(
	[Id] NVARCHAR(128) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(128) NULL, 
	CONSTRAINT AK_Name UNIQUE(Name),
    [Address] NVARCHAR(128) NULL, 
    [DbConnStr] NVARCHAR(128) NULL, 
    [Suspended] BIT NULL, 
    [ExpDate] DATETIME NULL 
)
