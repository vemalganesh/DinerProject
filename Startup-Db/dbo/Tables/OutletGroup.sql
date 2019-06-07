CREATE TABLE [dbo].[OutletGroup]
(
	[Id] NVARCHAR(128) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(128) NULL, 
    [Company_Id] NVARCHAR(128) NULL, 
    CONSTRAINT [Fk_OutletGroup_Company_RS] FOREIGN KEY ([Company_Id]) REFERENCES [dbo].[Company]([Id]) 
)
