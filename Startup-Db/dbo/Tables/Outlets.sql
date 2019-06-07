CREATE TABLE [dbo].[Outlets]
(
	[Id] NVARCHAR(128) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(128) NULL, 
    [OutletCode] NVARCHAR(MAX) NULL, 
    [Address] NVARCHAR(128) NULL, 
    [Suspended] BIT NULL, 
    [Company_Id] NVARCHAR(128) NULL, 
    [Group_Id] NVARCHAR(128) NULL , 
	CONSTRAINT [FK_Outlet_Group_RS] FOREIGN KEY ([Group_Id]) REFERENCES [dbo].[OutletGroup]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Outlet_CompanyId_RS] FOREIGN KEY ([Company_Id]) REFERENCES [dbo].[Company]([Id]) ON DELETE CASCADE,

)
